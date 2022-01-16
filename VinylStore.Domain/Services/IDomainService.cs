namespace VinylStore.Domain.Services;

public interface IDomainService<TEntity>
{
    TEntity? GetById(Guid id);

    IEnumerable<TEntity> GetAll(Func<TEntity, bool>? filter = null);

    void Create(TEntity entity);

    void Update(TEntity entity);

    void Delete(Guid id);
}
