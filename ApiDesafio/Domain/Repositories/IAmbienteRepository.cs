namespace ApiDesafio.Domain.Repositories;

using ApiDesafio.Domain.Entities;

public interface IAmbienteRepository
{
    Task<IEnumerable<Ambiente>> GetAllAsync();
    Task AddAsync(Ambiente ambiente);

    Task<Ambiente?> GetByIdAsync(int id);

    Task<bool> ExistsWithPropertiesButDifferentIdAsync<T>(
   Dictionary<string, object> propertyValuePairs) where T : class;

    Task<Ambiente?> GetByNameAsync(string nomeUnico);
}
