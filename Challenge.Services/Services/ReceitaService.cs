using AutoMapper;
using Challenge.Core;
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

    public async Task<ReceitasDTO> CreateAsync(ReceitasDTO receitasDto)
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
        return _mapper.Map<ReceitasDTO>(receitaCreated);
    }

    public async Task<ReceitasDTO> UpdateAsync(ReceitasDTO receitasDto)
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
        return _mapper.Map<ReceitasDTO>(receitaUpdated);
    }

    public async Task RemoveAsync(long id)
    {
        var receita = await _receitasRepository.Get(id);
        if (receita is null)
            throw new ServiceException("Nenhuma receita encontrada para remoção");
        
        await _receitasRepository.Remove(id);
    }

    public async Task<ReceitasDTO> GetAsync(long id)
    {
        var receita = await _receitasRepository.Get(id);
        if (receita is null)
            throw new ServiceException("Nenhuma receita encontrada.");

        return _mapper.Map<ReceitasDTO>(receita);
    }

    public async Task<List<ReceitasDTO>> GetAllAsync()
    {
        var allReceitas =  await _receitasRepository.GetAll();
        return _mapper.Map<List<ReceitasDTO>>(allReceitas);
    }

    public async Task<List<ReceitasDTO>> SearchByDescricaoAsync(string descricao)
    {
        var allReceitas = await _receitasRepository.SearchByDescricao(descricao);
        return _mapper.Map<List<ReceitasDTO>>(allReceitas);
    }

    public async Task<List<ReceitasDTO>> GetByMesAsync(int ano, int mes)
    {
        var allReceitas = _receitasRepository.GetAll().Result;
        var finalResult = await Task.Run(() =>
        {
            return allReceitas.Where(x => x.Data.Year == ano && x.Data.Month == mes).ToList();
        });

        return _mapper.Map<List<ReceitasDTO>>(finalResult);
    }
}