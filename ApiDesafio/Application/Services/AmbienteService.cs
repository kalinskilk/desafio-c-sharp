using ApiDesafio.Domain.Entities;
using ApiDesafio.Domain.Repositories;

namespace ApiDesafio.Application.Services
{
    public interface IAmbienteService
    {
        Task<AmbienteDto> CreateAmbienteAsync(CreateAmbienteDto dto);

        Task<IEnumerable<Ambiente>> GetAllAsync();

        Task<AmbienteDto?> GetByIdAsync(int id);
    }

    public class AmbienteService : IAmbienteService
    {
        private readonly IAmbienteRepository _repository;

        public AmbienteService(IAmbienteRepository repository)
        {
            _repository = repository;
        }

        private async Task CreateValidate(string NomeUnico)
        {

            var filtros = new Dictionary<string, object>{
              { "NomeUnico", NomeUnico }
              };

            var has = await _repository.ExistsWithPropertiesButDifferentIdAsync<Ambiente>(filtros);
            if (has)
            {
                throw new InvalidOperationException("Já existe outro Ambiente com essa descrição.");
            }
        }

        public async Task<AmbienteDto> CreateAmbienteAsync(CreateAmbienteDto dto)
        {
            await CreateValidate(dto.NomeUnico);

            var entity = new Ambiente
            {
                NomeUnico = dto.NomeUnico,
            };

            await _repository.AddAsync(entity);

            return new AmbienteDto
            {
                Id = entity.Id,
                NomeUnico = entity.NomeUnico
            };
        }

        public async Task<AmbienteDto?> GetByIdAsync(int id)
        {
            var ambiente = await _repository.GetByIdAsync(id);
            if (ambiente == null)
            {
                return null;
            }
            else
            {
                return new AmbienteDto
                {
                    Id = ambiente.Id,
                    NomeUnico = ambiente.NomeUnico
                };
            }
        }

        public async Task<IEnumerable<Ambiente>> GetAllAsync() => await _repository.GetAllAsync();
    }
}


