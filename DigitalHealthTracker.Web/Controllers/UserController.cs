using System.Net.Http.Json;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalHealthTracker.Web.Controllers;

[RequireRole("User")]
public class UserController : Controller
{
	[HttpGet("/User/Profile")]
	public async Task<IActionResult> Profile()
	{
		var userId = HttpContext.Session.GetInt32("UserId");
		if (userId is null) return RedirectToAction("Login", "Account");

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var apiUser = await client.GetFromJsonAsync<ApiUserDto>($"/api/Users/{userId}");
		if (apiUser is null)
			return View(new UserProfileVm { Error = "User data could not be loaded." });

		var vm = new UserProfileVm
		{
			Id = apiUser.Id,
			Name = apiUser.Name ?? "",
			Surname = apiUser.Surname ?? "",
			Phone = apiUser.Phone ?? "",
			Email = apiUser.Email,
			HeightCm = apiUser.HeightCm ?? 0,
			WeightKg = apiUser.WeightKg ?? 0,
			BirthYear = apiUser.BirthYear ?? 0,
			MedicalRecord = apiUser.MedicalRecord
		};

		return View(vm);
	}

	[HttpPost("/User/Profile")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Profile(UserProfileVm vm)
	{
		var userId = HttpContext.Session.GetInt32("UserId");
		if (userId is null) return RedirectToAction("Login", "Account");

		if (!ModelState.IsValid)
			return View(vm);

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		// mevcut user'ı çek
		var existing = await client.GetFromJsonAsync<ApiUserDto>($"/api/Users/{userId}");
		if (existing is null)
		{
			vm.Error = "User data could not be loaded.";
			return View(vm);
		}

		// edit alanlarını yaz
		existing.Name = vm.Name;
		existing.Surname = vm.Surname;
		existing.Phone = vm.Phone;
		existing.Email = string.IsNullOrWhiteSpace(vm.Email) ? null : vm.Email;

		existing.HeightCm = vm.HeightCm;
		existing.WeightKg = vm.WeightKg;

		existing.BirthYear = vm.BirthYear;
		existing.MedicalRecord = string.IsNullOrWhiteSpace(vm.MedicalRecord) ? null : vm.MedicalRecord;

		var resp = await client.PutAsJsonAsync($"/api/Users/{userId}", existing);
		var body = await resp.Content.ReadAsStringAsync();

		if (!resp.IsSuccessStatusCode)
		{
			vm.Error = $"Update failed ({(int)resp.StatusCode}): {body}";
			return View(vm);
		}

		TempData["UserMsg"] = "Profile updated.";
		return Redirect("/User/Profile");
	}

	// API User şemasından minimum alanlar
	private class ApiUserDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? Phone { get; set; }
		public string? Email { get; set; }
		public double? HeightCm { get; set; }
		public double? WeightKg { get; set; }
		public int? BirthYear { get; set; }
		public string? MedicalRecord { get; set; }
	}
}
