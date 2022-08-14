using AutoMapper;
using Challenge.Core;
using Challenge.Domain;
using Challenge.Infrastructure.Interfaces;
using Challenge.Services.DTO;
using Challenge.Services.Interfaces;

namespace Challenge.Services.Services;

public class DespesaService : IDespesaService
{
    private readonly IDespesasRepository _despesasRepository;
    private readonly IMapper _mapper;
    public DespesaService(IDespesasRepository despesasRepository, IMapper mapper)
    {
        _despesasRepository = despesasRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDespesa> CreateAsync(DespesasDTO despesasDto)
    {
        var entityDesc = await _despesasRepository.GetByDescricao(despesasDto.Descricao);
        if (entityDesc != default)
        {
            var entityMes = await _despesasRepository.GetByMes(despesasDto.Data.Month);
            if(entityMes != default)
                throw new DomainException("Não é possível existir duas despesas iguais no mesmo mês.");
        }

        var despesa = _mapper.Map<Despesas>(despesasDto);
        despesa.Validate();

        var despesaCreated = await _despesasRepository.Create(despesa);
        return _mapper.Map<ResponseDespesa>(despesaCreated);
    }

    // TODO > Complete the tests to this service.
    public async Task<ResponseDespesa> UpdateAsync(DespesasDTO despesasDto)
    {
        var oldDespesa = await _despesasRepository.Get(despesasDto.Id);
        if (oldDespesa == null)
            throw new ServiceException("Despesa não encontrada!");

        var entityDesc = await _despesasRepository.GetByDescricao(despesasDto.Descricao);
        if (entityDesc != default)
        {
            var entityMes = await _despesasRepository.GetByMes(despesasDto.Data.Month);
            if (entityMes != default)
                throw new DomainException("Não é possível existir duas despesas iguais no mesmo mês.");
        }
        

        Despesas despesas = _mapper.Map<Despesas>(despesasDto);
        despesas.Validate();

        var despesaUpdated = await _despesasRepository.Update(despesas);
        return _mapper.Map<ResponseDespesa>(despesas);
    }

    public async Task RemoveAsync(long id)
    {
        var despesa = _despesasRepository.Get(id).Result;
        if (despesa is null)
            throw new ServiceException(
                "Nenhuma despesa encontrada para remoção");
        
        await _despesasRepository.Remove(id);
    }

    public async Task<ResponseDespesa> GetAsync(long id)
    {
        var despesas = await _despesasRepository.Get(id);
        if (despesas is null)
            throw new ServiceException("Nenhum usuário encontrado.");
        
        return _mapper.Map<ResponseDespesa>(despesas);
    }

    public async Task<List<ResponseDespesa>> GetAllAsync()
    {
        var allDespesas = await _despesasRepository.GetAll();
        return _mapper.Map<List<ResponseDespesa>>(allDespesas);
    }

    public async Task<List<ResponseDespesa>> SearchByDescricaoAsync(string value)
    {
        var allDespesas = await _despesasRepository.SearchByDescricao(value);
        return _mapper.Map<List<ResponseDespesa>>(allDespesas);
    }

    public async Task<List<ResponseDespesa>> GetByMesAsync(int ano, int mes)
    {
        var allDespesas = _despesasRepository.GetAll().Result;
        var finalResult = await Task.Run(() =>
        {
            return allDespesas.Where(x => x.Data.Year == ano && x.Data.Month == mes).ToList();
        });

        return _mapper.Map<List<ResponseDespesa>>(finalResult);
    }
}