using ApiDesafio.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiDesafio.API.Controllers;

[ApiController]
[Route("api/ambiente")]
public class AmbienteController : ControllerBase
{
    private readonly IAmbienteService _ambienteService;

    public AmbienteController(IAmbienteService service)
    {
        _ambienteService = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AmbienteDto>>> GetAll()
    {
        var result = await _ambienteService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateAmbienteDto ambiente)
    {
        var result = await _ambienteService.CreateAmbienteAsync(ambiente);
        var data = await _ambienteService.GetByIdAsync(result.Id);
        return CreatedAtAction(null, new { id = result.Id }, data);
    }
}
