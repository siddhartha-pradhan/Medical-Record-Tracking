using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Silverline.Infrastructure.Persistence;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;

    private DbSet<T> _dbSet;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public T Get(int id)
    {
        return _dbSet.Find(id);
    }

    public List<T> GetAll()
    {
        IQueryable<T> query = _dbSet;

        return query.ToList();
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Save() 
    {
        _dbContext.SaveChanges();
    }
}
