using Microsoft.EntityFrameworkCore;
using ApiDesafio.Domain.Entities;

namespace ApiDesafio.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Ambiente> Ambiente { get; set; }

        public DbSet<FeatureToggle> FeatureToggle { get; set; }

        public DbSet<ConfiguracaoAmbienteFeature> ConfiguracaoAmbienteFeature { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
