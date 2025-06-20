using ApiDesafio.Domain.Entities;
using ApiDesafio.Domain.Repositories;

namespace ApiDesafio.Application.Services
{
    public interface IFeatureToggleService
    {
        Task<FeatureToggleDto> CreateFeatureToggleAsync(CreateFeatureToggleDto dto);
        Task<FeatureToggleDto?> GetByIdAsync(int id);
        Task<bool> Update(int id, UpdateFeatureToggleDto input);

        Task<IEnumerable<FeatureToggle>> GetAllAsync();
    }

    public class FeatureToggleService : IFeatureToggleService
    {
        private readonly IFeatureToggleRepository _repository;

        public FeatureToggleService(IFeatureToggleRepository repository)
        {
            _repository = repository;
        }


        public async Task<FeatureToggleDto?> GetByIdAsync(int id)
        {
            var toggle = await _repository.GetByIdAsync(id);
            if (toggle == null)
            {
                return null;
            }
            else
            {
                return new FeatureToggleDto
                {
                    Id = toggle.Id,
                    NomeUnico = toggle.NomeUnico,
                    Descricao = toggle.Descricao,
                    AtivoGlobalmente = toggle.AtivoGlobalmente
                };
            }

        }

        private async Task CreateValidate(string NomeUnico)
        {
            var has = await _repository.ExistsWithSameNomeUnicoButDifferent(NomeUnico);
            if (has)
            {
                throw new InvalidOperationException("Já existe outra FeatureToggle com essa descrição.");
            }
        }

        public async Task<FeatureToggleDto> CreateFeatureToggleAsync(CreateFeatureToggleDto dto)
        {
            await CreateValidate(dto.NomeUnico);

            var entity = new FeatureToggle
            {
                NomeUnico = dto.NomeUnico,
                Descricao = dto.Descricao,
                AtivoGlobalmente = dto.AtivoGlobalmente
            };

            await _repository.AddAsync(entity);

            return new FeatureToggleDto
            {
                Id = entity.Id,
                NomeUnico = entity.NomeUnico,
                Descricao = entity.Descricao,
                AtivoGlobalmente = entity.AtivoGlobalmente
            };
        }


        private async Task UpdateValidate(string NomeUnico, int id)
        {
            var has = await _repository.ExistsWithPropertiesButDifferentIdAsync<FeatureToggle>(NomeUnico, id);
            if (has)
            {
                throw new InvalidOperationException("Já existe outra FeatureToggle com essa descrição.");
            }
        }

        public async Task<bool> Update(int id, UpdateFeatureToggleDto input)
        {
            await UpdateValidate(input.NomeUnico, id);

            var toggle = await _repository.GetByIdAsync(id);
            if (toggle == null) return false;

            toggle.NomeUnico = input.NomeUnico;
            toggle.Descricao = input.Descricao;
            toggle.AtivoGlobalmente = input.AtivoGlobalmente;

            await _repository.UpdateAsync(toggle);
            return true;
        }

        public async Task<IEnumerable<FeatureToggle>> GetAllAsync() => await _repository.GetAllAsync();
    }
}


