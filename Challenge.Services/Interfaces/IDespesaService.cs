using Challenge.Services.DTO;

namespace Challenge.Services.Interfaces;

public interface IDespesaService
{
    Task<DespesasDTO> CreateAsync(DespesasDTO despesasDto);
    Task<DespesasDTO> UpdateAsync(DespesasDTO despesasDto);
    Task RemoveAsync(long id);
    Task<DespesasDTO> GetAsync(long id);
    Task<List<DespesasDTO>> GetAllAsync();
    Task<List<DespesasDTO>> SearchByDescricaoAsync(string descricao);
    Task<List<DespesasDTO>> GetByMesAsync(int ano, int mes);
}