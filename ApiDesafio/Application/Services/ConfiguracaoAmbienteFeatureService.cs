using ApiDesafio.Domain.Entities;
using ApiDesafio.Domain.Repositories;


namespace ApiDesafio.Application.Services
{

    public interface IConfiguracaoAmbienteFeatureService
    {
        Task<ConfiguracaoAmbienteFeatureDto> CreateOrUpdateConfiguracaoAmbienteFeatureAsync(CreateConfiguracaoAmbienteFeatureDto dto, int featureToggleId, int ambienteId);
        Task<bool?> GetStatusFeatureToggleAsync(string featureName, string environmentName);
    }

    public class ConfiguracaoAmbienteFeatureService : IConfiguracaoAmbienteFeatureService
    {
        private readonly IConfiguracaoAmbienteFeatureRepository _repository;
        private readonly IFeatureToggleRepository _featureToggleRepository;
        private readonly IAmbienteRepository _ambienteRepository;

        public ConfiguracaoAmbienteFeatureService(
            IConfiguracaoAmbienteFeatureRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IAmbienteRepository ambienteRepository
            )
        {
            _repository = repository;
            _featureToggleRepository = featureToggleRepository;
            _ambienteRepository = ambienteRepository;
        }

        public async Task CreateOrUpdateValidate(int featureToggleId, int ambienteId)
        {
            var feature = await _featureToggleRepository.GetByIdAsync(featureToggleId);
            if (feature == null)
                throw new InvalidOperationException("Nenhum FeatureToggle encontrado com o identificador informado. Verifique se o FeatureToggle foi criado.");

            var ambiente = await _ambienteRepository.GetByIdAsync(ambienteId);
            if (ambiente == null)
                throw new InvalidOperationException("Nenhum Ambiente encontrado com o identificador informado. Verifique se o Ambiente foi criado.");
        }

        public async Task<ConfiguracaoAmbienteFeatureDto> CreateOrUpdateConfiguracaoAmbienteFeatureAsync(CreateConfiguracaoAmbienteFeatureDto dto, int featureToggleId, int ambienteId)
        {
            await CreateOrUpdateValidate(featureToggleId, ambienteId);
            var configExists = await _repository.GetByFeatureToggleAndAmbienteAsync(featureToggleId, ambienteId);
            if (configExists == null)
            {
                var config = new ConfiguracaoAmbienteFeature
                {
                    AmbienteId = ambienteId,
                    FeatureToggleId = featureToggleId,
                    AtivoNesteAmbiente = dto.AtivoNesteAmbiente
                };
                await _repository.AddAsync(config);
                return new ConfiguracaoAmbienteFeatureDto
                {
                    Id = configExists?.Id ?? 0,
                    AmbienteId = config.AmbienteId,
                    FeatureToggleId = config.FeatureToggleId,
                    AtivoNesteAmbiente = config.AtivoNesteAmbiente
                };
            }
            else
            {
                configExists.AtivoNesteAmbiente = dto.AtivoNesteAmbiente;

                await _repository.UpdateAsync(configExists);
                return new ConfiguracaoAmbienteFeatureDto
                {
                    Id = configExists.Id,
                    AmbienteId = configExists.AmbienteId,
                    FeatureToggleId = configExists.FeatureToggleId,
                    AtivoNesteAmbiente = configExists.AtivoNesteAmbiente
                };
            }
        }


        public async Task<ConfiguracaoAmbienteFeatureDto?> GetByFeatureToggleAndAmbienteAsync(int featureToggleId, int ambienteId)
        {
            var config = await _repository.GetByFeatureToggleAndAmbienteAsync(featureToggleId, ambienteId);
            if (config == null)
            {
                return null;
            }
            else
            {
                return new ConfiguracaoAmbienteFeatureDto
                {
                    Id = config.Id,
                    AmbienteId = config.AmbienteId,
                    FeatureToggleId = config.FeatureToggleId,
                    AtivoNesteAmbiente = config.AtivoNesteAmbiente
                };
            }
        }

        public async Task<bool?> GetStatusFeatureToggleAsync(string featureName, string environmentName)
        {

            var feature = await _featureToggleRepository.GetByNameAsync(featureName);
            if (feature == null)
                return null;

            var ambiente = await _ambienteRepository.GetByNameAsync(environmentName);
            if (ambiente == null)
                return null;

            var config = await GetByFeatureToggleAndAmbienteAsync(feature.Id, ambiente.Id);

            return config?.AtivoNesteAmbiente ?? feature.AtivoGlobalmente;
        }
    }
}