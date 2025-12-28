using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigitalHealthTracker.Desktop.Services
{
	public class WorkoutProgramApiService
	{
		public async Task<List<WorkoutProgramListDto>> GetByTrainerAsync(int trainerId)
		{
			using var client = ApiClient.Create();
			var data = await client.GetFromJsonAsync<List<WorkoutProgramListDto>>($"/api/WorkoutPrograms/trainer/{trainerId}");
			return data ?? new List<WorkoutProgramListDto>();
		}

		public async Task<WorkoutProgramDetailDto?> GetByIdAsync(int id)
		{
			using var client = ApiClient.Create();
			return await client.GetFromJsonAsync<WorkoutProgramDetailDto>($"/api/WorkoutPrograms/{id}");
		}

		public async Task<int> CreateAsync(WorkoutProgramSaveRequest req)
		{
			using var client = ApiClient.Create();
			var resp = await client.PostAsJsonAsync("/api/WorkoutPrograms", req);
			resp.EnsureSuccessStatusCode();
			var r = await resp.Content.ReadFromJsonAsync<CreateResponse>();
			return r?.Id ?? 0;
		}

		public async Task UpdateAsync(int id, WorkoutProgramSaveRequest req)
		{
			using var client = ApiClient.Create();
			var resp = await client.PutAsJsonAsync($"/api/WorkoutPrograms/{id}", req);
			resp.EnsureSuccessStatusCode();
		}

		public async Task DeleteAsync(int id)
		{
			using var client = ApiClient.Create();
			var resp = await client.DeleteAsync($"/api/WorkoutPrograms/{id}");
			resp.EnsureSuccessStatusCode();
		}

		public class CreateResponse { public int Id { get; set; } }
	}

	public class WorkoutProgramListDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = "";
		public int TrainerId { get; set; }
	}

	public class WorkoutProgramDetailDto
	{
		public int Id { get; set; }
		public string Title { get; set; } = "";
		public int TrainerId { get; set; }
		public List<WorkoutProgramItemDto> Items { get; set; } = new();
	}

	public class WorkoutProgramItemDto
	{
		public int WorkoutId { get; set; }
		public string WorkoutName { get; set; } = "";
		public int DayNo { get; set; }
		public int Sets { get; set; }
		public int Reps { get; set; }
	}

	public class WorkoutProgramSaveRequest
	{
		public int TrainerId { get; set; }
		public string Title { get; set; } = "";
		public List<WorkoutProgramItemSave> Items { get; set; } = new();
	}

	public class WorkoutProgramItemSave
	{
		public int WorkoutId { get; set; }
		public int DayNo { get; set; }
		public int Sets { get; set; }
		public int Reps { get; set; }
	}
}
