
using ApiDesafio.Application.Services;
using ApiDesafio.Domain.Repositories;
using ApiDesafio.Infrastructure.Repositories;

namespace ApiDesafio.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Serviços da camada de aplicação
            services.AddScoped<IFeatureToggleService, FeatureToggleService>();
            services.AddScoped<IAmbienteService, AmbienteService>();

            // Repositórios (Domain ↔ Infra)
            services.AddScoped<IFeatureToggleRepository, FeatureToggleRepository>();
            services.AddScoped<IAmbienteRepository, AmbienteRepository>();

            return services;
        }
    }
}
