using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalHealthTracker.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TrainersController : ControllerBase
	{
		private readonly AppDbContext _context;
		public TrainersController(AppDbContext context) => _context = context;

		// GET: /api/Trainers
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var list = await _context.Trainers
				.OrderByDescending(t => t.Id)
				.Select(t => new
				{
					t.Id,
					t.Name,
					t.Surname,
					t.Phone,
					t.Email,
					t.IsApproved
				})
				.ToListAsync();

			return Ok(list);
		}

		// GET: /api/Trainers/{id}
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var t = await _context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
			if (t == null) return NotFound("Trainer not found.");

			return Ok(new
			{
				t.Id,
				t.Name,
				t.Surname,
				t.Phone,
				t.Email,
				t.IsApproved
			});
		}

		// GET: /api/Trainers/pending
		[HttpGet("pending")]
		public async Task<IActionResult> GetPending()
		{
			var list = await _context.Trainers
				.Where(t => t.IsApproved == false)
				.OrderByDescending(t => t.Id)
				.Select(t => new
				{
					t.Id,
					t.Name,
					t.Surname,
					t.Phone,
					t.Email,
					t.IsApproved
				})
				.ToListAsync();

			return Ok(list);
		}

		// PUT: /api/Trainers/{id}
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, [FromBody] TrainerUpdateRequest req)
		{
			var t = await _context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
			if (t == null) return NotFound("Trainer not found.");

			if (string.IsNullOrWhiteSpace(req.Name) ||
				string.IsNullOrWhiteSpace(req.Surname) ||
				string.IsNullOrWhiteSpace(req.Phone))
				return BadRequest("Name, Surname, Phone are required.");

			t.Name = req.Name.Trim();
			t.Surname = req.Surname.Trim();
			t.Phone = req.Phone.Trim();
			t.Email = string.IsNullOrWhiteSpace(req.Email) ? "" : req.Email.Trim();

			await _context.SaveChangesAsync();
			return Ok();
		}

		// DELETE: /api/Trainers/{id}
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var t = await _context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
			if (t == null) return NotFound("Trainer not found.");

			_context.Trainers.Remove(t);
			await _context.SaveChangesAsync();
			return Ok();
		}

		// PUT: /api/Trainers/{id}/approve
		[HttpPut("{id:int}/approve")]
		public async Task<IActionResult> Approve(int id)
		{
			var t = await _context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
			if (t == null) return NotFound("Trainer not found.");

			t.IsApproved = true;
			await _context.SaveChangesAsync();
			return Ok();
		}

		// ✅ PUT: /api/Trainers/{id}/unapprove
		[HttpPut("{id:int}/unapprove")]
		public async Task<IActionResult> Unapprove(int id)
		{
			var t = await _context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
			if (t == null) return NotFound("Trainer not found.");

			t.IsApproved = false;
			await _context.SaveChangesAsync();
			return Ok();
		}
	}

	public class TrainerUpdateRequest
	{
		public string Name { get; set; } = "";
		public string Surname { get; set; } = "";
		public string Phone { get; set; } = "";
		public string? Email { get; set; }
	}
}
