using Challenge.Services.DTO;

namespace Challenge.Services.Interfaces;

public interface IResumoService
{
    Task<ResumoDTO> ResumoOverall(int ano, int mes);
}