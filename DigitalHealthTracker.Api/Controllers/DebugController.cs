using Microsoft.AspNetCore.Mvc;
using DigitalHealthTracker.Data;
using DigitalHealthTracker.Data.Infrastructure;

namespace DigitalHealthTracker.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DebugController : ControllerBase
	{
		[HttpGet("dbpath")]
		public IActionResult GetDbPath()
			=> Ok(new { DbPath = DbPath.GetDbFilePath() });
	}
}
