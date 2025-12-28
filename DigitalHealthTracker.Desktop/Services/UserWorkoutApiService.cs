using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigitalHealthTracker.Desktop.Services
{
	public class UserWorkoutApiService
	{
		// GET /api/WorkoutLogs/user/{userId}
		public async Task<List<UserWorkoutLogDto>> GetLogsAsync(int userId)
		{
			using var client = ApiClient.Create();

			var data = await client.GetFromJsonAsync<List<UserWorkoutLogDto>>(
				$"/api/WorkoutLogs/user/{userId}"
			);

			return data ?? new List<UserWorkoutLogDto>();
		}
	}

	public class UserWorkoutLogDto
	{
		public int Id { get; set; }
		public string Program { get; set; } = "";
		public string Workout { get; set; } = "";
		public int DayNo { get; set; }
		public int Sets { get; set; }
		public int Reps { get; set; }
		public string CompletedAt { get; set; } = "";
	}
}
