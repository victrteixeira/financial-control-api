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

    public async Task<List<Receitas>> SearchByDescricao(string descricao)
    {
        var allReceitas = await _context.TReceitas
            .AsNoTracking()
            .Where(d => d.Descricao
                .Trim().ToLower()
                .Contains(descricao.Trim().ToLower()))
            .ToListAsync();

        return allReceitas;
    }

    public async Task<Receitas> GetByValor(double valor)
    {
        var resultValor = await _context.TReceitas
            .AsNoTracking()
            .Where(v => v.Valor == valor)
            .ToListAsync();

        return resultValor.FirstOrDefault();
    }

    public async Task<Receitas> GetByData(DateTime data)
    {
        var resultData = await _context.TReceitas
            .AsNoTracking()
            .Where(d => d.Data == data)
            .ToListAsync();

        return resultData.FirstOrDefault();
    }
}