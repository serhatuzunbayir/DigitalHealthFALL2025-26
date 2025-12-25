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

		public TrainersController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Trainers
		[HttpGet]
		public async Task<ActionResult<List<Trainer>>> GetAll()
		{
			var trainers = await _context.Trainers
				.OrderBy(t => t.Id)
				.ToListAsync();

			return Ok(trainers);
		}

		// GET: api/Trainers/pending
		[HttpGet("pending")]
		public async Task<ActionResult<List<Trainer>>> GetPending()
		{
			var pending = await _context.Trainers
				.Where(t => t.IsApproved == false)
				.OrderBy(t => t.Id)
				.ToListAsync();

			return Ok(pending);
		}

		// GET: api/Trainers/5
		[HttpGet("{id:int}")]
		public async Task<ActionResult<Trainer>> GetById(int id)
		{
			var trainer = await _context.Trainers.FindAsync(id);
			if (trainer == null) return NotFound();
			return Ok(trainer);
		}

		// POST: api/Trainers
		[HttpPost]
		public async Task<ActionResult<Trainer>> Create(Trainer trainer)
		{
			_context.Trainers.Add(trainer);
			await _context.SaveChangesAsync();
			return Ok(trainer);
		}

		// PUT: api/Trainers/5  (route id esas)
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, Trainer trainer)
		{
			var existing = await _context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
			if (existing == null) return NotFound($"Trainer not found: {id}");

			existing.Name = trainer.Name;
			existing.Surname = trainer.Surname;
			existing.Phone = trainer.Phone;
			existing.Email = trainer.Email;
			existing.BirthYear = trainer.BirthYear;
			existing.IsApproved = trainer.IsApproved;

			await _context.SaveChangesAsync();
			return Ok(existing);
		}

		// PUT: api/Trainers/5/approve
		[HttpPut("{id:int}/approve")]
		public async Task<IActionResult> Approve(int id)
		{
			var trainer = await _context.Trainers.FindAsync(id);
			if (trainer == null) return NotFound($"Trainer not found: {id}");

			trainer.IsApproved = true;
			await _context.SaveChangesAsync();
			return Ok(trainer);
		}

		// DELETE: api/Trainers/5
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var trainer = await _context.Trainers.FindAsync(id);
			if (trainer == null) return NotFound();

			_context.Trainers.Remove(trainer);
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}
