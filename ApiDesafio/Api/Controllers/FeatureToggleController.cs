using ApiDesafio.Domain.Entities;
using ApiDesafio.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiDesafio.API.Controllers;


[ApiController]
[Route("api/featureToggles")]
public class FeatureTogglesController : ControllerBase
{
    private readonly IFeatureToggleService _featureToggleService;

    public FeatureTogglesController(IFeatureToggleService service)
    {

        _featureToggleService = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeatureToggle>>> GetAll()
    {
        var result = await _featureToggleService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateFeatureToggleDto toggle)
    {
        var result = await _featureToggleService.CreateFeatureToggleAsync(toggle);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _featureToggleService.GetByIdAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateFeatureToggleDto input)
    {
        var result = await _featureToggleService.Update(id, input);
        if (result == false)
            return NotFound();

        return Ok(result);
    }
}
