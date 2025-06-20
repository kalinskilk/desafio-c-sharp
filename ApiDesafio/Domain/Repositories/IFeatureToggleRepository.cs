namespace ApiDesafio.Domain.Repositories;

using ApiDesafio.Domain.Entities;

public interface IFeatureToggleRepository
{
    Task<IEnumerable<FeatureToggle>> GetAllAsync();
    Task<FeatureToggle?> GetByIdAsync(int id);
    Task AddAsync(FeatureToggle toggle);
    Task UpdateAsync(FeatureToggle toggle);

    Task<bool> ExistsWithPropertiesButDifferentIdAsync<T>(
    Dictionary<string, object> propertyValuePairs) where T : class;

    Task<FeatureToggle?> GetByNameAsync(string nomeUnico);
}
