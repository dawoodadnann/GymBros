using Microsoft.AspNetCore.Mvc;
using GymBroApi.Services;
using GymBroApi.Helpers;

namespace GymBroApi.Controllers;

[ApiController]
[Route("api")]
public class LeaderboardController : ControllerBase
{
	private readonly GymBroService _service;
	public LeaderboardController(GymBroService service) => _service = service;

	[HttpGet("leaderboard")]
	public ActionResult GetLeaderboard() => Ok(_service.GetLeaderboard());

	[HttpGet("roast/{id1}/vs/{id2}")]
	public ActionResult Roast(int id1, int id2)
	{
		var bro1 = _service.GetById(id1);
		var bro2 = _service.GetById(id2);

		if (bro1 is null || bro2 is null)
			return NotFound("One of these bros doesn't exist. Can't roast a ghost.");

		if (bro1.TotalLifted == 0 && bro2.TotalLifted == 0)
			return BadRequest("Neither bro has logged a single PR. Log something first, cowards. 🛋️");

		return Ok(new { roast = RoastEngine.Roast(bro1, bro2) });
	}
}