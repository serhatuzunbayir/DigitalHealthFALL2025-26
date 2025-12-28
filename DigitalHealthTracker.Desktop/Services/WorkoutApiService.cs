using DigitalHealthTracker.Data.Entities;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigitalHealthTracker.Desktop.Services
{
	public class WorkoutApiService
	{
		public async Task<List<Workout>> GetAllAsync()
		{
			using var client = ApiClient.Create();
			var data = await client.GetFromJsonAsync<List<Workout>>("/api/Workouts");
			return data ?? new List<Workout>();
		}

		public async Task<Workout?> CreateAsync(Workout workout)
		{
			using var client = ApiClient.Create();
			var resp = await client.PostAsJsonAsync("/api/Workouts", workout);
			resp.EnsureSuccessStatusCode();
			return await resp.Content.ReadFromJsonAsync<Workout>();
		}

		public async Task<Workout?> UpdateAsync(int id, Workout workout)
		{
			using var client = ApiClient.Create();
			var resp = await client.PutAsJsonAsync($"/api/Workouts/{id}", workout);
			resp.EnsureSuccessStatusCode();
			return await resp.Content.ReadFromJsonAsync<Workout>();
		}

		public async Task DeleteAsync(int id)
		{
			using var client = ApiClient.Create();
			var resp = await client.DeleteAsync($"/api/Workouts/{id}");
			resp.EnsureSuccessStatusCode();
		}
	}
}
