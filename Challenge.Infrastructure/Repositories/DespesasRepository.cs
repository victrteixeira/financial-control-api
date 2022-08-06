﻿using Challenge.Domain;
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
        var despesa = await _context.TDespesas
            .AsNoTracking()
            .Where(d => d.Data.Month == mes)
            .FirstOrDefaultAsync();

        return despesa;
    }

    public async Task<Despesas> GetByDescricao(string descricao)
    {
        var despesa = await _context.TDespesas
            .AsNoTracking().Where(d => d.Descricao.ToLower().Contains(descricao.ToLower()))
            .FirstOrDefaultAsync();

        return despesa;
    }

    public async Task<List<Despesas>> SearchByDescricao(string descricao)
    {
        var allDespesas = await _context.TDespesas
            .AsNoTracking()
            .Where(d => d.Descricao.ToLower()
                .Contains(descricao.ToLower()))
            .ToListAsync();

        return allDespesas;
    }

    public async Task<List<Despesas>> SearchByValor(double valor)
    {
        var resultValor = await _context.TDespesas
            .AsNoTracking()
            .Where(v => v.Valor == valor)
            .ToListAsync();

        return resultValor;
    }

    public async Task<List<Despesas>> SearchByMes(int mes)
    {
        var resultData = await _context.TDespesas
            .AsNoTracking()
            .Where(x => x.Data.Month == mes)
            .ToListAsync();

        return resultData;
    }
}