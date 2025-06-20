namespace ApiDesafio.Domain.Repositories;

using ApiDesafio.Domain.Entities;

public interface IConfiguracaoAmbienteFeatureRepository
{
    Task AddAsync(ConfiguracaoAmbienteFeature configuracaoAmbienteFeature);

    Task UpdateAsync(ConfiguracaoAmbienteFeature configuracaoAmbienteFeature);

    Task<ConfiguracaoAmbienteFeature?> GetByFeatureToggleAndAmbienteAsync(int featureToggleId, int ambienteId);
}
