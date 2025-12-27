using DigitalHealthTracker.Contracts.Auth;
using DigitalHealthTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DigitalHealthTracker.Web.Controllers;

public class AccountController : Controller
{
	private readonly IHttpClientFactory _http;
	public AccountController(IHttpClientFactory http) { _http = http; }

	[HttpGet]
	public IActionResult Login()
	{
		return View(new LoginVm());
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginVm vm)
	{
		if (!ModelState.IsValid)
			return View(vm);

		try
		{
			var client = _http.CreateClient("api");

			var req = new LoginRequestDto
			{
				Phone = vm.Phone,
				Password = vm.Password,
				Role = vm.Role
			};

			var resp = await client.PostAsJsonAsync("/api/Auth/login", req);

			if (resp.StatusCode == HttpStatusCode.Unauthorized)
			{
				vm.Error = "Invalid phone or password.";
				return View(vm);
			}

			if (resp.StatusCode == HttpStatusCode.Forbidden)
			{
				vm.Error = "Trainer account is not approved yet.";
				return View(vm);
			}

			if (!resp.IsSuccessStatusCode)
			{
				vm.Error = $"Login failed ({(int)resp.StatusCode}).";
				return View(vm);
			}

			var data = await resp.Content.ReadFromJsonAsync<LoginResponseDto>();
			if (data is null)
			{
				vm.Error = "Login response is empty.";
				return View(vm);
			}

			HttpContext.Session.SetInt32("UserId", data.Id);
			HttpContext.Session.SetString("Role", data.Role);
			HttpContext.Session.SetString("DisplayName", data.DisplayName);

			var role = data.Role;

			if (role != "Admin" && role != "Trainer" && role != "User")
			{
				vm.Error = "Invalid role.";
				return View(vm);
			}

			return Redirect("/" + role);
		}
		catch
		{
			vm.Error = "Cannot reach the API. Is the API running?";
			return View(vm);
		}
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Logout()
	{
		HttpContext.Session.Clear();
		return RedirectToAction("Login");
	}

	public IActionResult Denied()
	{
		return View();
	}
}
