using System.Net.Http.Json;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHealthTracker.Web.Controllers;

[RequireRole("Trainer")]
public class TrainerController : Controller
{
	// Trainer dashboard: My Users
	[HttpGet("/Trainer")]
	public async Task<IActionResult> Trainer()
	{
		var trainerId = HttpContext.Session.GetInt32("UserId");
		if (trainerId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var list = await client.GetFromJsonAsync<List<UserLookupVm>>(
			$"/api/Users/assigned-to-trainer/{trainerId}"
		) ?? new List<UserLookupVm>();

		return View(list);
	}
}
