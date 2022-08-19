using Challenge.Services.DTO;

namespace Challenge.Services.Interfaces;

public interface IResumoService
{
    Task<ResumoDto> ResumoOverall(int ano, int mes);
}