using DigitalHealthTracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigitalHealthTracker.Desktop.Services
{
	public class UserApiService
	{
		// GET: api/Users
		public async Task<List<User>> GetAllAsync()
		{
			using var client = ApiClient.Create();
			var users = await client.GetFromJsonAsync<List<User>>("/api/Users");
			return users ?? new List<User>();
		}

		// ✅ NEW: GET: api/Users/{id}
		public async Task<User?> GetByIdAsync(int id)
		{
			using var client = ApiClient.Create();
			return await client.GetFromJsonAsync<User>($"/api/Users/{id}");
		}

		// POST: api/Users  (Admin add kaldırıldı, normalde kullanılmayacak)
		public async Task<User?> CreateAsync(User user)
		{
			using var client = ApiClient.Create();
			var resp = await client.PostAsJsonAsync("/api/Users", user);
			resp.EnsureSuccessStatusCode();
			return await resp.Content.ReadFromJsonAsync<User>();
		}

		// PUT: api/Users/{id}
		public async Task<User?> UpdateAsync(int id, User user)
		{
			using var client = ApiClient.Create();
			var resp = await client.PutAsJsonAsync($"/api/Users/{id}", user);

			if (!resp.IsSuccessStatusCode)
			{
				var body = await resp.Content.ReadAsStringAsync();
				throw new Exception($"PUT failed: {(int)resp.StatusCode} {resp.ReasonPhrase}\n{body}");
			}

			return await resp.Content.ReadFromJsonAsync<User>();
		}

		// DELETE: api/Users/{id}
		public async Task DeleteAsync(int id)
		{
			using var client = ApiClient.Create();
			var response = await client.DeleteAsync($"/api/Users/{id}");
			response.EnsureSuccessStatusCode();
		}
	}
}
