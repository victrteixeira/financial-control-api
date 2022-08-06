using Challenge.Domain;

namespace Challenge.Infrastructure.Interfaces;

public interface IReceitasRepository : IBaseRepository<Receitas>
{
    Task<Receitas> GetByMes(int mes);
    Task<Receitas> GetByDescricao(string descricao);
    Task<List<Receitas>> SearchByDescricao(string descricao);
    Task<List<Receitas>> SearchByValor(double valor);
    Task<List<Receitas>> SearchByMes(int mes);
}