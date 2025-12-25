using Microsoft.AspNetCore.Mvc;
using DigitalHealthTracker.Data;

namespace DigitalHealthTracker.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DebugController : ControllerBase
	{
		[HttpGet("dbpath")]
		public IActionResult GetDbPath()
			=> Ok(new { DbPath = DbPaths.GetDbFilePath() });
	}
}
