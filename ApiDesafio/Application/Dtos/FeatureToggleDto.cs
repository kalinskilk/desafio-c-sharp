public class FeatureToggleDto
{
    public int Id { get; set; }
    public string NomeUnico { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public bool AtivoGlobalmente { get; set; }
}