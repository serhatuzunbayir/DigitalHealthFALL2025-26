using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigitalHealthTracker.Desktop.Services
{
	public class AssignedProgramApiService
	{
		// =========================
		// TRAINER SIDE
		// =========================

		public async Task<List<AssignedProgramRowDto>> GetForTrainerAsync(int trainerId)
		{
			using var client = ApiClient.Create();
			var data = await client.GetFromJsonAsync<List<AssignedProgramRowDto>>(
				$"/api/AssignedPrograms/trainer/{trainerId}"
			);
			return data ?? new List<AssignedProgramRowDto>();
		}

		public async Task AssignAsync(int trainerId, int userId, int programId)
		{
			using var client = ApiClient.Create();

			var req = new
			{
				TrainerId = trainerId,
				UserId = userId,
				WorkoutProgramId = programId
			};

			var resp = await client.PostAsJsonAsync("/api/AssignedPrograms", req);
			var body = await resp.Content.ReadAsStringAsync();

			if (!resp.IsSuccessStatusCode)
				throw new System.Exception(body);
		}

		public async Task DeleteAsync(int assignmentId, int trainerId)
		{
			using var client = ApiClient.Create();

			var resp = await client.DeleteAsync($"/api/AssignedPrograms/{assignmentId}?trainerId={trainerId}");
			var body = await resp.Content.ReadAsStringAsync();

			if (!resp.IsSuccessStatusCode)
				throw new System.Exception(body);
		}

		// =========================
		// USER SIDE (MyWorkout için)
		// =========================

		// GET /api/AssignedPrograms/user/{userId}/active
		public async Task<ActiveAssignmentDto?> GetActiveForUserAsync(int userId)
		{
			using var client = ApiClient.Create();

			var url = $"/api/AssignedPrograms/user/{userId}/active";
			var resp = await client.GetAsync(url);
			var body = await resp.Content.ReadAsStringAsync();

			if (!resp.IsSuccessStatusCode)
				throw new System.Exception($"GET {url} -> {(int)resp.StatusCode}\n{body}");

			if (string.IsNullOrWhiteSpace(body) || body.Trim() == "null")
				return null;

			return JsonSerializer.Deserialize<ActiveAssignmentDto>(
				body,
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
			);
		}

		// PUT /api/AssignedPrograms/{id}/complete-with-logs?userId=#
		public async Task CompleteWithLogsAsync(int assignmentId, int userId)
		{
			using var client = ApiClient.Create();

			var url = $"/api/AssignedPrograms/{assignmentId}/complete-with-logs?userId={userId}";
			var resp = await client.PutAsync(url, null);
			var body = await resp.Content.ReadAsStringAsync();

			if (!resp.IsSuccessStatusCode)
				throw new System.Exception(body);
		}
	}

	public class AssignedProgramRowDto
	{
		public int Id { get; set; }
		public string User { get; set; } = "";
		public string Program { get; set; } = "";
		public string Status { get; set; } = "";
		public string AssignedAt { get; set; } = "";
		public string ApprovedAt { get; set; } = "";
	}
}
