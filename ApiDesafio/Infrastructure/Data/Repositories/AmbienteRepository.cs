using Microsoft.EntityFrameworkCore;
using ApiDesafio.Domain.Entities;
using ApiDesafio.Domain.Repositories;
using ApiDesafio.Infrastructure.Data.Context;

using System.Linq.Expressions;

namespace ApiDesafio.Infrastructure.Repositories;


public class AmbienteRepository : IAmbienteRepository
{
    private readonly AppDbContext _context;

    public AmbienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Ambiente>> GetAllAsync() =>
        await _context.Ambiente.ToListAsync();

    public async Task AddAsync(Ambiente ambiente)
    {
        _context.Ambiente.Add(ambiente);
        await _context.SaveChangesAsync();
    }

    public async Task<Ambiente?> GetByIdAsync(int id) =>
       await _context.Ambiente.FindAsync(id);

    public async Task<Ambiente?> GetByNameAsync(string nomeUnico) =>
    await _context.Ambiente.FirstOrDefaultAsync(f => f.NomeUnico == nomeUnico);

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
