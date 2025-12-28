using Microsoft.AspNetCore.Mvc;

namespace DigitalHealthTracker.Web.Controllers;

public class HomeController : Controller
{
	// Root: https://localhost:7086/
	[HttpGet("/")]
	public IActionResult Index()
	{
		var userId = HttpContext.Session.GetInt32("UserId");
		var role = HttpContext.Session.GetString("Role");

		// Login değilse -> Login sayfasına
		if (userId is null || string.IsNullOrWhiteSpace(role))
			return RedirectToAction("Login", "Account");

		// Logindeyse -> role dashboard
		return role switch
		{
			"Admin" => Redirect("/Admin"),
			"Trainer" => Redirect("/Trainer"),
			"User" => Redirect("/User"),
			_ => RedirectToAction("Login", "Account")
		};
	}
}
