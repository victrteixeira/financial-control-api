using Challenge.Domain;

namespace Challenge.Infrastructure.Interfaces;

public interface IDespesasRepository : IBaseRepository<Despesas>
{
    Task<List<Despesas>> SearchByDescricao(string descricao);
    Task<Despesas> GetByValor(double valor);
    Task<Despesas> GetByData(DateTime data);
}