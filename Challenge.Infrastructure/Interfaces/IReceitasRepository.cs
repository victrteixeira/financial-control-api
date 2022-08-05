using Challenge.Domain;

namespace Challenge.Infrastructure.Interfaces;

public interface IReceitasRepository : IBaseRepository<Receitas>
{
    Task<List<Receitas>> SearchByDescricao(string descricao);
    Task<List<Receitas>> SearchByValor(double valor);
    Task<List<Receitas>> SearchByMes(int mes);
}