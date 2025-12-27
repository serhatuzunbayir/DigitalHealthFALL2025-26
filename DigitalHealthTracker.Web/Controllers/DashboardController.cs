using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Http.Json;
using DigitalHealthTracker.Contracts.Auth;
using DigitalHealthTracker.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHealthTracker.Web.Controllers;

[Authorize]
public class DashboardController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
