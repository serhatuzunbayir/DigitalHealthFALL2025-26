using System.Net.Http.Json;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalHealthTracker.Web.Controllers;

[RequireRole("Trainer")]
public class TrainerController : Controller
{
	[HttpGet("/Trainer/Assign")]
	public async Task<IActionResult> Assign()
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var users = await client.GetFromJsonAsync<List<UserLookupVm>>("/api/Users/lookup")
			?? new List<UserLookupVm>();


		var programs = await client.GetFromJsonAsync<List<ProgramPickVm>>($"/api/WorkoutPrograms/trainer/{trainerId}")
					  ?? new List<ProgramPickVm>();

		var vm = new AssignProgramVm
		{
			Users = users,
			Programs = programs
		};

		return View(vm);
	}

	[HttpPost("/Trainer/Assign")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Assign(AssignProgramVm vm)
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var reqBody = new
		{
			trainerId = trainerId.Value,
			userId = vm.UserId,
			workoutProgramId = vm.WorkoutProgramId
		};

		var resp = await client.PostAsJsonAsync("/api/AssignedPrograms", reqBody);
		var body = await resp.Content.ReadAsStringAsync();

		if (!resp.IsSuccessStatusCode)
		{
			// tekrar dropdownları doldur
			vm.Users = await client.GetFromJsonAsync<List<UserLookupVm>>("/api/Users/lookup") ?? new List<UserLookupVm>();


			vm.Programs = await client.GetFromJsonAsync<List<ProgramPickVm>>($"/api/WorkoutPrograms/trainer/{trainerId}")
						 ?? new List<ProgramPickVm>();

			vm.Error = $"Assign failed ({(int)resp.StatusCode}): {body}";
			return View(vm);
		}

		TempData["Msg"] = "Program assigned.";
		return Redirect("/Trainer");
	}
}
