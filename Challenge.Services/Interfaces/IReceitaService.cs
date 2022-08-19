using Challenge.Services.DTO;

namespace Challenge.Services.Interfaces;

public interface IReceitaService
{
    Task<ReceitasDto> CreateAsync(ReceitasDto receitasDto);
    Task<ReceitasDto> UpdateAsync(ReceitasDto receitasDto);
    Task RemoveAsync(long id);
    Task<ReceitasDto> GetAsync(long id);
    Task<List<ReceitasDto>> GetAllAsync();
    Task<List<ReceitasDto>> SearchByDescricaoAsync(string descricao);
    Task<List<ReceitasDto>> GetByMesAsync(int ano, int mes);
}