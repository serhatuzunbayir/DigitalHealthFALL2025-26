using System.Net.Http.Json;
using DigitalHealthTracker.Web.Filters;
using DigitalHealthTracker.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHealthTracker.Web.Controllers;

[RequireRole("Admin")]
public class AdminTrainersController : Controller
{
	// =========================
	// LIST
	// GET /Admin/Trainers
	// =========================
	[HttpGet("/Admin/Trainers")]
	public async Task<IActionResult> Index()
	{
		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var list = await client.GetFromJsonAsync<List<AdminTrainerRowVm>>(
			"/api/Admin/trainers"
		) ?? new();

		return View(list);
	}

	// =========================
	// EDIT (GET)
	// =========================
	[HttpGet("/Admin/Trainers/Edit/{id:int}")]
	public async Task<IActionResult> Edit(int id)
	{
		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var vm = await client.GetFromJsonAsync<AdminTrainerEditVm>(
			$"/api/Admin/trainers/{id}"
		);

		if (vm == null)
			return NotFound();

		return View(vm);
	}

	// =========================
	// EDIT (POST)
	// =========================
	[HttpPost("/Admin/Trainers/Edit/{id:int}")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(AdminTrainerEditVm vm)
	{
		if (!ModelState.IsValid)
			return View(vm);

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var resp = await client.PutAsJsonAsync(
			$"/api/Admin/trainers/{vm.Id}",
			vm
		);

		if (!resp.IsSuccessStatusCode)
		{
			vm.Error = await resp.Content.ReadAsStringAsync();
			return View(vm);
		}

		return RedirectToAction(nameof(Index));
	}

	// =========================
	// APPROVE / UNAPPROVE
	// =========================
	[HttpPost("/Admin/Trainers/ToggleApproval/{id:int}")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ToggleApproval(int id)
	{
		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		await client.PutAsync(
			$"/api/Admin/trainers/{id}/toggle-approval",
			null
		);

		return RedirectToAction(nameof(Index));
	}

	// =========================
	// DELETE
	// =========================
	[HttpPost("/Admin/Trainers/Delete/{id:int}")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(int id)
	{
		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		await client.DeleteAsync($"/api/Admin/trainers/{id}");

		return RedirectToAction(nameof(Index));
	}
}
