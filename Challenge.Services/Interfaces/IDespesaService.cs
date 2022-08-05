using Challenge.Services.DTO;

namespace Challenge.Services.Interfaces;

public interface IDespesaService
{
    Task<DespesasDTO> CreateAsync(DespesasDTO value);
    Task<DespesasDTO> UpdateAsync(DespesasDTO value);
    Task RemoveAsync(long id);
    Task<DespesasDTO> GetAsync(long id);
    Task<List<DespesasDTO>> GetAllAsync();
    Task<List<DespesasDTO>> GetAllByValorAsync(double value);
    Task<List<DespesasDTO>> SearchByDescricaoAsync(string value);
    Task<List<DespesasDTO>> SearchByMonthAsync(string value);
}