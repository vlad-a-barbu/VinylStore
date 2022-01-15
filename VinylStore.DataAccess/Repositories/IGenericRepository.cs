namespace VinylStore.DataAccess.Repositories;

public interface IGenericRepository<TEntity> 
    where TEntity : class
{
    IQueryable<TEntity> GetAll();
    TEntity? Get(Guid id);
    
    void Insert(TEntity entity);
    
    void Update(TEntity entity);
    
    void Delete(TEntity entity);
    
    void Commit();
}
