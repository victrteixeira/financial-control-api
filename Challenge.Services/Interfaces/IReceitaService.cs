using Challenge.Services.DTO;

namespace Challenge.Services.Interfaces;

public interface IReceitaService
{
    Task<ReceitasDTO> CreateAsync(ReceitasDTO receitasDto);
    Task<ReceitasDTO> UpdateAsync(ReceitasDTO receitasDto);
    Task RemoveAsync(long id);
    Task<ReceitasDTO> GetAsync(long id);
    Task<List<ReceitasDTO>> GetAllAsync();
    Task<List<ReceitasDTO>> SearchByValorAsync(double valor);
    Task<List<ReceitasDTO>> SearchByDescricaoAsync(string descricao);
    Task<List<ReceitasDTO>> SearchByMesAsync(int mes);
}