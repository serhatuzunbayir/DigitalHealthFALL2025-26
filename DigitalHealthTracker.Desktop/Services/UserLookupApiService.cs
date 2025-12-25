using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigitalHealthTracker.Desktop.Services
{
	// get user fropm API then fill to UI Forms -> new User()
	public class UserLookupApiService
	{
		public async Task<List<UserLookupDto>> GetLookupAsync()
		{
			using var client = ApiClient.Create();
			var data = await client.GetFromJsonAsync<List<UserLookupDto>>("/api/Users/lookup");
			return data ?? new List<UserLookupDto>();
		}
	}

	public class UserLookupDto
	{
		public int Id { get; set; }
		public string FullName { get; set; } = "";
	}
}
