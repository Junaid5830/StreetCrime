using DAL.Data;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repository
{
  class GenericRepository<T> : IGenericRepository<T> where T : class
  {
    protected readonly DatabaseContext _context;
    protected readonly DbSet<T> _db;

    public GenericRepository(DatabaseContext context)
    {
      _context = context;
      _db = _context.Set<T>();
    }

    public async Task Delete(int id)
    {
      var entity = await _db.FindAsync(id);
      _db.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
      _db.RemoveRange(entities);
    }

    public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, List<string> includes = null)
    {
      IQueryable<T> query = _db;

      if(expression != null)
      {
        query = query.Where(expression);
      }

      if (includes != null)
      {
        foreach (var includeProperty in includes)
        {
          query = query.Include(includeProperty);
        }
      }

      if(orderby != null)
      {
        query = orderby(query);
      }

      return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
    {
      IQueryable<T> query = _db;

      if (includes != null)
      {
        foreach (var includeProperty in includes)
        {
          query = query.Include(includeProperty);
        }
      }

      return await query.AsNoTracking().FirstOrDefaultAsync(expression);
    }

    public async Task Insert(T entiy)
    {
      await _db.AddAsync(entiy);
    }

    public async Task InsertRange(IEnumerable<T> entities)
    {
      await _db.AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
      _db.Attach(entity);
      _context.Entry(entity).State = EntityState.Modified;
    }
  }
}
