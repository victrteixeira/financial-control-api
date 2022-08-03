using Challenge.Domain;
using Challenge.Infrastructure.Context;
using Challenge.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Repositories;

public class DespesasRepository : BaseRepository<Despesas>, IDespesasRepository
{
    private readonly FinanceContext _context;
    public DespesasRepository(FinanceContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Despesas>> SearchByDescricao(string descricao)
    {
        var allDespesas = await _context.TDespesas
            .AsNoTracking()
            .Where(d => d.Descricao
                .Trim().ToLower()
                .Contains(descricao.Trim().ToLower()))
            .ToListAsync();

        return allDespesas;
    }

    public async Task<Despesas> GetByValor(double valor)
    {
        var resultValor = await _context.TDespesas
            .AsNoTracking()
            .Where(v => v.Valor == valor)
            .ToListAsync();

        return resultValor.FirstOrDefault();
    }

    public async Task<Despesas> GetByData(DateTime data)
    {
        var resultData = await _context.TDespesas
            .AsNoTracking()
            .Where(d => d.Data == data)
            .ToListAsync();

        return resultData.FirstOrDefault();
    }
}