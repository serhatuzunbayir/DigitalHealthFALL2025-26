using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalHealthTracker.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WorkoutsController : ControllerBase
	{
		private readonly AppDbContext _context;
		public WorkoutsController(AppDbContext context) => _context = context;

		// GET: api/Workouts
		[HttpGet]
		public async Task<ActionResult<List<Workout>>> GetAll()
		{
			var workouts = await _context.Workouts
				.OrderBy(w => w.Id)
				.ToListAsync();

			return Ok(workouts);
		}

		// GET: api/Workouts/{id}
		[HttpGet("{id:int}")]
		public async Task<ActionResult<Workout>> GetById(int id)
		{
			var workout = await _context.Workouts.FindAsync(id);
			if (workout == null) return NotFound();
			return Ok(workout);
		}

		// POST: api/Workouts
		[HttpPost]
		public async Task<ActionResult<Workout>> Create(Workout workout)
		{
			if (string.IsNullOrWhiteSpace(workout.Name))
				return BadRequest("Name is required.");

			if (string.IsNullOrWhiteSpace(workout.Description))
				return BadRequest("Description is required.");

			_context.Workouts.Add(workout);
			await _context.SaveChangesAsync();
			return Ok(workout);
		}

		// PUT: api/Workouts/{id}  (route id esas)
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, Workout workout)
		{
			var existing = await _context.Workouts.FirstOrDefaultAsync(x => x.Id == id);
			if (existing == null) return NotFound($"Workout not found: {id}");

			existing.Name = workout.Name?.Trim() ?? "";
			existing.Description = workout.Description?.Trim() ?? "";

			await _context.SaveChangesAsync();
			return Ok(existing);
		}

		// DELETE: api/Workouts/{id}
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var workout = await _context.Workouts.FindAsync(id);
			if (workout == null) return NotFound();

			// 1) Bu workout'a bağlı logları sil (WorkoutLogs)
			var logs = await _context.WorkoutLogs
				.Where(x => x.WorkoutId == id)
				.ToListAsync();
			if (logs.Count > 0)
				_context.WorkoutLogs.RemoveRange(logs);

			// 2) Bu workout'u kullanan program item'larını sil (WorkoutProgramItems)
			var items = await _context.WorkoutProgramItems
				.Where(x => x.WorkoutId == id)
				.ToListAsync();
			if (items.Count > 0)
				_context.WorkoutProgramItems.RemoveRange(items);

			// 3) En son workout'u sil
			_context.Workouts.Remove(workout);

			await _context.SaveChangesAsync();
			return Ok();
		}

	}
}
