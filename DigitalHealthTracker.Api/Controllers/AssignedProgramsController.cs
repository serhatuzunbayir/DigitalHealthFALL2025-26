using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalHealthTracker.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AssignedProgramsController : ControllerBase
	{
		private readonly AppDbContext _context;
		public AssignedProgramsController(AppDbContext context) => _context = context;

		// =========================
		// TRAINER: LIST
		// GET /api/AssignedPrograms/trainer/{trainerId}
		// =========================
		[HttpGet("trainer/{trainerId:int}")]
		public async Task<IActionResult> GetForTrainer(int trainerId)
		{
			var list = await _context.AssignedPrograms
				.Include(a => a.User)
				.Include(a => a.WorkoutProgram)
				.Where(a => a.TrainerId == trainerId)
				.OrderByDescending(a => a.AssignedAt)
				.Select(a => new
				{
					a.Id,
					User = a.User.Name + " " + a.User.Surname,
					Program = a.WorkoutProgram.Title,
					Status = a.Status.ToString(),
					AssignedAt = a.AssignedAt.ToString("yyyy-MM-dd HH:mm"),
					ApprovedAt = a.ApprovedAt.HasValue ? a.ApprovedAt.Value.ToString("yyyy-MM-dd HH:mm") : ""
				})
				.ToListAsync();

			return Ok(list);
		}

		// =========================
		// TRAINER: ASSIGN
		// POST /api/AssignedPrograms
		// =========================
		[HttpPost]
		public async Task<IActionResult> Assign([FromBody] AssignProgramRequest req)
		{
			if (req.TrainerId <= 0 || req.UserId <= 0 || req.WorkoutProgramId <= 0)
				return BadRequest("TrainerId, UserId, WorkoutProgramId are required.");

			// program gerçekten bu trainer'ın mı?
			bool ownsProgram = await _context.WorkoutPrograms
				.AnyAsync(p => p.Id == req.WorkoutProgramId && p.TrainerId == req.TrainerId);

			if (!ownsProgram)
				return BadRequest("You can only assign your own programs.");

			// aynı program tamamlanmadan tekrar atanmasın (opsiyonel)
			bool exists = await _context.AssignedPrograms.AnyAsync(a =>
				a.UserId == req.UserId &&
				a.WorkoutProgramId == req.WorkoutProgramId &&
				a.Status != AssignmentStatus.Completed);

			if (exists)
				return BadRequest("This program is already assigned to the user (not completed yet).");

			var assignment = new AssignedProgram
			{
				TrainerId = req.TrainerId,
				UserId = req.UserId,
				WorkoutProgramId = req.WorkoutProgramId,
				Status = AssignmentStatus.Pending,
				AssignedAt = DateTime.Now
			};

			_context.AssignedPrograms.Add(assignment);
			await _context.SaveChangesAsync();

			return Ok(new { assignment.Id });
		}

		// =========================
		// TRAINER: DELETE
		// DELETE /api/AssignedPrograms/{id}?trainerId=#
		// =========================
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id, [FromQuery] int trainerId)
		{
			var assignment = await _context.AssignedPrograms.SingleOrDefaultAsync(a => a.Id == id);
			if (assignment == null) return NotFound("Assignment not found.");

			if (assignment.TrainerId != trainerId)
				return BadRequest("You can only delete your own assignments.");

			_context.AssignedPrograms.Remove(assignment);
			await _context.SaveChangesAsync();
			return Ok();
		}

		// =========================
		// USER: PENDING LIST
		// GET /api/AssignedPrograms/user/{userId}
		// =========================
		[HttpGet("user/{userId:int}")]
		public async Task<IActionResult> GetForUser(int userId)
		{
			var list = await _context.AssignedPrograms
				.Include(a => a.Trainer)
				.Include(a => a.WorkoutProgram)
				.Where(a => a.UserId == userId)
				.OrderByDescending(a => a.AssignedAt)
				.Select(a => new
				{
					a.Id,
					a.WorkoutProgramId,
					ProgramTitle = a.WorkoutProgram.Title,
					TrainerName = a.Trainer.Name + " " + a.Trainer.Surname,
					Status = a.Status.ToString(),
					AssignedAt = a.AssignedAt.ToString("yyyy-MM-dd HH:mm"),
					ApprovedAt = a.ApprovedAt.HasValue ? a.ApprovedAt.Value.ToString("yyyy-MM-dd HH:mm") : ""
				})
				.ToListAsync();

			return Ok(list);
		}

		// =========================
		// USER: APPROVE
		// PUT /api/AssignedPrograms/{id}/approve?userId=#
		// =========================
		[HttpPut("{id:int}/approve")]
		public async Task<IActionResult> Approve(int id, [FromQuery] int userId)
		{
			var assignment = await _context.AssignedPrograms.SingleOrDefaultAsync(a => a.Id == id);
			if (assignment == null) return NotFound("Assignment not found.");

			if (assignment.UserId != userId)
				return BadRequest("You can only approve your own assignments.");

			if (assignment.Status != AssignmentStatus.Pending)
				return BadRequest("Only Pending assignments can be approved.");

			assignment.Status = AssignmentStatus.Active;
			assignment.ApprovedAt = DateTime.Now;

			await _context.SaveChangesAsync();
			return Ok();
		}

		// =========================
		// USER: ACTIVE ASSIGNMENT
		// GET /api/AssignedPrograms/user/{userId}/active
		// =========================
		[HttpGet("user/{userId:int}/active")]
		public async Task<IActionResult> GetActiveForUser(int userId)
		{
			var active = await _context.AssignedPrograms
				.Include(a => a.WorkoutProgram)
				.Include(a => a.Trainer)
				.Where(a => a.UserId == userId && a.Status == AssignmentStatus.Active)
				.OrderByDescending(a => a.ApprovedAt)
				.Select(a => new
				{
					a.Id,
					WorkoutProgramId = a.WorkoutProgramId,
					ProgramTitle = a.WorkoutProgram.Title,
					TrainerName = a.Trainer.Name + " " + a.Trainer.Surname
				})
				.FirstOrDefaultAsync();

			return Ok(active); // null dönebilir
		}

		// =========================
		// USER: COMPLETE WITH LOGS (PROGRAMIN TÜM ITEM'LARINI LOGA BASAR)
		// PUT /api/AssignedPrograms/{id}/complete-with-logs?userId=#
		// =========================
		[HttpPut("{id:int}/complete-with-logs")]
		public async Task<IActionResult> CompleteWithLogs(int id, [FromQuery] int userId)
		{
			var assignment = await _context.AssignedPrograms
				.Include(a => a.WorkoutProgram)
					.ThenInclude(p => p.Items)
				.SingleOrDefaultAsync(a => a.Id == id);

			if (assignment == null) return NotFound("Assignment not found.");

			if (assignment.UserId != userId)
				return BadRequest("You can only complete your own assignment.");

			if (assignment.Status != AssignmentStatus.Active)
				return BadRequest("Only ACTIVE assignments can be completed.");

			// ✅ EN ÖNEMLİ DÜZELTME:
			// Aynı PROGRAM tekrar assign edilirse tekrar complete edilebilsin.
			// Engeli program bazlı değil, assignment bazlı yap.
			bool existsForThisAssignment = await _context.WorkoutLogs
				.AnyAsync(l => l.UserId == userId && l.AssignedProgramId == assignment.Id);

			if (existsForThisAssignment)
				return BadRequest("This assignment already completed (logs exist).");

			var items = assignment.WorkoutProgram.Items
				.OrderBy(i => i.DayNo)
				.ToList();

			if (items.Count == 0)
				return BadRequest("Program has no items.");

			foreach (var it in items)
			{
				// her item için log bas
				var log = new WorkoutLog
				{
					UserId = userId,
					TrainerId = assignment.TrainerId,
					WorkoutProgramId = assignment.WorkoutProgramId,
					WorkoutId = it.WorkoutId,
					DayNo = it.DayNo,
					Sets = it.Sets,
					Reps = it.Reps,
					AssignedProgramId = assignment.Id,
					CompletedAt = DateTime.Now
				};

				_context.WorkoutLogs.Add(log);
			}

			assignment.Status = AssignmentStatus.Completed;

			await _context.SaveChangesAsync();
			return Ok();
		}
	}

	public class AssignProgramRequest
	{
		public int TrainerId { get; set; }
		public int UserId { get; set; }
		public int WorkoutProgramId { get; set; }
	}
}
