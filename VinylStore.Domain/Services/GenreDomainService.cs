using VinylStore.DataAccess;
using VinylStore.DataObjects;
using EFModels = VinylStore.DataAccess.EF.Models;

namespace VinylStore.Domain.Services;

public class GenreDomainService : IDomainService<Genre>
{
    private readonly UnitOfWork _uow;

    public GenreDomainService(UnitOfWork uow) => _uow = uow;

    public Genre? GetById(Guid id)
    {
        var genre = _uow.Genres.Get(id);

        return genre is null ? null : new Genre
        {
            Id = genre!.Id,
            Name = genre!.Name
        };
    }

    public IEnumerable<Genre> GetAll(Func<Genre, bool>? filter = null)
    {
        var genres = _uow.Genres.GetAll().Select(g => new Genre
        {
            Id = g.Id,
            Name = g.Name
        }).ToList();

        return filter is null ? genres : genres.Where(filter);
    }
    
    public void Create(Genre genre)
    {
        _uow.Genres.Insert(new EFModels.Genre
        {
            Name = genre.Name
        });
        
        _uow.SaveChanges();
    }

    public void Update(Genre genre)
    {
        var entity = _uow.Genres.Get(genre.Id)
                     ?? throw new ArgumentException(nameof(genre));

        entity.Name = genre.Name;
        
        _uow.Genres.Update(entity);
        
        _uow.SaveChanges();
    }

    public void Delete(Guid id)
    {
        _uow.Genres.Delete(id);
        
        _uow.SaveChanges();
    }
}
