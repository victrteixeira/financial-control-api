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

    public async Task<T> Create(T obj)
    {
        _context.Add(obj);
        await _context.SaveChangesAsync();

        return obj;
    }

    public async Task<T> Update(T obj)
    {
        _context.Entry(obj).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return obj;
    }

    public async Task Remove(long id)
    {
        var t = await Get(id);
        if (t != null)
            _context.Remove(t);
        
        await _context.SaveChangesAsync();
    }

    public async Task<T> Get(long id)
    {
        var t = await _context.Set<T>()
            .AsNoTracking()
            .Where(i => i.Id == id)
            .ToListAsync();

        return t.FirstOrDefault();
    }

    public Task<List<T>> GetAll(long id)
    {
        return _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
}