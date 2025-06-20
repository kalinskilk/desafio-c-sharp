using ApiDesafio.Domain.Entities;
using ApiDesafio.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiDesafio.API.Controllers;


[ApiController]
[Route("api/featureToggles")]
public class FeatureTogglesController : ControllerBase
{
    private readonly IFeatureToggleService _featureToggleService;

    private readonly IConfiguracaoAmbienteFeatureService _configuracaoAmbienteFeatureService;

    public FeatureTogglesController(IFeatureToggleService service, IConfiguracaoAmbienteFeatureService configuracaoAmbienteFeatureService)
    {

        _featureToggleService = service;
        _configuracaoAmbienteFeatureService = configuracaoAmbienteFeatureService;
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

    [HttpPost("{featureToggleId}/ambientes/{ambienteId}/config")]
    public async Task<IActionResult> DefinirConfiguracaoAmbiente(
    int featureToggleId,
    int ambienteId,
    [FromBody] CreateConfiguracaoAmbienteFeatureDto dto)
    {
        var result = await _configuracaoAmbienteFeatureService.CreateOrUpdateConfiguracaoAmbienteFeatureAsync(dto, featureToggleId, ambienteId);

        if (result == null)
            return NotFound("FeatureToggle ou Ambiente não encontrado.");

        return Ok(result);
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetStatus(
    [FromQuery] string featureName,
    [FromQuery] string environmentName)
    {
        var ativo = await _configuracaoAmbienteFeatureService
            .GetStatusFeatureToggleAsync(featureName, environmentName);

        return Ok(new { ativo });
    }
}
