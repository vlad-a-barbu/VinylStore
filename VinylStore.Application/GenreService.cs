using VinylStore.Application.Base;
using VinylStore.DataObjects;

namespace VinylStore.Application;

public class GenreService : BaseService
{
    public GenreService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
    {
    }
    
    public Genre GetById(Guid id)
    {
        return ExecuteInTransaction(d => d.Genre.GetById(id))
            ?? throw new ArgumentException(nameof(id));
    }

    public IEnumerable<Genre> GetAll()
    {
        return ExecuteInTransaction(d => d.Genre.GetAll());
    }

    public void CreateGenre(Genre genre)
    {
        ExecuteInTransaction(d => d.Genre.Create(genre));
    }
    
    public void UpdateGenre(Genre genre)
    {
        ExecuteInTransaction(d => d.Genre.Update(genre));
    }
    
    public void DeleteGenre(Guid id)
    {
        ExecuteInTransaction(d => d.Genre.Delete(id));
    }
}
