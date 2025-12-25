using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigitalHealthTracker.Desktop.Services
{
	public class UserProgramApiService
	{
		// GET: /api/WorkoutPrograms/{programId}/user/{userId}
		public async Task<UserProgramDetailDto?> GetProgramForUserAsync(int programId, int userId)
		{
			using var client = ApiClient.Create();
			return await client.GetFromJsonAsync<UserProgramDetailDto>(
				$"/api/WorkoutPrograms/{programId}/user/{userId}"
			);
		}

		// POST: /api/WorkoutLogs  (API Sets/Reps/TrainerId'yi kendi dolduruyor)
		public async Task LogCompletedItemAsync(int userId, int programId, int workoutId, int dayNo)
		{
			using var client = ApiClient.Create();

			var req = new CreateWorkoutLogRequest
			{
				UserId = userId,
				WorkoutProgramId = programId,
				WorkoutId = workoutId,
				DayNo = dayNo
			};

			var resp = await client.PostAsJsonAsync("/api/WorkoutLogs", req);

			if (!resp.IsSuccessStatusCode)
			{
				var body = await resp.Content.ReadAsStringAsync();
				throw new System.Exception(body);
			}
		}
	}

	public class UserProgramDetailDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = "";
		public int TrainerId { get; set; }
		public List<UserProgramItemDto> Items { get; set; } = new();
	}

	public class UserProgramItemDto
	{
		public int WorkoutId { get; set; }
		public string WorkoutName { get; set; } = "";
		public string WorkoutDescription { get; set; } = "";
		public int DayNo { get; set; }
		public int Sets { get; set; }
		public int Reps { get; set; }
		public bool IsCompleted { get; set; }
		public string CompletedAt { get; set; } = "";
	}

	public class CreateWorkoutLogRequest
	{
		public int UserId { get; set; }
		public int WorkoutProgramId { get; set; }
		public int WorkoutId { get; set; }
		public int DayNo { get; set; }
	}
}
