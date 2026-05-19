using Microsoft.AspNetCore.Mvc;
using GymBroApi.Models;
using GymBroApi.Services;

namespace GymBroApi.Controllers;

[ApiController]
[Route("api/bros")]
public class BrosController : ControllerBase
{
    private readonly GymBroService _service;
    public BrosController(GymBroService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<List<GymBro>>> GetAll() =>
        Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<ActionResult<GymBro>> GetById(int id)
    {
        var bro = await _service.GetById(id);
        return bro is null ? NotFound($"No bro with id {id}. Maybe he quit.") : Ok(bro);
    }

    [HttpPost]
    public async Task<ActionResult<GymBro>> Create(GymBro bro)
    {
        var created = await _service.Create(bro);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GymBro>> Update(int id, GymBro updated)
    {
        var bro = await _service.Update(id, updated);
        return bro is null ? NotFound($"Bro {id} not found.") : Ok(bro);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _service.Delete(id);
        return deleted ? NoContent() : NotFound("Can't delete what doesn't exist, bro.");
    }

    [HttpPost("{id}/pr")]
    public async Task<ActionResult<GymBro>> LogPr(int id, PrRequest pr)
    {
        var bro = await _service.LogPr(id, pr);
        return bro is null ? NotFound($"Bro {id} not found.") : Ok(bro);
    }
}