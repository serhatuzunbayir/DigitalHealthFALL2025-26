using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DigitalHealthTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHealthTracker.Web.Controllers;

public class AccountController : Controller
{
	// =========================
	// LOGIN
	// =========================
	[HttpGet("/Account/Login")]
	public IActionResult Login()
	{
		return View(new LoginVm());
	}

	[HttpPost("/Account/Login")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginVm vm)
	{
		if (!ModelState.IsValid) return View(vm);

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var payload = new
		{
			phone = vm.Phone,
			password = vm.Password,
			role = vm.Role
		};

		HttpResponseMessage resp;
		string body;

		try
		{
			resp = await client.PostAsJsonAsync("/api/Auth/login", payload);
			body = await resp.Content.ReadAsStringAsync();
		}
		catch (Exception ex)
		{
			TempData["Error"] = $"API connection error: {ex.Message}";
			return View(vm);
		}

		if (!resp.IsSuccessStatusCode)
		{
			// ✅ Trainer approve bekliyor durumunu handle et
			if (resp.StatusCode == HttpStatusCode.Forbidden &&
				(body?.ToLowerInvariant().Contains("not approved") ?? false))
			{
				TempData["Error"] = "Your trainer account is pending admin approval. Please try again later.";
				return View(vm);
			}

			TempData["Error"] = string.IsNullOrWhiteSpace(body)
				? $"Login failed (HTTP {(int)resp.StatusCode})."
				: body;

			return View(vm);
		}

		if (!TryExtractLoginInfo(body, out var userId, out var role, out var displayName))
		{
			TempData["Error"] = $"Login response invalid. Raw: {body}";
			return View(vm);
		}

		HttpContext.Session.SetInt32("UserId", userId);
		HttpContext.Session.SetString("Role", role);
		HttpContext.Session.SetString("DisplayName", string.IsNullOrWhiteSpace(displayName) ? vm.Phone : displayName);

		if (role == "Admin") return Redirect("/Admin");
		if (role == "Trainer") return Redirect("/Trainer");
		return Redirect("/User");
	}

	// =========================
	// LOGOUT
	// =========================
	[HttpGet("/Account/Logout")]
	public IActionResult Logout()
	{
		HttpContext.Session.Clear();
		return RedirectToAction(nameof(Login));
	}

	// =========================
	// REGISTER PAGES
	// =========================
	[HttpGet("/Account/RegisterUser")]
	public IActionResult RegisterUser() => View(new RegisterVm());

	[HttpGet("/Account/RegisterTrainer")]
	public IActionResult RegisterTrainer() => View(new RegisterVm());

