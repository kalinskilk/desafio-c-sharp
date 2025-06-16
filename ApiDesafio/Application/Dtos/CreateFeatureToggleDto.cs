public class CreateFeatureToggleDto
{
    public string NomeUnico { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public bool AtivoGlobalmente { get; set; }
}