
using Microsoft.AspNetCore.Mvc;
using ApiDesafio.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class FeatureTogglesController : ControllerBase
{
    private static List<FeatureToggle> _features = new(); // Simulando DB
    private static int _nextId = 1;

    [HttpPost]
    public IActionResult Create([FromBody] CreateFeatureToggleDto dto)
    {
        if (_features.Any(f => f.NomeUnico == dto.NomeUnico))
            return Conflict("Nome único já existe.");

        var feature = new FeatureToggle
        {
            Id = _nextId++,
            NomeUnico = dto.NomeUnico,
            Descricao = dto.Descricao,
            AtivoGlobalmente = dto.AtivoGlobalmente
        };

        _features.Add(feature);
        return CreatedAtAction(nameof(GetAll), new { id = feature.Id }, feature);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_features);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateFeatureToggleDto dto)
    {
        var feature = _features.FirstOrDefault(f => f.Id == id);
        if (feature == null) return NotFound();

        feature.Descricao = dto.Descricao;
        feature.AtivoGlobalmente = dto.AtivoGlobalmente;

        return NoContent();
    }

    /* [HttpPost("{featureToggleId}/ambientes/{ambienteId}/config")]
    public IActionResult UpdateAmbienteConfig(int featureToggleId, string ambienteId, [FromBody] UpdateAmbienteConfigDto dto)
    {
        var feature = _features.FirstOrDefault(f => f.Id == featureToggleId);
        if (feature == null) return NotFound();

        var config = feature.ConfiguracoesAmbiente
            .FirstOrDefault(c => c.AmbienteNomeUnico == ambienteId);

        if (config == null)
        {
            config = new FeatureToggleAmbienteConfig
            {
                Id = feature.ConfiguracoesAmbiente.Count + 1,
                FeatureToggleId = feature.Id,
                AmbienteNomeUnico = ambienteId,
                AtivoNesteAmbiente = dto.AtivoNesteAmbiente,
                FeatureToggle = feature
            };
            feature.ConfiguracoesAmbiente.Add(config);
        }
        else
        {
            config.AtivoNesteAmbiente = dto.AtivoNesteAmbiente;
        }

        return Ok(config);
    } */

    /*   [HttpGet("status")]
      public IActionResult GetStatus([FromQuery] string featureName, [FromQuery] string environmentName)
      {
          var feature = _features.FirstOrDefault(f => f.NomeUnico == featureName);
          if (feature == null) return NotFound("Feature não encontrada.");

          var config = feature.ConfiguracoesAmbiente
              .FirstOrDefault(c => c.AmbienteNomeUnico == environmentName);

          var status = config?.AtivoNesteAmbiente ?? feature.AtivoGlobalmente;
          return Ok(new { ativo = status });
      } */
}