	// =========================
	// REGISTER POSTS
	// =========================
	[HttpPost("/Account/RegisterUser")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> RegisterUser(RegisterVm vm)
	{
		if (!ModelState.IsValid) return View(vm);

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var payload = new
		{
			name = vm.Name,
			surname = vm.Surname,
			phone = vm.Phone,
			email = vm.Email,
			password = vm.Password,
			role = "User"
		};

		var (ok, error) = await PostRegisterWithFallbackAsync(client, payload, "User");
		if (!ok)
		{
			vm.Error = error;
			return View(vm);
		}

		TempData["Success"] = "User registered successfully. Please login.";
		return RedirectToAction(nameof(Login));
	}

	[HttpPost("/Account/RegisterTrainer")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> RegisterTrainer(RegisterVm vm)
	{
		if (!ModelState.IsValid) return View(vm);

		var client = HttpContext.RequestServices
			.GetRequiredService<IHttpClientFactory>()
			.CreateClient("api");

		var payload = new
		{
			name = vm.Name,
			surname = vm.Surname,
			phone = vm.Phone,
			email = vm.Email,
			password = vm.Password,
			role = "Trainer"
		};

		var (ok, error) = await PostRegisterWithFallbackAsync(client, payload, "Trainer");
		if (!ok)
		{
			vm.Error = error;
			return View(vm);
		}

		TempData["Success"] = "Trainer registered. Wait for admin approval, then login.";
		return RedirectToAction(nameof(Login));
	}

	// =========================
	// REGISTER helper (fallback)
	// =========================
	private static async Task<(bool ok, string error)> PostRegisterWithFallbackAsync(
		HttpClient client,
		object payload,
		string role)
	{
		var candidates = role == "Trainer"
			? new[]
			{
				"/api/Auth/register-trainer",
				"/api/Auth/register/trainer",
				"/api/Auth/register",
				"/api/Auth/register-user",
				"/api/Auth/register/user",
			}
			: new[]
			{
				"/api/Auth/register-user",
				"/api/Auth/register/user",
				"/api/Auth/register",
				"/api/Auth/register-trainer",
				"/api/Auth/register/trainer",
			};

		foreach (var url in candidates)
		{
			var resp = await client.PostAsJsonAsync(url, payload);
			var body = await resp.Content.ReadAsStringAsync();

			if (resp.IsSuccessStatusCode)
				return (true, "");

			if (resp.StatusCode == HttpStatusCode.NotFound)
				continue;

			return (false, string.IsNullOrWhiteSpace(body)
				? $"Register failed (HTTP {(int)resp.StatusCode})."
				: body);
		}

		return (false, "Register endpoint not found (404). API Auth register route is different.");
	}

	// =========================
	// LOGIN parse helpers
	// =========================
	private static bool TryExtractLoginInfo(string json, out int userId, out string role, out string displayName)
	{
		userId = 0;
		role = "";
		displayName = "";

		if (string.IsNullOrWhiteSpace(json))
			return false;

		try
		{
			using var doc = JsonDocument.Parse(json);
			var root = doc.RootElement;

			var obj = root;
			if (TryGetProperty(root, out var dataEl, "data", "result", "payload") &&
				dataEl.ValueKind == JsonValueKind.Object)
			{
				obj = dataEl;
			}

			userId =
				TryGetInt(obj, "userId", "userid", "userID", "id", "Id", "UserId", "UserID")
				?? 0;

			role =
				TryGetString(obj, "role", "Role", "userRole", "UserRole")
				?? "";

			displayName =
				TryGetString(obj, "displayName", "DisplayName", "name", "Name", "fullName", "FullName")
				?? "";

			// ✅ SENİN DTO'DA id/role/displayName var:
			// LoginResponseDto => { Id, Role, DisplayName }
			// O yüzden bunları da deniyoruz.
			if (userId <= 0) userId = TryGetInt(obj, "Id", "ID") ?? userId;
			if (string.IsNullOrWhiteSpace(role)) role = TryGetString(obj, "Role") ?? role;
			if (string.IsNullOrWhiteSpace(displayName)) displayName = TryGetString(obj, "DisplayName") ?? displayName;

			return userId > 0 && !string.IsNullOrWhiteSpace(role);
		}
		catch
		{
			return false;
		}
	}

	private static bool TryGetProperty(JsonElement obj, out JsonElement value, params string[] names)
	{
		foreach (var prop in obj.EnumerateObject())
		{
			foreach (var n in names)
			{
				if (string.Equals(prop.Name, n, StringComparison.OrdinalIgnoreCase))
				{
					value = prop.Value;
					return true;
				}
			}
		}
		value = default;
		return false;
	}

	private static int? TryGetInt(JsonElement obj, params string[] names)
	{
		if (!TryGetProperty(obj, out var v, names)) return null;

		if (v.ValueKind == JsonValueKind.Number && v.TryGetInt32(out var n)) return n;
		if (v.ValueKind == JsonValueKind.String && int.TryParse(v.GetString(), out var s)) return s;

		return null;
	}

	private static string? TryGetString(JsonElement obj, params string[] names)
	{
		if (!TryGetProperty(obj, out var v, names)) return null;

		if (v.ValueKind == JsonValueKind.String) return v.GetString();
		if (v.ValueKind == JsonValueKind.Number) return v.GetRawText();

		return null;
	}
}
