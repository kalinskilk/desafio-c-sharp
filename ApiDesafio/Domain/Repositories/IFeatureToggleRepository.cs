namespace ApiDesafio.Domain.Repositories;

using ApiDesafio.Domain.Entities;

public interface IFeatureToggleRepository
{
    Task<IEnumerable<FeatureToggle>> GetAllAsync();
    Task<FeatureToggle?> GetByIdAsync(int id);
    Task AddAsync(FeatureToggle toggle);
    Task UpdateAsync(FeatureToggle toggle);

    Task<bool> ExistsWithPropertiesButDifferentIdAsync<T>(
    string NomeUnico, int id) where T : class;

    Task<bool> ExistsWithSameNomeUnicoButDifferent(string NomeUnico);
    Task<FeatureToggle?> GetByNameAsync(string nomeUnico);
}
