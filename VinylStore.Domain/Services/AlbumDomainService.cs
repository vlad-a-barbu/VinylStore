using VinylStore.DataAccess;
using VinylStore.DataObjects.Entities;
using VinylStore.Domain.Base;
using VinylStore.Domain.Mapper;
using EFModels = VinylStore.DataAccess.EF.Models;

namespace VinylStore.Domain.Services;

public class AlbumDomainService : IDomainService<Album>
{
    private readonly UnitOfWork _uow;

    public AlbumDomainService(UnitOfWork uow)
    {
        _uow = uow;
    }

    public Album? Get(Guid id)
    {
        var album = _uow.Albums.Get(id);

        return album is null ? null :
            Builder
                .For<EFModels.Album, Album>()
                .Invoke(album);
    }

    public IEnumerable<Album> GetAll(Func<Album, bool>? filter = null)
    {
        var albums = _uow.Albums
            .GetAll()
            .ToList()
            .Select(album =>
                Builder
                    .For<EFModels.Album, Album>()
                    .Invoke(album)
            );

        return filter is null ? albums : albums.Where(filter);
    }
    
    public Guid Create(Album album)
    {
        var entity = Builder
            .For<Album, EFModels.Album>()
            .Invoke(album);
        
        _uow.Albums.Insert(entity);
        _uow.SaveChanges();

        return entity.Id;
    }

    public void Update(Album album)
    {
        var entity = _uow.Albums.Get(album.Id)
            ?? throw new ArgumentException($"Album {album.Id} not found", nameof(album));
        
        _uow.Albums.Update(
            Builder
                .For<Album, EFModels.Album>(entity)
                .Invoke(album)
        );
        
        _uow.SaveChanges();
    }

    public void Delete(Guid id)
    {
        _uow.Albums.Delete(id);
        
        _uow.SaveChanges();
    }
}
