using VinylStore.DataAccess;
using VinylStore.DataObjects.Entities;
using VinylStore.Domain.Base;
using EFModels = VinylStore.DataAccess.EF.Models;

namespace VinylStore.Domain.Services;

public class ArtistDomainService : IDomainService<Artist>
{
    private readonly UnitOfWork _uow;

    public ArtistDomainService(UnitOfWork uow)
    {
        _uow = uow;
    }
    
    public Artist? Get(Guid id)
    {
        var artist = _uow.Artists.Get(id);

        return artist is null ? null :
            Mapper.Builder
                .For<EFModels.Artist, Artist>()
                .Invoke(artist);
    }

    public IEnumerable<Artist> GetAll(Func<Artist, bool>? filter = null)
    {
        var artists = _uow.Artists
            .GetAll()
            .ToList()
            .Select(artist => 
                Mapper.Builder
                    .For<EFModels.Artist, Artist>()
                    .Invoke(artist)
            );

        return filter is null ? artists : artists.Where(filter);
    }
    
    public Guid Create(Artist artist)
    {
        var entity = Mapper.Builder
            .For<Artist, EFModels.Artist>()
            .Invoke(artist);
        
        _uow.Artists.Insert(entity);
        _uow.SaveChanges();

        return entity.Id;
    }

    public void Update(Artist artist)
    {
        var entity = _uow.Artists.Get(artist.Id)
                     ?? throw new ArgumentException($"Artist {artist.Id} not found", nameof(artist));
        
        _uow.Artists.Update(
            Mapper.Builder
                .For<Artist, EFModels.Artist>(entity)
                .Invoke(artist)
        );
        
        _uow.SaveChanges();
    }

    public void Delete(Guid id)
    {
        _uow.Artists.Delete(id);
        
        _uow.SaveChanges();
    }
}
