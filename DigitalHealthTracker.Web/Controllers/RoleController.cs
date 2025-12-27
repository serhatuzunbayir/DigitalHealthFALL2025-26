using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHealthTracker.Web.Controllers;

public class RoleController : Controller
{
	// =========================
	// ADMIN
	// =========================
	[RequireRole("Admin")]
	[HttpGet("/Admin")]
	public async Task<IActionResult> Admin()
	{
		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var list = await client.GetFromJsonAsync<List<TrainerPendingVm>>("/api/Trainers/pending")
				   ?? new List<TrainerPendingVm>();

		return View(list);
	}

	[RequireRole("Admin")]
	[HttpPost("/Admin/Approve")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ApproveTrainer(int id)
	{
		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var resp = await client.PutAsync($"/api/Trainers/{id}/approve", null);

		TempData["Msg"] = resp.IsSuccessStatusCode
			? "Trainer approved."
			: $"Approve failed ({(int)resp.StatusCode}).";

		return Redirect("/Admin");
	}

	// =========================
	// TRAINER
	// =========================
	[RequireRole("Trainer")]
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

	// =========================
	// USER DASHBOARD (BURASI /User)
	// =========================
	[RequireRole("User")]
	[HttpGet("/User")]
	public async Task<IActionResult> UserDashboard()
	{
		var userId = HttpContext.Session.GetInt32("UserId");
		if (userId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		// ✅ ACTIVE LIST
		var activeList = await client.GetFromJsonAsync<List<ActiveListRowDto>>(
			$"/api/AssignedPrograms/user/{userId}/active-list"
		) ?? new List<ActiveListRowDto>();

		// ✅ ALL ASSIGNMENTS
		var rows = await client.GetFromJsonAsync<List<UserAssignedProgramRowVm>>(
			$"/api/AssignedPrograms/user/{userId}"
		) ?? new List<UserAssignedProgramRowVm>();

		// ✅ HEALTH SUMMARY
		HealthVm? health = null;
		var u = await client.GetFromJsonAsync<UserHealthDto>($"/api/Users/{userId}");

		if (u is not null)
		{
			var heightCm = u.HeightCm ?? 0;
			var weightKg = u.WeightKg ?? 0;

			double bmi = 0;
			if (heightCm > 0 && weightKg > 0)
			{
				var m = heightCm / 100.0;
				bmi = weightKg / (m * m);
			}

			string category =
				bmi <= 0 ? "-" :
				bmi < 18.5 ? "Underweight" :
				bmi < 25.0 ? "Normal" :
				bmi < 30.0 ? "Overweight" : "Obese";

			double targetWeight = 0;
			double diff = 0;

			if (heightCm > 0)
			{
				var m = heightCm / 100.0;
				targetWeight = 22.0 * (m * m);
				diff = weightKg - targetWeight;
			}

			health = new HealthVm
			{
				HeightCm = heightCm,
				WeightKg = weightKg,
				Bmi = bmi,
				Category = category,
				TargetWeightKg = targetWeight,
				WeightDiffKg = diff
			};
		}

		var vm = new UserDashboardVm
		{
			Health = health,
			AssignedPrograms = rows,
			ActiveAssignments = activeList.Select(x => new UserActiveAssignmentVm
			{
				Id = x.Id,
				WorkoutProgramId = x.WorkoutProgramId,
				ProgramTitle = x.ProgramTitle ?? "",
				TrainerName = x.TrainerName ?? ""
			}).ToList()
		};

		return View("User", vm);
	}

	// =========================
	// USER: APPROVE
	// =========================
	[RequireRole("User")]
	[HttpPost("/User/Approve")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ApproveAssignedProgram(int id)
	{
		var userId = HttpContext.Session.GetInt32("UserId");
		if (userId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var req = new HttpRequestMessage(
			HttpMethod.Put,
			$"/api/AssignedPrograms/{id}/approve?userId={userId}"
		)
		{
			Content = new StringContent("{}", Encoding.UTF8, "application/json")
		};

		var resp = await client.SendAsync(req);
		var body = await resp.Content.ReadAsStringAsync();

		TempData["UserMsg"] = resp.IsSuccessStatusCode
			? "Program approved."
			: $"Approve failed ({(int)resp.StatusCode}): {body}";

		return Redirect("/User");
	}

	// =========================
	// USER: COMPLETE WITH LOGS
	// =========================
	[RequireRole("User")]
	[HttpPost("/User/Complete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> CompleteWithLogs(int id)
	{
		var userId = HttpContext.Session.GetInt32("UserId");
		if (userId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var req = new HttpRequestMessage(
			HttpMethod.Put,
			$"/api/AssignedPrograms/{id}/complete-with-logs?userId={userId}"
		)
		{
			Content = new StringContent("{}", Encoding.UTF8, "application/json")
		};

		var resp = await client.SendAsync(req);
		var body = await resp.Content.ReadAsStringAsync();

		TempData["UserMsg"] = resp.IsSuccessStatusCode
			? "Workout completed and logs saved."
			: $"Complete failed ({(int)resp.StatusCode}): {body}";

		return Redirect("/User");
	}

	private class UserHealthDto
	{
		public double? HeightCm { get; set; }
		public double? WeightKg { get; set; }
	}

	private sealed class ActiveListRowDto
	{
		[JsonPropertyName("id")] public int Id { get; set; }
		[JsonPropertyName("workoutProgramId")] public int WorkoutProgramId { get; set; }
		[JsonPropertyName("programTitle")] public string? ProgramTitle { get; set; }
		[JsonPropertyName("trainerName")] public string? TrainerName { get; set; }
	}
}
