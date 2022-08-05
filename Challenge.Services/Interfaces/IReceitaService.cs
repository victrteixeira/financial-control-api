using Challenge.Services.DTO;

namespace Challenge.Services.Interfaces;

public interface IReceitaService
{
    Task<ReceitasDTO> CreateAsync(ReceitasDTO value);
    Task<ReceitasDTO> UpdateAsync(ReceitasDTO value);
    Task RemoveAsync(long id);
    Task<ReceitasDTO> GetAsync(long id);
    Task<List<ReceitasDTO>> GetAllAsync();
    Task<List<ReceitasDTO>> GetAllByValorAsync(double value);
    Task<List<ReceitasDTO>> SearchByDescricaoAsync(string value);
    Task<List<ReceitasDTO>> SearchByMonthAsync(string value);
}