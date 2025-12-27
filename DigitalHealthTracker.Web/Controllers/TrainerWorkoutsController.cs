using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels.Trainer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalHealthTracker.Web.Controllers;

[RequireRole("Trainer")]
public class TrainerWorkoutsController : Controller
{
	// LIST
	[HttpGet("/Trainer/Workouts")]
	public async Task<IActionResult> Index()
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var list = await client.GetFromJsonAsync<List<WorkoutListRowApiDto>>("/api/Workouts")
				   ?? new List<WorkoutListRowApiDto>();

		var vm = list.Select(x => new WorkoutListRowVm
		{
			Id = x.Id,
			Name = x.Name ?? x.Title ?? $"Workout #{x.Id}",
			Description = x.Description
		}).ToList();

		return View(vm);
	}

	// CREATE (GET)
	[HttpGet("/Trainer/Workouts/Create")]
	public IActionResult Create()
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		return View(new WorkoutEditVm());
	}

	// CREATE (POST)
	[HttpPost("/Trainer/Workouts/Create")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(WorkoutEditVm vm)
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		if (!ModelState.IsValid)
			return View(vm);

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var res = await client.PostAsJsonAsync("/api/Workouts", new
		{
			name = vm.Name,
			description = vm.Description
		});

		if (res.IsSuccessStatusCode)
		{
			TempData["Success"] = "Workout created.";
			return RedirectToAction(nameof(Index));
		}

		var body = await SafeReadAsync(res);
		vm.Error = res.StatusCode == HttpStatusCode.BadRequest
			? body
			: $"Create failed (HTTP {(int)res.StatusCode}): {body}";

		return View(vm);
	}

	// EDIT (GET)
	[HttpGet("/Trainer/Workouts/{id:int}/Edit")]
	public async Task<IActionResult> Edit(int id)
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var dto = await client.GetFromJsonAsync<WorkoutDetailApiDto>($"/api/Workouts/{id}");
		if (dto is null)
		{
			TempData["Error"] = "Workout not found.";
			return RedirectToAction(nameof(Index));
		}

		var vm = new WorkoutEditVm
		{
			Id = dto.Id,
			Name = dto.Name ?? dto.Title ?? "",
			Description = dto.Description
		};

		return View(vm);
	}

	// EDIT (POST)
	[HttpPost("/Trainer/Workouts/{id:int}/Edit")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, WorkoutEditVm vm)
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		vm.Id = id;

		if (!ModelState.IsValid)
			return View(vm);

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var res = await client.PutAsJsonAsync($"/api/Workouts/{id}", new
		{
			id = id,
			name = vm.Name,
			description = vm.Description
		});

		if (res.IsSuccessStatusCode)
		{
			TempData["Success"] = "Workout updated.";
			return RedirectToAction(nameof(Index));
		}

		var body = await SafeReadAsync(res);
		vm.Error = res.StatusCode == HttpStatusCode.BadRequest
			? body
			: $"Update failed (HTTP {(int)res.StatusCode}): {body}";

		return View(vm);
	}

	// DELETE (POST)
	[HttpPost("/Trainer/Workouts/{id:int}/Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(int id)
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var res = await client.DeleteAsync($"/api/Workouts/{id}");

		if (res.IsSuccessStatusCode)
		{
			TempData["Success"] = "Workout deleted.";
			return RedirectToAction(nameof(Index));
		}

		var body = await SafeReadAsync(res);
		TempData["Error"] = $"Delete failed (HTTP {(int)res.StatusCode}): {body}";
		return RedirectToAction(nameof(Index));
	}

	private static async Task<string> SafeReadAsync(HttpResponseMessage res)
	{
		try { return await res.Content.ReadAsStringAsync(); }
		catch { return ""; }
	}

	// --- local API DTOs (API farklı isim döndürebilir diye alternatif alanlar ekledim) ---

	private sealed class WorkoutListRowApiDto
	{
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("name")] public string? Name { get; set; }
		[JsonPropertyName("title")] public string? Title { get; set; }
		[JsonPropertyName("description")] public string? Description { get; set; }
	}

	private sealed class WorkoutDetailApiDto
	{
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("name")] public string? Name { get; set; }
		[JsonPropertyName("title")] public string? Title { get; set; }
		[JsonPropertyName("description")] public string? Description { get; set; }
	}
}
