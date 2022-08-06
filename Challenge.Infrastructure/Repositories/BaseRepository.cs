using Challenge.Domain;
using Challenge.Infrastructure.Context;
using Challenge.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly FinanceContext _context;

    public BaseRepository(FinanceContext context)
    {
        _context = context;
    }

    public virtual async Task<T> Create(T obj)
    {
        _context.Add(obj);
        await _context.SaveChangesAsync();

        return obj;
    }

    public virtual async Task<T> Update(T obj)
    {
        _context.Entry(obj).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return obj;
    }

    public virtual async Task Remove(long id)
    {
        var t = await Get(id);
        if (t != null)
            _context.Remove(t);
        
        await _context.SaveChangesAsync();
    }

    public virtual async Task<T> Get(long id)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync();
    }

    public virtual Task<List<T>> GetAll()
    {
        return _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
}