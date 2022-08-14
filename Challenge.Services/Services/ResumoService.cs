using AutoMapper;
using Challenge.Domain.Enums;
using Challenge.Infrastructure.Interfaces;
using Challenge.Services.DTO;
using Challenge.Services.Interfaces;

namespace Challenge.Services.Services;

public class ResumoService : IResumoService
{
    private readonly IMapper _mapper;
    private readonly IReceitasRepository _receitasRepository;
    private readonly IDespesasRepository _despesasRepository;
    public ResumoService(IMapper mapper, IReceitasRepository receitasRepository, IDespesasRepository despesasRepository)
    {
        _mapper = mapper;
        _receitasRepository = receitasRepository;
        _despesasRepository = despesasRepository;
    }
    
    public async Task<ResumoDTO> ResumoOverall(int ano, int mes)
    {
        ResumoDTO resumoDto = new();
        resumoDto.CategoriaOverAll = new List<CategoriaDTO>();

        var allReceitas = _receitasRepository.GetAll().Result;
        await Task.Run(() =>
        {
            var receitaValue = allReceitas.Where(x => x.Data.Year == ano && x.Data.Month == mes).Sum(x => x.Valor);
                resumoDto.ReceitaValue = (decimal)receitaValue;
        });

        var allDespesas = _despesasRepository.GetAll().Result;
        await Task.Run(() =>
        {
            var despesaValue = allDespesas.Where(x => x.Data.Year == ano && x.Data.Month == mes).Sum(x => x.Valor);
            return resumoDto.DespesaValue = (decimal)despesaValue;
        });

        resumoDto.Saldo = (resumoDto.ReceitaValue - resumoDto.DespesaValue);

        await Task.Run(() =>
        {
            foreach (var categoria in (Categoria[])Enum.GetValues(typeof(Categoria)))
            {
                var value = allDespesas.Where(x => x.Categorias == categoria && x.Data.Year == ano && x.Data.Month == mes).Sum(x => x.Valor);
                var categoryDto = new CategoriaDTO((decimal)value, categoria);
                resumoDto.CategoriaOverAll.Add(categoryDto);
            }
        });
        return resumoDto;
    }
}