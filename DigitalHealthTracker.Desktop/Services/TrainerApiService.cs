using DigitalHealthTracker.Data.Entities;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigitalHealthTracker.Desktop.Services
{
	public class TrainerApiService
	{
		public async Task<List<Trainer>> GetAllAsync()
		{
			using var client = ApiClient.Create();
			var trainers = await client.GetFromJsonAsync<List<Trainer>>("/api/Trainers");
			return trainers ?? new List<Trainer>();
		}

		public async Task<List<Trainer>> GetPendingAsync()
		{
			using var client = ApiClient.Create();
			var trainers = await client.GetFromJsonAsync<List<Trainer>>("/api/Trainers/pending");
			return trainers ?? new List<Trainer>();
		}

		// ✅ GET: api/Trainers/{id}
		public async Task<Trainer?> GetByIdAsync(int id)
		{
			using var client = ApiClient.Create();
			return await client.GetFromJsonAsync<Trainer>($"/api/Trainers/{id}");
		}

		// POST: api/Trainers (Admin add kaldırıldı, normalde kullanılmayacak)
		public async Task<Trainer?> CreateAsync(Trainer trainer)
		{
			using var client = ApiClient.Create();
			var resp = await client.PostAsJsonAsync("/api/Trainers", trainer);
			resp.EnsureSuccessStatusCode();
			return await resp.Content.ReadFromJsonAsync<Trainer>();
		}

		// ✅ FIX: PUT /api/Trainers/{id} API body dönmüyor olabilir (boş response)
		// Bu yüzden JSON parse yapmıyoruz. Güncel halini görmek istiyorsak tekrar GET yapıyoruz.
		public async Task<Trainer?> UpdateAsync(int id, Trainer trainer)
		{
			using var client = ApiClient.Create();

			var resp = await client.PutAsJsonAsync($"/api/Trainers/{id}", trainer);
			resp.EnsureSuccessStatusCode();

			// API Ok() ile boş body dönebilir -> tekrar GET ile güncel veriyi al
			return await client.GetFromJsonAsync<Trainer>($"/api/Trainers/{id}");
		}

		public async Task ApproveAsync(int id)
		{
			using var client = ApiClient.Create();
			var resp = await client.PutAsync($"/api/Trainers/{id}/approve", null);
			resp.EnsureSuccessStatusCode();
		}

		// ✅ (İleride lazım olacak) Unapprove endpoint'in var: /api/Trainers/{id}/unapprove
		public async Task UnapproveAsync(int id)
		{
			using var client = ApiClient.Create();
			var resp = await client.PutAsync($"/api/Trainers/{id}/unapprove", null);
			resp.EnsureSuccessStatusCode();
		}

		public async Task DeleteAsync(int id)
		{
			using var client = ApiClient.Create();
			var resp = await client.DeleteAsync($"/api/Trainers/{id}");
			resp.EnsureSuccessStatusCode();
		}
	}
}
