using VinylStore.DataAccess;
using VinylStore.DataObjects;
using VinylStore.Domain.Base;
using EFModels = VinylStore.DataAccess.EF.Models;

namespace VinylStore.Domain.Services;

public class GenreDomainService : IDomainService<Genre>
{
    private readonly UnitOfWork _uow;

    public GenreDomainService(UnitOfWork uow)
    {
        _uow = uow;
    }

    public Genre? Get(Guid id)
    {
        var genre = _uow.Genres.Get(id);

        return genre is null ? null :
            Mapper.Builder
                .For<EFModels.Genre, Genre>()
                .Invoke(genre);
    }

    public IEnumerable<Genre> GetAll(Func<Genre, bool>? filter = null)
    {
        var genres = _uow.Genres
            .GetAll()
            .ToList()
            .Select(genre => 
                Mapper.Builder
                    .For<EFModels.Genre, Genre>()
                    .Invoke(genre)
            );

        return filter is null ? genres : genres.Where(filter);
    }
    
    public void Create(Genre genre)
    {
        _uow.Genres.Insert(
            Mapper.Builder
                .For<Genre, EFModels.Genre>()
                .Invoke(genre)
        );
        
        _uow.SaveChanges();
    }

    public void Update(Genre genre)
    {
        var entity = _uow.Genres.Get(genre.Id)
            ?? throw new ArgumentException($"Genre {genre.Id} not found", nameof(genre));
        
        _uow.Genres.Update(
            Mapper.Builder
                .For<Genre, EFModels.Genre>(entity)
                .Invoke(genre)
        );
        
        _uow.SaveChanges();
    }

    public void Delete(Guid id)
    {
        _uow.Genres.Delete(id);
        
        _uow.SaveChanges();
    }
}
