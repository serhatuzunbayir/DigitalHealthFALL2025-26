using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalHealthTracker.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly AppDbContext _context;

		public UsersController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Users
		[HttpGet]
		public async Task<ActionResult<List<User>>> GetAll()
		{
			var users = await _context.Users
				.OrderByDescending(u => u.Id)
				.ToListAsync();

			return Ok(users);
		}

		// GET: api/Users/5
		[HttpGet("{id:int}")]
		public async Task<ActionResult<User>> GetById(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();
			return Ok(user);
		}

		// ❌ ADMIN CREATE YOK (Register ile oluşuyor)
		// [HttpPost] kaldırıldı

		// PUT: api/Users/5
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, User user)
		{
			var existing = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
			if (existing == null) return NotFound($"User not found: {id}");

			existing.Name = user.Name;
			existing.Surname = user.Surname;
			existing.Email = user.Email;
			existing.Phone = user.Phone;
			existing.MedicalRecord = user.MedicalRecord;
			existing.BirthYear = user.BirthYear;
			existing.HeightCm = user.HeightCm;
			existing.WeightKg = user.WeightKg;

			// PasswordHash'a dokunma!
			await _context.SaveChangesAsync();
			return Ok(existing);
		}

		// DELETE: api/Users/5
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
			return Ok();
		}

		// desktop UI -> combobox (kalsın)
		[HttpGet("lookup")]
		public async Task<IActionResult> GetLookup()
		{
			var users = await _context.Users
				.OrderBy(u => u.Id)
				.Select(u => new
				{
					u.Id,
					FullName = u.Name + " " + u.Surname + " (" + u.Phone + ")"
				})
				.ToListAsync();

			return Ok(users);
		}

		// GET: api/Users/assigned-to-trainer/5
		// Trainer kendi sorumlu olduğu kullanıcıları combobox için çeker
		[HttpGet("assigned-to-trainer/{trainerId:int}")]
		public async Task<IActionResult> GetUsersAssignedToTrainer(int trainerId)
		{
			var users = await _context.AssignedPrograms
				.Where(ap => ap.TrainerId == trainerId)
				.Select(ap => ap.UserId)
				.Distinct()
				.Join(
					_context.Users,
					userId => userId,
					u => u.Id,
					(userId, u) => new
					{
						u.Id,
						FullName = u.Name + " " + u.Surname + " (" + u.Phone + ")",
						u.HeightCm,
						u.WeightKg
					}
				)
				.OrderBy(x => x.Id)
				.ToListAsync();

			return Ok(users);
		}


	}
}
