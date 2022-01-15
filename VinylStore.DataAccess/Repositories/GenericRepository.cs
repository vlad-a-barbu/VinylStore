using Microsoft.EntityFrameworkCore;
using VinylStore.DataAccess.Models;
using VinylStore.DataAccess.Models.Base;

namespace VinylStore.DataAccess.Repositories;

public class GenericRepository<TEntity>
    : IGenericRepository<TEntity> where TEntity : class, IEntity
{
    private readonly VinylStoreContext _context;
    private readonly DbSet<TEntity> _entities;

    public GenericRepository(VinylStoreContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _entities;
    }

    public TEntity? Get(Guid id)
    {
        return _entities.Find(id);
    }

    public void Insert(TEntity entity)
    {
        _entities.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _entities.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _entities.Remove(entity);
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
}
