using Challenge.Domain;

namespace Challenge.Infrastructure.Interfaces;

public interface IDespesasRepository : IBaseRepository<Despesas>
{
    Task<Despesas> GetByMes(int mes);
    Task<Despesas> GetByDescricao(string descricao);
    Task<List<Despesas>> SearchByDescricao(string descricao);
    Task<List<Despesas>> SearchByValor(double valor);
    Task<List<Despesas>> SearchByMes(int mes);
}