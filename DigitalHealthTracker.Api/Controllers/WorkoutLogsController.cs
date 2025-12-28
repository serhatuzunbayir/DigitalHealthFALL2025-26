using DigitalHealthTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalHealthTracker.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WorkoutLogsController : ControllerBase
	{
		private readonly AppDbContext _context;
		public WorkoutLogsController(AppDbContext context) => _context = context;

		// ✅ USER: tüm log geçmişi
		// GET: /api/WorkoutLogs/user/10
		[HttpGet("user/{userId:int}")]
		public async Task<IActionResult> GetHistoryForUser(int userId)
		{
			var logs = await BuildUserLogs(userId);
			return Ok(logs);
		}

		// ✅ TRAINER: seçilen user'ın logları (aynı data, farklı route)
		// GET: /api/WorkoutLogs/by-user/10
		[HttpGet("by-user/{userId:int}")]
		public async Task<IActionResult> GetByUser(int userId)
		{
			var logs = await BuildUserLogs(userId);
			return Ok(logs);
		}

		// ortak query (tek yerden yönetelim)
		private async Task<List<object>> BuildUserLogs(int userId)
		{
			var logs = await _context.WorkoutLogs
				.AsNoTracking()
				.Include(l => l.WorkoutProgram)
				.Include(l => l.Workout)
				.Where(l => l.UserId == userId)
				.OrderByDescending(l => l.CompletedAt)
				.Select(l => new
				{
					l.Id,
					ProgramTitle = l.WorkoutProgram.Title,
					WorkoutName = l.Workout.Name,
					l.DayNo,
					l.Sets,
					l.Reps,
					CompletedAt = l.CompletedAt.ToString("yyyy-MM-dd HH:mm")
				})
				.ToListAsync();

			// anonymous list'i object list olarak döndürüyoruz (controller içinde kullanmak için)
			return logs.Cast<object>().ToList();
		}
	}
}
