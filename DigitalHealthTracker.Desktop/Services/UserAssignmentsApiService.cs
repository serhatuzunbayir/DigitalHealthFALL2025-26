using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigitalHealthTracker.Desktop.Services
{
	public class UserAssignmentsApiService
	{
		// GET /api/AssignedPrograms/user/{userId}
		public async Task<List<UserPendingAssignmentDto>> GetPendingAsync(int userId)
		{
			using var client = ApiClient.Create();

			var all = await client
				.GetFromJsonAsync<List<UserAssignedProgramRowDto>>($"/api/AssignedPrograms/user/{userId}")
				?? new List<UserAssignedProgramRowDto>();

			var pending = new List<UserPendingAssignmentDto>();

			foreach (var a in all)
			{
				if (a.Status == "Pending")
				{
					pending.Add(new UserPendingAssignmentDto
					{
						Id = a.Id,
						Program = a.ProgramTitle,
						Trainer = a.TrainerName,
						AssignedAt = a.AssignedAt
					});
				}
			}

			return pending;
		}

		// PUT /api/AssignedPrograms/{id}/approve?userId=#
		public async Task ApproveAsync(int assignmentId, int userId)
		{
			using var client = ApiClient.Create();

			var url = $"/api/AssignedPrograms/{assignmentId}/approve?userId={userId}";
			var resp = await client.PutAsync(url, null);
			var body = await resp.Content.ReadAsStringAsync();

			if (!resp.IsSuccessStatusCode)
				throw new System.Exception($"PUT {url} -> {(int)resp.StatusCode}\n{body}");
		}
	}
}
