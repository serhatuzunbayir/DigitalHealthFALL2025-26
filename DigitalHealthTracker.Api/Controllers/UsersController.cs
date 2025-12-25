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

		[HttpGet]
		public async Task<ActionResult<List<User>>> GetAll()
		{
			var users = await _context.Users
				.OrderByDescending(u => u.Id)
				.ToListAsync();

			return Ok(users);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<User>> GetById(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();
			return Ok(user);
		}

		[HttpPost]
		public async Task<ActionResult<User>> Create(User user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return Ok(user);
		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, User user)
		{
			var existing = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
			if (existing == null) return NotFound($"User not found: {id}");

			// Route id esas: body'den Id gelmese bile sorun yok
			existing.Name = user.Name;
			existing.Surname = user.Surname;
			existing.Email = user.Email;
			existing.Phone = user.Phone;
			existing.MedicalRecord = user.MedicalRecord;
			existing.BirthYear = user.BirthYear;
			existing.HeightCm = user.HeightCm;
			existing.WeightKg = user.WeightKg;

			await _context.SaveChangesAsync();
			return Ok(existing);
		}


		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound();

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();
			return Ok();
		}


		// get spesific user according t0o id in order to assign(trainer) program to user
		//desktop UI -> combobox
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

	}
}
