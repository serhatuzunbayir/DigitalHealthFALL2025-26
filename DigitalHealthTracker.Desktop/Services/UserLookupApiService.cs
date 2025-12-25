using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigitalHealthTracker.Desktop.Services
{
	// Trainer combobox: sadece trainer'ın sorumlu olduğu userlar
	public class UserLookupApiService
	{
		public async Task<List<UserLookupDto>> GetAssignedToTrainerAsync(int trainerId)
		{
			using var client = ApiClient.Create();
			var data = await client.GetFromJsonAsync<List<UserLookupDto>>(
				$"/api/Users/assigned-to-trainer/{trainerId}"
			);

			return data ?? new List<UserLookupDto>();
		}

		// (İstersen eskisi de kalsın: genel lookup)
		public async Task<List<UserLookupDto>> GetLookupAsync()
		{
			using var client = ApiClient.Create();
			var data = await client.GetFromJsonAsync<List<UserLookupDto>>("/api/Users/lookup");
			return data ?? new List<UserLookupDto>();
		}
	}
}
