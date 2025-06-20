using Microsoft.EntityFrameworkCore;
using ApiDesafio.Domain.Entities;
using ApiDesafio.Domain.Repositories;
using ApiDesafio.Infrastructure.Data.Context;

namespace ApiDesafio.Infrastructure.Repositories;


public class ConfiguracaoAmbienteFeatureRepository : IConfiguracaoAmbienteFeatureRepository
{
    private readonly AppDbContext _context;

    public ConfiguracaoAmbienteFeatureRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ConfiguracaoAmbienteFeature configuracaoAmbienteFeature)
    {
        _context.ConfiguracaoAmbienteFeature.Add(configuracaoAmbienteFeature);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ConfiguracaoAmbienteFeature configuracaoAmbienteFeature)
    {
        await _context.SaveChangesAsync();
    }


    public async Task<ConfiguracaoAmbienteFeature?> GetByFeatureToggleAndAmbienteAsync(int featureToggleId, int ambienteId)
    {
        return await _context.ConfiguracaoAmbienteFeature
            .FirstOrDefaultAsync(c =>
                c.FeatureToggleId == featureToggleId &&
                c.AmbienteId == ambienteId);
    }
}
