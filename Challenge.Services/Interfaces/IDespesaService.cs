﻿using Challenge.Services.DTO;

namespace Challenge.Services.Interfaces;

public interface IDespesaService
{
    Task<ResponseDespesa> CreateAsync(DespesasDTO despesasDto);
    Task<ResponseDespesa> UpdateAsync(DespesasDTO despesasDto);
    Task RemoveAsync(long id);
    Task<ResponseDespesa> GetAsync(long id);
    Task<List<ResponseDespesa>> GetAllAsync();
    Task<List<ResponseDespesa>> SearchByDescricaoAsync(string descricao);
    Task<List<ResponseDespesa>> GetByMesAsync(int ano, int mes);
}