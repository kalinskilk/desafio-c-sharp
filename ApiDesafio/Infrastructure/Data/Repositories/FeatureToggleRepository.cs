using Microsoft.EntityFrameworkCore;
using ApiDesafio.Domain.Entities;
using ApiDesafio.Domain.Repositories;
using ApiDesafio.Infrastructure.Data.Context;
using System.Linq.Expressions;

namespace ApiDesafio.Infrastructure.Repositories;

public class FeatureToggleRepository : IFeatureToggleRepository
{
    private readonly AppDbContext _context;

    public FeatureToggleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FeatureToggle>> GetAllAsync() =>
        await _context.FeatureToggle.ToListAsync();

    public async Task<FeatureToggle?> GetByIdAsync(int id) =>
        await _context.FeatureToggle.FindAsync(id);

    public async Task<FeatureToggle?> GetByNameAsync(string nomeUnico)
    {
        return await _context.FeatureToggle.FirstOrDefaultAsync(f => f.NomeUnico == nomeUnico);
    }

    public async Task AddAsync(FeatureToggle toggle)
    {
        _context.FeatureToggle.Add(toggle);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FeatureToggle toggle)
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsWithSameNomeUnicoButDifferent(string NomeUnico)
    {
        return await _context.FeatureToggle
            .AnyAsync(f => f.NomeUnico == NomeUnico);
    }



    public async Task<bool> ExistsWithPropertiesButDifferentIdAsync<T>(
    Dictionary<string, object> propertyValuePairs) where T : class
    {
        var parameter = Expression.Parameter(typeof(T), "e");

        Expression? body = null;

        foreach (var kvp in propertyValuePairs)
        {
            var property = Expression.Property(parameter, kvp.Key);
            var constant = Expression.Constant(kvp.Value);
            var equals = Expression.Equal(property, constant);

            body = body == null ? equals : Expression.AndAlso(body, equals);
        }

        var lambda = Expression.Lambda<Func<T, bool>>(body!, parameter);

        return await _context.Set<T>().AnyAsync(lambda);
    }
}
