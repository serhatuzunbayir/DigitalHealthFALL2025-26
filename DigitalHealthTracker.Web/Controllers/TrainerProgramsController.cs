using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels;
using DigitalHealthTracker.Web.ViewModels.Trainer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalHealthTracker.Web.Controllers;

[RequireRole("Trainer")]
public class TrainerProgramsController : Controller
{
	// =========================
	// LIST
	// =========================

	[HttpGet("/Trainer/Programs")]
	public async Task<IActionResult> Index()
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var list = await client.GetFromJsonAsync<List<ProgramListRowVm>>($"/api/WorkoutPrograms/trainer/{trainerId}")
				   ?? new List<ProgramListRowVm>();

		return View(list);
	}

	// =========================
	// CREATE
	// =========================

	[HttpGet("/Trainer/Programs/Create")]
	public async Task<IActionResult> Create()
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var vm = new WorkoutProgramEditVm
		{
			TrainerId = trainerId.Value,
			Title = "",
			Items = new List<WorkoutProgramItemEditVm>
			{
				new WorkoutProgramItemEditVm { DayNo = 1, Sets = 3, Reps = 10 }
			}
		};

		await FillWorkoutsAsync(client, vm);
		return View(vm);
	}

	[HttpPost("/Trainer/Programs/Create")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(WorkoutProgramEditVm vm, string action)
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		vm.TrainerId = trainerId.Value;

		// remove
		if (!string.IsNullOrWhiteSpace(action) && action.StartsWith("remove:", StringComparison.OrdinalIgnoreCase))
		{
			if (TryParseRemoveIndex(action, out var removeIndex))
			{
				if (removeIndex >= 0 && removeIndex < vm.Items.Count)
					vm.Items.RemoveAt(removeIndex);
			}

			if (vm.Items.Count == 0)
				vm.Items.Add(new WorkoutProgramItemEditVm { DayNo = 1, Sets = 3, Reps = 10 });

			ModelState.Clear();
			await FillWorkoutsAsync(client, vm);
			return View(vm);
		}

		// add
		if (string.Equals(action, "add", StringComparison.OrdinalIgnoreCase))
		{
			vm.Items.Add(new WorkoutProgramItemEditVm { DayNo = 1, Sets = 3, Reps = 10 });

			ModelState.Clear();
			await FillWorkoutsAsync(client, vm);
			return View(vm);
		}

		// save
		vm.Items ??= new List<WorkoutProgramItemEditVm>();
		if (vm.Items.Count == 0)
			ModelState.AddModelError("", "Program en az 1 item içermeli.");

		foreach (var it in vm.Items)
			it.DayNo = 1;

		if (!ModelState.IsValid)
		{
			await FillWorkoutsAsync(client, vm);
			return View(vm);
		}

		var requestBody = new WorkoutProgramCreateUpdateApiRequest
		{
			TrainerId = vm.TrainerId,
			Title = vm.Title,
			Items = vm.Items.Select(x => new WorkoutProgramItemApiDto
			{
				WorkoutId = x.WorkoutId,
				DayNo = x.DayNo,
				Sets = x.Sets,
				Reps = x.Reps
			}).ToList()
		};

		var res = await client.PostAsJsonAsync("/api/WorkoutPrograms", requestBody);

		if (res.IsSuccessStatusCode)
		{
			TempData["Success"] = "Program oluşturuldu.";
			return RedirectToAction(nameof(Index));
		}

		TempData["Error"] = res.StatusCode == HttpStatusCode.BadRequest
			? (await SafeReadStringAsync(res)).Trim()
			: $"Program oluşturulamadı. (HTTP {(int)res.StatusCode})";

		await FillWorkoutsAsync(client, vm);
		return View(vm);
	}

	// =========================
	// EDIT
	// =========================

	[HttpGet("/Trainer/Programs/{id:int}/Edit")]
	public async Task<IActionResult> Edit(int id)
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var program = await client.GetFromJsonAsync<WorkoutProgramDetailApiDto>($"/api/WorkoutPrograms/{id}");
		if (program is null)
		{
			TempData["Error"] = "Program bulunamadı.";
			return RedirectToAction(nameof(Index));
		}

		if (program.TrainerId != trainerId.Value)
		{
			TempData["Error"] = "Bu programa erişimin yok.";
			return RedirectToAction(nameof(Index));
		}

		var vm = new WorkoutProgramEditVm
		{
			Id = program.Id,
			TrainerId = program.TrainerId,
			Title = program.Title ?? "",
			Items = (program.Items ?? new List<WorkoutProgramItemApiDto>())
				.Select(x => new WorkoutProgramItemEditVm
				{
					WorkoutId = x.WorkoutId,
					DayNo = x.DayNo <= 0 ? 1 : x.DayNo,
					Sets = x.Sets <= 0 ? 1 : x.Sets,
					Reps = x.Reps <= 0 ? 1 : x.Reps
				})
				.ToList()
		};

		if (vm.Items.Count == 0)
			vm.Items.Add(new WorkoutProgramItemEditVm { DayNo = 1, Sets = 3, Reps = 10 });

		await FillWorkoutsAsync(client, vm);
		return View(vm);
	}

	[HttpPost("/Trainer/Programs/{id:int}/Edit")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, WorkoutProgramEditVm vm, string action)
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		vm.TrainerId = trainerId.Value;
		vm.Id = id;

		// remove
		if (!string.IsNullOrWhiteSpace(action) && action.StartsWith("remove:", StringComparison.OrdinalIgnoreCase))
		{
			if (TryParseRemoveIndex(action, out var removeIndex))
			{
				if (removeIndex >= 0 && removeIndex < vm.Items.Count)
					vm.Items.RemoveAt(removeIndex);
			}

			if (vm.Items.Count == 0)
				vm.Items.Add(new WorkoutProgramItemEditVm { DayNo = 1, Sets = 3, Reps = 10 });

			ModelState.Clear();
			await FillWorkoutsAsync(client, vm);
			return View(vm);
		}

		// add
		if (string.Equals(action, "add", StringComparison.OrdinalIgnoreCase))
		{
			vm.Items.Add(new WorkoutProgramItemEditVm { DayNo = 1, Sets = 3, Reps = 10 });

			ModelState.Clear();
			await FillWorkoutsAsync(client, vm);
			return View(vm);
		}

		// save
		vm.Items ??= new List<WorkoutProgramItemEditVm>();
		if (vm.Items.Count == 0)
			ModelState.AddModelError("", "Program en az 1 item içermeli.");

		foreach (var it in vm.Items)
			it.DayNo = 1;

		if (!ModelState.IsValid)
		{
			await FillWorkoutsAsync(client, vm);
			return View(vm);
		}

		var requestBody = new WorkoutProgramCreateUpdateApiRequest
		{
			TrainerId = vm.TrainerId,
			Title = vm.Title,
			Items = vm.Items.Select(x => new WorkoutProgramItemApiDto
			{
				WorkoutId = x.WorkoutId,
				DayNo = x.DayNo,
				Sets = x.Sets,
				Reps = x.Reps
			}).ToList()
		};

		var res = await client.PutAsJsonAsync($"/api/WorkoutPrograms/{id}", requestBody);

		if (res.IsSuccessStatusCode)
		{
			TempData["Success"] = "Program güncellendi.";
			return RedirectToAction(nameof(Index));
		}

		TempData["Error"] = res.StatusCode == HttpStatusCode.BadRequest
			? (await SafeReadStringAsync(res)).Trim()
			: $"Program güncellenemedi. (HTTP {(int)res.StatusCode})";

		await FillWorkoutsAsync(client, vm);
		return View(vm);
	}

	// =========================
	// DELETE
	// =========================

	[HttpPost("/Trainer/Programs/{id:int}/Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(int id)
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var program = await client.GetFromJsonAsync<WorkoutProgramDetailApiDto>($"/api/WorkoutPrograms/{id}");
		if (program is null)
		{
			TempData["Error"] = "Program bulunamadı.";
			return RedirectToAction(nameof(Index));
		}

		if (program.TrainerId != trainerId.Value)
		{
			TempData["Error"] = "Bu programı silemezsin.";
			return RedirectToAction(nameof(Index));
		}

		var res = await client.DeleteAsync($"/api/WorkoutPrograms/{id}");

		if (res.IsSuccessStatusCode)
		{
			TempData["Success"] = "Program silindi.";
			return RedirectToAction(nameof(Index));
		}

		TempData["Error"] = $"Program silinemedi. (HTTP {(int)res.StatusCode})";
		return RedirectToAction(nameof(Index));
	}

	// =========================
	// Helpers
	// =========================

	private static bool TryParseRemoveIndex(string action, out int index)
	{
		index = -1;
		var parts = action.Split(':', 2, StringSplitOptions.RemoveEmptyEntries);
		return parts.Length == 2 && int.TryParse(parts[1], out index);
	}

	private static async Task<string> SafeReadStringAsync(HttpResponseMessage res)
	{
		try { return await res.Content.ReadAsStringAsync(); }
		catch { return ""; }
	}

	private static async Task FillWorkoutsAsync(HttpClient client, WorkoutProgramEditVm vm)
	{
		var workouts = await client.GetFromJsonAsync<List<WorkoutLookupApiDto>>("/api/Workouts")
					  ?? new List<WorkoutLookupApiDto>();

		vm.Workouts = workouts.Select(w => new WorkoutLookupVm
		{
			Id = w.Id,
			Name = w.Name ?? w.Title ?? $"Workout #{w.Id}"
		}).ToList();
	}

	// =========================
	// Local API DTOs
	// =========================

	private sealed class WorkoutProgramDetailApiDto
	{
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("trainerId")] public int TrainerId { get; set; }
		[JsonPropertyName("title")] public string? Title { get; set; }
		[JsonPropertyName("items")] public List<WorkoutProgramItemApiDto>? Items { get; set; }
	}

	private sealed class WorkoutProgramCreateUpdateApiRequest
	{
		[JsonPropertyName("trainerId")] public int TrainerId { get; set; }
		[JsonPropertyName("title")] public string Title { get; set; } = "";
		[JsonPropertyName("items")] public List<WorkoutProgramItemApiDto> Items { get; set; } = new();
	}

	private sealed class WorkoutProgramItemApiDto
	{
		[JsonPropertyName("workoutId")] public int WorkoutId { get; set; }
		[JsonPropertyName("dayNo")] public int DayNo { get; set; }
		[JsonPropertyName("sets")] public int Sets { get; set; }
		[JsonPropertyName("reps")] public int Reps { get; set; }
	}

	private sealed class WorkoutLookupApiDto
	{
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("name")] public string? Name { get; set; }
		[JsonPropertyName("title")] public string? Title { get; set; }
	}
}
