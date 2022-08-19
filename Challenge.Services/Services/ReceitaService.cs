using AutoMapper;
using Challenge.Domain;
using Challenge.Infrastructure.Interfaces;
using Challenge.Services.DTO;
using Challenge.Services.Interfaces;

namespace Challenge.Services.Services;

public class ReceitaService : IReceitaService
{
    private readonly IMapper _mapper;
    private readonly IReceitasRepository _receitasRepository;
    public ReceitaService(IReceitasRepository receitasRepository, IMapper mapper)
    {
        _receitasRepository = receitasRepository;
        _mapper = mapper;
    }

    public async Task<ReceitasDto> CreateAsync(ReceitasDto receitasDto)
    {
        var entityDesc = await _receitasRepository.GetByDescricao(receitasDto.Descricao);
        if (entityDesc != default)
        {
            var entityMes = await _receitasRepository.GetByMes(receitasDto.Data.Month);
            if (entityMes != default)
                throw new DomainException("Não é possível existir duas receitas iguais no mesmo mês.");
        }

        var receita = _mapper.Map<Receitas>(receitasDto);
        receita.Validate();

        var receitaCreated = await _receitasRepository.Create(receita);
        return _mapper.Map<ReceitasDto>(receitaCreated);
    }

    public async Task<ReceitasDto> UpdateAsync(ReceitasDto receitasDto)
    {
        var receitaExist = await _receitasRepository.Get(receitasDto.Id);
        if (receitaExist == null)
            throw new ServiceException("Receita não encontrada!");

        var entityDesc = await _receitasRepository.GetByDescricao(receitasDto.Descricao);
        if (entityDesc != default)
        {
            var entityMes = await _receitasRepository.GetByMes(receitasDto.Data.Month);
            if (entityMes != default)
                throw new DomainException("Não é possível existir duas receitas iguais no mesmo mês.");
        }

        var receita = _mapper.Map<Receitas>(receitasDto);
        receita.Validate();

        var receitaUpdated = await _receitasRepository.Update(receita);
        return _mapper.Map<ReceitasDto>(receitaUpdated);
    }

    public async Task RemoveAsync(long id)
    {
        var receita = await _receitasRepository.Get(id);
        if (receita is null)
            throw new ServiceException("Nenhuma receita encontrada para remoção");
        
        await _receitasRepository.Remove(id);
    }

    public async Task<ReceitasDto> GetAsync(long id)
    {
        var receita = await _receitasRepository.Get(id);
        if (receita is null)
            throw new ServiceException("Nenhuma receita encontrada.");

        return _mapper.Map<ReceitasDto>(receita);
    }

    public async Task<List<ReceitasDto>> GetAllAsync()
    {
        var allReceitas =  await _receitasRepository.GetAll();
        return _mapper.Map<List<ReceitasDto>>(allReceitas);
    }

    public async Task<List<ReceitasDto>> SearchByDescricaoAsync(string descricao)
    {
        var allReceitas = await _receitasRepository.SearchByDescricao(descricao);
        return _mapper.Map<List<ReceitasDto>>(allReceitas);
    }

    public async Task<List<ReceitasDto>> GetByMesAsync(int ano, int mes)
    {
        var allReceitas = _receitasRepository.GetAll().Result;
        var finalResult = await Task.Run(() =>
        {
            return allReceitas.Where(x => x.Data.Year == ano && x.Data.Month == mes).ToList();
        });

        return _mapper.Map<List<ReceitasDto>>(finalResult);
    }
}