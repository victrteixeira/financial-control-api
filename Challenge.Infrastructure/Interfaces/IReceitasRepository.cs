using Challenge.Domain;

namespace Challenge.Infrastructure.Interfaces;

public interface IReceitasRepository : IBaseRepository<Receitas>
{
    Task<List<Receitas>> SearchByDescricao(string descricao);
    Task<Receitas> GetByValor(double valor);
    Task<Receitas> GetByData(DateTime data);
}