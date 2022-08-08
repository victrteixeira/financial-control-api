using Challenge.Domain;
using Challenge.Infrastructure.Context;
using Challenge.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Repositories;

public class ReceitasRepository : BaseRepository<Receitas>, IReceitasRepository
{
    private readonly FinanceContext _context;
    public ReceitasRepository(FinanceContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Receitas> GetByMes(int mes)
    {
        return await _context.TReceitas
            .AsNoTracking()
            .Where(d => d.Data.Month == mes)
            .FirstOrDefaultAsync();
    }

    public async Task<Receitas> GetByDescricao(string descricao)
    {
        return await _context.TReceitas
            .AsNoTracking().Where(d => d.Descricao
                .ToLower()
                .Contains(descricao.ToLower()))
            .FirstOrDefaultAsync();
    }

    public async Task<List<Receitas>> SearchByDescricao(string descricao)
    {
        return await _context.TReceitas
            .AsNoTracking()
            .Where(d => d.Descricao
                .ToLower()
                .Contains(descricao.ToLower()))
            .ToListAsync();
    }

    public async Task<List<Receitas>> SearchByValor(double valor)
    {
        return await _context.TReceitas
            .AsNoTracking()
            .Where(v => v.Valor == valor)
            .ToListAsync();
    }

    public async Task<List<Receitas>> SearchByMes(int mes)
    {
        return await _context.TReceitas
            .AsNoTracking()
            .Where(d => d.Data.Month == mes)
            .ToListAsync();
    }
}