using Microsoft.AspNetCore.Mvc;
using GymBroApi.Models;
using GymBroApi.Services;
using GymBroApi.Helpers;

namespace GymBroApi.Controllers;

[ApiController]
[Route("api/bros")]
public class BrosController : ControllerBase
{
    private readonly GymBroService _service;
    public BrosController(GymBroService service) => _service = service;

    [HttpGet]
    public ActionResult<List<GymBro>> GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public ActionResult<GymBro> GetById(int id)
    {
        var bro = _service.GetById(id);
        return bro is null ? NotFound($"No bro with id {id}. Maybe he quit.") : Ok(bro);
    }

    [HttpPost]
    public ActionResult<GymBro> Create(GymBro bro)
    {
        var created = _service.Create(bro);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public ActionResult<GymBro> Update(int id, GymBro updated)
    {
        var bro = _service.Update(id, updated);
        return bro is null ? NotFound($"Bro {id} not found. Did he skip API day too?") : Ok(bro);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);
        return deleted ? NoContent() : NotFound($"Can't delete what doesn't exist, bro.");
    }

    [HttpPost("{id}/pr")]
    public ActionResult<GymBro> LogPr(int id, PrRequest pr)
    {
        var bro = _service.LogPr(id, pr);
        return bro is null ? NotFound($"Bro {id} not found.") : Ok(bro);
    }
}