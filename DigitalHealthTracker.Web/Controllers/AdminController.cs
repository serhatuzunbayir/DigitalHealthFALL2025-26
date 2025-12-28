using Microsoft.AspNetCore.Mvc;
using DigitalHealthTracker.Web.Filters;

namespace DigitalHealthTracker.Web.Controllers;

[RequireRole("Admin")]
public class AdminController : Controller
{
	[HttpGet("/Admin")]
	public IActionResult Index()
	{
		ViewBag.DisplayName = HttpContext.Session.GetString("DisplayName") ?? "Admin";
		return View();
	}
}
