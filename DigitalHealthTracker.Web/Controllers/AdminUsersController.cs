using System.Net;
using System.Net.Http.Json;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHealthTracker.Web.Controllers;

[RequireRole("Admin")]
public class AdminUsersController : Controller
{
	// =========================
	// LIST
	// GET /Admin/Users
	// =========================
	[HttpGet("/Admin/Users")]
	public async Task<IActionResult> Index()
	{
		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var list = await client.GetFromJsonAsync<List<ApiUserDto>>("/api/Users")
				   ?? new List<ApiUserDto>();

		var vm = list.Select(u => new AdminUserRowVm
		{
			Id = u.Id,
			Name = u.Name ?? "",
			Surname = u.Surname ?? "",
			Phone = u.Phone ?? "",
			Email = u.Email,
			HeightCm = u.HeightCm,
			WeightKg = u.WeightKg,
			BirthYear = u.BirthYear
		}).ToList();

		return View(vm);
	}

	// =========================
	// EDIT GET
	// GET /Admin/Users/Edit/{id}
	// =========================
	[HttpGet("/Admin/Users/Edit/{id:int}")]
	public async Task<IActionResult> Edit(int id)
	{
		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var u = await client.GetFromJsonAsync<ApiUserDto>($"/api/Users/{id}");
		if (u is null)
		{
			TempData["Error"] = "User not found.";
			return RedirectToAction(nameof(Index));
		}

		var vm = new AdminUserEditVm
		{
			Id = u.Id,
			Name = u.Name ?? "",
			Surname = u.Surname ?? "",
			Phone = u.Phone ?? "",
			Email = u.Email,
			HeightCm = u.HeightCm ?? 0,
			WeightKg = u.WeightKg ?? 0,
			BirthYear = u.BirthYear ?? 0,
			MedicalRecord = u.MedicalRecord
		};

		return View(vm);
	}

	// =========================
	// EDIT POST
	// POST /Admin/Users/Edit/{id}
	// =========================
	[HttpPost("/Admin/Users/Edit/{id:int}")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, AdminUserEditVm vm)
	{
		if (id != vm.Id) vm.Id = id;

		if (!ModelState.IsValid)
			return View(vm);

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		// API tarafında "Email required" olabiliyor → boş bırakılırsa "" gönderiyoruz
		var payload = new
		{
			id = vm.Id,
			name = vm.Name,
			surname = vm.Surname,
			phone = vm.Phone,
			email = string.IsNullOrWhiteSpace(vm.Email) ? "" : vm.Email,
			heightCm = vm.HeightCm,
			weightKg = vm.WeightKg,
			birthYear = vm.BirthYear,
			medicalRecord = string.IsNullOrWhiteSpace(vm.MedicalRecord) ? "" : vm.MedicalRecord
		};

		var resp = await client.PutAsJsonAsync($"/api/Users/{id}", payload);
		var body = await resp.Content.ReadAsStringAsync();

		if (!resp.IsSuccessStatusCode)
		{
			vm.Error = resp.StatusCode == HttpStatusCode.BadRequest && !string.IsNullOrWhiteSpace(body)
				? body
				: $"Update failed (HTTP {(int)resp.StatusCode}). {body}";
			return View(vm);
		}

		TempData["Success"] = "User updated.";
		return RedirectToAction(nameof(Index));
	}

	// =========================
	// DELETE
	// POST /Admin/Users/Delete/{id}
	// =========================
	[HttpPost("/Admin/Users/Delete/{id:int}")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(int id)
	{
		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var resp = await client.DeleteAsync($"/api/Users/{id}");

		if (resp.IsSuccessStatusCode)
		{
			TempData["Success"] = "User deleted.";
			return RedirectToAction(nameof(Index));
		}

		var body = await resp.Content.ReadAsStringAsync();
		TempData["Error"] = $"Delete failed (HTTP {(int)resp.StatusCode}). {body}";
		return RedirectToAction(nameof(Index));
	}

	// =========================
	// local API DTO
	// =========================
	private sealed class ApiUserDto
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
