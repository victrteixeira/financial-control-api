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

    public async Task<Despesas> GetByMes(int mes)
    {
        return await _context.TDespesas
            .AsNoTracking()
            .Where(d => d.Data.Month == mes)
            .FirstOrDefaultAsync();
    }

    public async Task<Despesas> GetByDescricao(string descricao)
    {
        return await _context.TDespesas
            .AsNoTracking().Where(d => d.Descricao.ToLower().Contains(descricao.ToLower()))
            .FirstOrDefaultAsync();
    }

    public async Task<List<Despesas>> SearchByDescricao(string descricao)
    {
        return await _context.TDespesas
            .AsNoTracking()
            .Where(d => d.Descricao.ToLower()
                .Contains(descricao.ToLower()))
            .ToListAsync();
    }

    public async Task<List<Despesas>> SearchByValor(double valor)
    {
        return await _context.TDespesas
            .AsNoTracking()
            .Where(v => v.Valor == valor)
            .ToListAsync();
    }

    public async Task<List<Despesas>> SearchByMes(int mes)
    {
        return await _context.TDespesas
            .AsNoTracking()
            .Where(x => x.Data.Month == mes)
            .ToListAsync();
    }
}