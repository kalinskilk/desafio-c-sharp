// Application/DTOs/UpdateFeatureToggleDto.cs
public class UpdateFeatureToggleDto
{
    public string Descricao { get; set; } = null!;
    public string NomeUnico { get; set; } = null!;
    public bool AtivoGlobalmente { get; set; }
}