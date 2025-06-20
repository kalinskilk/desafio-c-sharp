using Xunit;
using Moq;

using ApiDesafio.Application.Services;
using ApiDesafio.Domain.Entities;
using ApiDesafio.Domain.Repositories;

namespace ApiDesafio.Tests.Unit
{
    public class ConfiguracaoAmbienteFeatureServiceUnitTests
    {
        private readonly Mock<IConfiguracaoAmbienteFeatureRepository> _mockConfigRepo;
        private readonly Mock<IFeatureToggleRepository> _mockFeatureRepo;
        private readonly Mock<IAmbienteRepository> _mockAmbienteRepo;
        private readonly ConfiguracaoAmbienteFeatureService _service;

        public ConfiguracaoAmbienteFeatureServiceUnitTests()
        {
            _mockConfigRepo = new Mock<IConfiguracaoAmbienteFeatureRepository>();
            _mockFeatureRepo = new Mock<IFeatureToggleRepository>();
            _mockAmbienteRepo = new Mock<IAmbienteRepository>();

            _service = new ConfiguracaoAmbienteFeatureService(
                _mockConfigRepo.Object,
                _mockFeatureRepo.Object,
                _mockAmbienteRepo.Object);
        }

        [Fact]
        public async Task DeveRetornarValorGlobal_SeNaoHouverConfiguracaoEspecifica()
        {
            // Arrange
            var feature = new FeatureToggle
            {
                Id = 1,
                NomeUnico = "FeatureY",
                AtivoGlobalmente = true,
                Descricao = "FeatureY"
            };

            var ambiente = new Ambiente
            {
                Id = 1,
                NomeUnico = "Homolog"
            };

            _mockFeatureRepo.Setup(x => x.GetByNameAsync("FeatureY"))
                .ReturnsAsync(feature);

            _mockAmbienteRepo.Setup(x => x.GetByNameAsync("Homolog"))
                .ReturnsAsync(ambiente);

            _mockConfigRepo.Setup(x => x.GetByFeatureToggleAndAmbienteAsync(1, 1))
                .ReturnsAsync((ConfiguracaoAmbienteFeature?)null);

            // Act
            var status = await _service.GetStatusFeatureToggleAsync("FeatureY", "Homolog");

            // Assert
            Assert.True(status);
        }

        [Fact]
        public async Task DeveRetornarAtivoNesteAmbiente_SeHouverConfiguracao()
        {
            // Arrange
            var feature = new FeatureToggle
            {
                Id = 1,
                NomeUnico = "FeatureX",
                AtivoGlobalmente = false,
                Descricao = "FeatureY"
            };

            var ambiente = new Ambiente
            {
                Id = 1,
                NomeUnico = "Prod"
            };

            var config = new ConfiguracaoAmbienteFeature
            {
                FeatureToggleId = 1,
                AmbienteId = 1,
                AtivoNesteAmbiente = true
            };

            _mockFeatureRepo.Setup(x => x.GetByNameAsync("FeatureX"))
                .ReturnsAsync(feature);

            _mockAmbienteRepo.Setup(x => x.GetByNameAsync("Prod"))
                .ReturnsAsync(ambiente);

            _mockConfigRepo.Setup(x => x.GetByFeatureToggleAndAmbienteAsync(1, 1))
                .ReturnsAsync(config);

            // Act
            var status = await _service.GetStatusFeatureToggleAsync("FeatureX", "Prod");

            // Assert
            Assert.True(status);
        }

        [Fact]
        public async Task DeveRetornarInativo_SeHouverConfiguracao()
        {
            // Arrange
            var feature = new FeatureToggle
            {
                Id = 1,
                NomeUnico = "FeatureX",
                AtivoGlobalmente = false,
                Descricao = "FeatureY"
            };

            var ambiente = new Ambiente
            {
                Id = 1,
                NomeUnico = "Prod"
            };

            var config = new ConfiguracaoAmbienteFeature
            {
                FeatureToggleId = 1,
                AmbienteId = 1,
                AtivoNesteAmbiente = false
            };

            _mockFeatureRepo.Setup(x => x.GetByNameAsync("FeatureX"))
                .ReturnsAsync(feature);

            _mockAmbienteRepo.Setup(x => x.GetByNameAsync("Prod"))
                .ReturnsAsync(ambiente);

            _mockConfigRepo.Setup(x => x.GetByFeatureToggleAndAmbienteAsync(1, 1))
                .ReturnsAsync(config);

            // Act
            var status = await _service.GetStatusFeatureToggleAsync("FeatureX", "Prod");

            // Assert
            Assert.False(status);
        }

        [Fact]
        public async Task DeveRetornarInativo_SeNaoHouverConfiguracao()
        {
            // Arrange
            var feature = new FeatureToggle
            {
                Id = 1,
                NomeUnico = "FeatureX",
                AtivoGlobalmente = false,
                Descricao = "FeatureY"
            };

            var ambiente = new Ambiente
            {
                Id = 1,
                NomeUnico = "Prod"
            };

            _mockFeatureRepo.Setup(x => x.GetByNameAsync("FeatureX"))
                .ReturnsAsync(feature);

            _mockAmbienteRepo.Setup(x => x.GetByNameAsync("Prod"))
                .ReturnsAsync(ambiente);

            _mockConfigRepo.Setup(x => x.GetByFeatureToggleAndAmbienteAsync(1, 1))
                .ReturnsAsync((ConfiguracaoAmbienteFeature?)null);

            // Act
            var status = await _service.GetStatusFeatureToggleAsync("FeatureX", "Prod");

            // Assert
            Assert.False(status);
        }
    }
}