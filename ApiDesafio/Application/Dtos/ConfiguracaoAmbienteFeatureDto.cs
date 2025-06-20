public class ConfiguracaoAmbienteFeatureDto
{
    public int Id { get; set; }
    public bool AtivoNesteAmbiente { get; set; } = false;

    public int AmbienteId { get; set; }

    public int FeatureToggleId { get; set; }
}