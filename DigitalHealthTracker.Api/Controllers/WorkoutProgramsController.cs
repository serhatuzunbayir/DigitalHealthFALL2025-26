using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalHealthTracker.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WorkoutProgramsController : ControllerBase
	{
		private readonly AppDbContext _context;
		public WorkoutProgramsController(AppDbContext context) => _context = context;

		// GET: api/WorkoutPrograms/trainer/3
		[HttpGet("trainer/{trainerId:int}")]
		public async Task<IActionResult> GetByTrainer(int trainerId)
		{
			var programs = await _context.WorkoutPrograms
				.Where(p => p.TrainerId == trainerId)
				.OrderBy(p => p.Id)
				.Select(p => new
				{
					p.Id,
					p.Title,
					p.TrainerId
				})
				.ToListAsync();

			return Ok(programs);
		}

		// GET: api/WorkoutPrograms/5  (edit ekranı için items + workout name)
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			var program = await _context.WorkoutPrograms
				.Include(p => p.Items)
				.ThenInclude(i => i.Workout)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (program == null) return NotFound();

			var dto = new
			{
				program.Id,
				program.Title,
				program.TrainerId,
				Items = program.Items
					.OrderBy(x => x.DayNo)
					.Select(x => new
					{
						x.WorkoutId,
						WorkoutName = x.Workout != null ? x.Workout.Name : "",
						x.DayNo,
						x.Sets,
						x.Reps
					})
					.ToList()
			};

			return Ok(dto);
		}

		// POST: api/WorkoutPrograms
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] WorkoutProgramCreateUpdateRequest req)
		{
			if (string.IsNullOrWhiteSpace(req.Title))
				return BadRequest("Title is required.");

			if (req.Items == null || req.Items.Count == 0)
				return BadRequest("At least one item is required.");

			var program = new WorkoutProgram
			{
				Title = req.Title.Trim(),
				TrainerId = req.TrainerId,
				Items = req.Items.Select(it => new WorkoutProgramItem
				{
					WorkoutId = it.WorkoutId,
					DayNo = it.DayNo,
					Sets = it.Sets,
					Reps = it.Reps
				}).ToList()
			};

			_context.WorkoutPrograms.Add(program);
			await _context.SaveChangesAsync();
			return Ok(new { program.Id });
		}

		// PUT: api/WorkoutPrograms/5
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, [FromBody] WorkoutProgramCreateUpdateRequest req)
		{
			var program = await _context.WorkoutPrograms
				.Include(p => p.Items)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (program == null) return NotFound("Program not found.");

			if (string.IsNullOrWhiteSpace(req.Title))
				return BadRequest("Title is required.");

			if (req.Items == null || req.Items.Count == 0)
				return BadRequest("At least one item is required.");

			// güvenlik: trainer değiştirme
			// (istersen kontrol: program.TrainerId == req.TrainerId)
			program.Title = req.Title.Trim();

			// eski item’ları sil, yenileri ekle (senin desktop mantığın)
			_context.WorkoutProgramItems.RemoveRange(program.Items);

			program.Items = req.Items.Select(it => new WorkoutProgramItem
			{
				WorkoutId = it.WorkoutId,
				DayNo = it.DayNo,
				Sets = it.Sets,
				Reps = it.Reps
			}).ToList();

			await _context.SaveChangesAsync();
			return Ok();
		}

		// DELETE: api/WorkoutPrograms/5  (senin desktop delete logic birebir)
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var program = await _context.WorkoutPrograms
				.Include(p => p.Items)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (program == null) return NotFound("Program not found.");

			// 1) assignments
			var assignments = await _context.AssignedPrograms
				.Where(a => a.WorkoutProgramId == program.Id)
				.ToListAsync();
			if (assignments.Count > 0)
				_context.AssignedPrograms.RemoveRange(assignments);

			// 2) logs
			var logs = await _context.WorkoutLogs
				.Where(l => l.WorkoutProgramId == program.Id)
				.ToListAsync();
			if (logs.Count > 0)
				_context.WorkoutLogs.RemoveRange(logs);

			// 3) items
			if (program.Items != null && program.Items.Count > 0)
				_context.WorkoutProgramItems.RemoveRange(program.Items);

			// 4) program
			_context.WorkoutPrograms.Remove(program);

			await _context.SaveChangesAsync();
			return Ok();
		}

		// ✅ USER VIEW: Program items + user log status
		// GET: /api/WorkoutPrograms/{programId}/user/{userId}
		[HttpGet("{programId:int}/user/{userId:int}")]
		public async Task<IActionResult> GetProgramForUser(int programId, int userId)
		{
			var program = await _context.WorkoutPrograms
				.Include(p => p.Items)
				.ThenInclude(i => i.Workout)
				.FirstOrDefaultAsync(p => p.Id == programId);

			if (program == null) return NotFound("Program not found.");

			// user loglarını al
			var logs = await _context.WorkoutLogs
				.Where(l => l.UserId == userId && l.WorkoutProgramId == programId)
				.ToListAsync();

			var items = program.Items
				.OrderBy(i => i.DayNo)
				.ThenBy(i => i.Workout.Name)
				.Select(i => new
				{
					i.WorkoutId,
					WorkoutName = i.Workout.Name,
					WorkoutDescription = i.Workout.Description,
					i.DayNo,
					i.Sets,
					i.Reps,
					IsCompleted = logs.Any(l => l.DayNo == i.DayNo && l.WorkoutId == i.WorkoutId),
					CompletedAt = logs
						.Where(l => l.DayNo == i.DayNo && l.WorkoutId == i.WorkoutId)
						.Select(l => l.CompletedAt.ToString("yyyy-MM-dd HH:mm"))
						.FirstOrDefault() ?? ""
				})
				.ToList();

			return Ok(new
			{
				program.Id,
				program.Title,
				program.TrainerId,
				Items = items
			});
		}


	}

	// Request model (aynı dosyada durabilir)
	public class WorkoutProgramCreateUpdateRequest
	{
		public int TrainerId { get; set; }
		public string Title { get; set; } = "";
		public List<WorkoutProgramItemRequest> Items { get; set; } = new();
	}

	public class WorkoutProgramItemRequest
	{
		public int WorkoutId { get; set; }
		public int DayNo { get; set; }
		public int Sets { get; set; }
		public int Reps { get; set; }
	}
}
