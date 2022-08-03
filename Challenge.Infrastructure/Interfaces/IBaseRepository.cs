using Challenge.Domain;

namespace Challenge.Infrastructure.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> Create(T obj);
    Task<T> Update(T obj);
    Task Remove(long id);
    Task<T> Get(long id);
    Task<List<T>> GetAll(long id);
}