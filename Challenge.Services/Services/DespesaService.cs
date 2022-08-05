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

    public async Task<DespesasDTO> CreateAsync(DespesasDTO despesasDto)
    {
        var despesaExist = await _despesasRepository.SearchByDescricao(despesasDto.Descricao);
        if (despesaExist != default)
            throw new DomainException("Não é possível adicionar duas despesas iguais no mesmo mês.");

        var despesa = _mapper.Map<Despesas>(despesasDto);
        despesa.Validate();

        var despesaCreated = await _despesasRepository.Create(despesa);
        return _mapper.Map<DespesasDTO>(despesaCreated);
    }

    public async Task<DespesasDTO> UpdateAsync(DespesasDTO despesasDto)
    {
        var oldDespesa = await _despesasRepository.Get(despesasDto.Id);
        if (oldDespesa is null)
            throw new ServiceException("Despesa não encontrada");

        Despesas despesas = _mapper.Map<Despesas>(despesasDto);
        despesas.Validate();

        await _despesasRepository.Update(despesas);
        return _mapper.Map<DespesasDTO>(despesas);
    }

    public async Task RemoveAsync(long id)
    {
        var despesa = _despesasRepository.Get(id);
        if (despesa is null)
            throw new ServiceException(
                "Nenhum usuário encontrado para remoção");
        
        await _despesasRepository.Remove(id);
    }

    public async Task<DespesasDTO> GetAsync(long id)
    {
        var despesas = await _despesasRepository.Get(id);
        if (despesas is null)
            throw new ServiceException("Nenhum usuário encontrado.");
        
        return _mapper.Map<DespesasDTO>(despesas);
    }

    public async Task<List<DespesasDTO>> GetAllAsync()
    {
        var allDespesas = await _despesasRepository.GetAll();
        return _mapper.Map<List<DespesasDTO>>(allDespesas);
    }

    public async Task<List<DespesasDTO>> SearchByValorAsync(double value)
    {
        var allDespesas = await _despesasRepository.SearchByValor(value);
        return _mapper.Map<List<DespesasDTO>>(allDespesas);
    }

    public async Task<List<DespesasDTO>> SearchByDescricaoAsync(string value)
    {
        var allDespesas = await _despesasRepository.SearchByDescricao(value);
        return _mapper.Map<List<DespesasDTO>>(allDespesas);
    }

    public async Task<List<DespesasDTO>> SearchByMonthAsync(int value)
    {
        if (value > 12)
            throw new ServiceException("Para buscar por um mês é necessário inserir um valor entre 1 e 12");
        
        var allDespesas = await _despesasRepository.SearchByMes(value);
        return _mapper.Map<List<DespesasDTO>>(allDespesas);
    }
}