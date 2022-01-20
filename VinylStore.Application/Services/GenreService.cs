using VinylStore.Application.Services.Base;
using VinylStore.DataObjects.Entities;

namespace VinylStore.Application.Services;

public class GenreService : BaseService
{
    public GenreService(ServiceDependencies serviceDependencies) : base(serviceDependencies) { }
    
    public Genre GetById(Guid id)
    {
        return ExecuteInTransaction(d => d.Genre.Get(id))
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
