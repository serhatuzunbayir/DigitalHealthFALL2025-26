using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigitalHealthTracker.Desktop.Services
{
	public class WorkoutLogApiService
	{
		// USER history
		public async Task<List<WorkoutLogRowDto>> GetUserHistoryAsync(int userId)
		{
			using var client = ApiClient.Create();
			var data = await client.GetFromJsonAsync<List<WorkoutLogRowDto>>($"/api/WorkoutLogs/user/{userId}");
			return data ?? new List<WorkoutLogRowDto>();
		}

		// TRAINER student logs (aynı şeyi döndürüyor, farklı endpoint)
		public async Task<List<WorkoutLogRowDto>> GetByUserAsync(int userId)
		{
			using var client = ApiClient.Create();
			var data = await client.GetFromJsonAsync<List<WorkoutLogRowDto>>($"/api/WorkoutLogs/by-user/{userId}");
			return data ?? new List<WorkoutLogRowDto>();
		}
	}
}
