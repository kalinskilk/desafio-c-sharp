using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ApiDesafio.Application.Services;
using ApiDesafio.Infrastructure.Data.Context;
using ApiDesafio.Domain.Entities;
using ApiDesafio.Infrastructure.Repositories;

namespace ApiDesafio.Tests.Integration
{
    public class ConfiguracaoAmbienteFeatureServiceIntegrationTests
    {
        private AppDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new AppDbContext(options);
            context.Database.OpenConnection();     // obrigatório para SQLite em memória
            context.Database.EnsureCreated();      // cria as tabelas
            return context;
        }

        [Fact]
        public async Task DeveRetornarValorGlobal_SeNaoHouverConfiguracaoEspecifica()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();

            var feature = new FeatureToggle
            {
                NomeUnico = "FeatureY",
                AtivoGlobalmente = true,
                Descricao = "FeatureY"
            };

            var ambiente = new Ambiente
            {
                NomeUnico = "Homolog"
            };

            context.FeatureToggle.Add(feature);
            context.Ambiente.Add(ambiente);

            await context.SaveChangesAsync();

            var repoFeature = new FeatureToggleRepository(context);
            var repoAmbiente = new AmbienteRepository(context);
            var repoConfig = new ConfiguracaoAmbienteFeatureRepository(context);

            var service = new ConfiguracaoAmbienteFeatureService(
                repoConfig,
                repoFeature,
                repoAmbiente);

            // Act
            var status = await service.GetStatusFeatureToggleAsync("FeatureY", "Homolog");

            // Assert
            Assert.True(status);
        }

        [Fact]
        public async Task DeveRetornarAtivoNesteAmbiente_SeHouverConfiguracao()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();

            var feature = new FeatureToggle
            {
                NomeUnico = "FeatureX",
                AtivoGlobalmente = false,
                Descricao = "FeatureY"
            };

            var ambiente = new Ambiente
            {
                NomeUnico = "Prod"
            };

            await context.FeatureToggle.AddAsync(feature);
            await context.Ambiente.AddAsync(ambiente);
            await context.SaveChangesAsync();

            var config = new ConfiguracaoAmbienteFeature
            {
                FeatureToggleId = feature.Id,
                AmbienteId = ambiente.Id,
                AtivoNesteAmbiente = true
            };

            await context.ConfiguracaoAmbienteFeature.AddAsync(config);
            await context.SaveChangesAsync();

            var repoFeature = new FeatureToggleRepository(context);
            var repoAmbiente = new AmbienteRepository(context);
            var repoConfig = new ConfiguracaoAmbienteFeatureRepository(context);

            var service = new ConfiguracaoAmbienteFeatureService(repoConfig, repoFeature, repoAmbiente);

            // Act
            var status = await service.GetStatusFeatureToggleAsync("FeatureX", "Prod");

            // Assert
            Assert.True(status);
        }

        [Fact]
        public async Task DeveRetornarInativo_SeHouverConfiguracao()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();

            var feature = new FeatureToggle
            {
                NomeUnico = "FeatureX",
                AtivoGlobalmente = false,
                Descricao = "FeatureY"
            };

            var ambiente = new Ambiente
            {
                NomeUnico = "Prod"
            };

            await context.FeatureToggle.AddAsync(feature);
            await context.Ambiente.AddAsync(ambiente);
            await context.SaveChangesAsync();

            var config = new ConfiguracaoAmbienteFeature
            {
                FeatureToggleId = feature.Id,
                AmbienteId = ambiente.Id,
                AtivoNesteAmbiente = false
            };

            await context.ConfiguracaoAmbienteFeature.AddAsync(config);
            await context.SaveChangesAsync();

            var repoFeature = new FeatureToggleRepository(context);
            var repoAmbiente = new AmbienteRepository(context);
            var repoConfig = new ConfiguracaoAmbienteFeatureRepository(context);

            var service = new ConfiguracaoAmbienteFeatureService(repoConfig, repoFeature, repoAmbiente);

            // Act
            var status = await service.GetStatusFeatureToggleAsync("FeatureX", "Prod");

            // Assert
            Assert.False(status);
        }

        [Fact]
        public async Task DeveRetornarInativo_SeNaoHouverConfiguracao()
        {
            // Arrange
            using var context = CreateInMemoryDbContext();

            var feature = new FeatureToggle
            {
                NomeUnico = "FeatureX",
                AtivoGlobalmente = false,
                Descricao = "FeatureY"
            };

            var ambiente = new Ambiente
            {
                NomeUnico = "Prod"
            };

            await context.FeatureToggle.AddAsync(feature);
            await context.Ambiente.AddAsync(ambiente);
            await context.SaveChangesAsync();


            var repoFeature = new FeatureToggleRepository(context);
            var repoAmbiente = new AmbienteRepository(context);
            var repoConfig = new ConfiguracaoAmbienteFeatureRepository(context);

            var service = new ConfiguracaoAmbienteFeatureService(repoConfig, repoFeature, repoAmbiente);

            // Act
            var status = await service.GetStatusFeatureToggleAsync("FeatureX", "Prod");

            // Assert
            Assert.False(status);
        }
    }
}