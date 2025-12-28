using System.Net.Http.Json;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalHealthTracker.Web.Controllers;

public class LogsController : Controller
{
	[RequireRole("Trainer")]
	[HttpGet("/Trainer/Logs/{userId:int}")]
	public async Task<IActionResult> TrainerUserLogs(int userId)
	{
		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var list = await client.GetFromJsonAsync<List<WorkoutLogRowVm>>($"/api/WorkoutLogs/by-user/{userId}")
				   ?? new List<WorkoutLogRowVm>();

		ViewBag.UserId = userId;
		return View(list);
	}

	[RequireRole("User")]
	[HttpGet("/User/Logs")]
	public async Task<IActionResult> UserLogs()
	{
		var userId = HttpContext.Session.GetInt32("UserId");
		if (userId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var list = await client.GetFromJsonAsync<List<WorkoutLogRowVm>>($"/api/WorkoutLogs/user/{userId}")
				   ?? new List<WorkoutLogRowVm>();

		return View(list);
	}

}
