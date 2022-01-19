namespace VinylStore.Domain.Base;

public interface IDomainService<T>
{
    T? Get(Guid id);

    IEnumerable<T> GetAll(Func<T, bool>? filter = null);

    void Create(T entity);

    void Update(T entity);

    void Delete(Guid id);
}
