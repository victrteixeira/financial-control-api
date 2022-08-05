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
        var receitaExist = await _receitasRepository.SearchByDescricao(receitasDto.Descricao);
        if (receitaExist != default && receitaExist[0].Data.Month == receitasDto.Data.Month)
            throw new DomainException("Não é possível existir duas receitas iguais no mesmo mês.");

        var receita = _mapper.Map<Receitas>(receitasDto);
        receita.Validate();

        await _receitasRepository.Create(receita);
        return _mapper.Map<ReceitasDTO>(receita);
    }

    public async Task<ReceitasDTO> UpdateAsync(ReceitasDTO receitasDto)
    {
        var receitaExist = await _receitasRepository.Get(receitasDto.Id);
        if (receitaExist is null)
            throw new ServiceException("Receita não encontrada!");

        var receitaRepeated = await _receitasRepository.SearchByDescricao(receitasDto.Descricao);
        if (receitaRepeated[0].Data.Month == receitasDto.Data.Month)
            throw new DomainException("Não é possível existir duas receitas iguais no mesmo mês.");

        var receita = _mapper.Map<Receitas>(receitasDto);
        receita.Validate();

        await _receitasRepository.Update(receita);
        return _mapper.Map<ReceitasDTO>(receita);
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

    public async Task<List<ReceitasDTO>> SearchByValorAsync(double valor)
    {
        var allReceitas = await _receitasRepository.SearchByValor(valor);
        return _mapper.Map<List<ReceitasDTO>>(allReceitas);
    }

    public async Task<List<ReceitasDTO>> SearchByDescricaoAsync(string descricao)
    {
        var allReceitas = await _receitasRepository.SearchByDescricao(descricao);
        return _mapper.Map<List<ReceitasDTO>>(allReceitas);
    }

    public async Task<List<ReceitasDTO>> SearchByMesAsync(int mes)
    {
        var allReceitas = await _receitasRepository.SearchByMes(mes);
        return _mapper.Map<List<ReceitasDTO>>(allReceitas);
    }
}