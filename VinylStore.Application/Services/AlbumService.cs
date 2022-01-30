using VinylStore.Application.Services.Base;
using VinylStore.DataObjects.BusinessModels;
using VinylStore.DataObjects.Entities;
using VinylStore.Domain.Mapper;

namespace VinylStore.Application.Services;

public class AlbumService : BaseService
{
    public AlbumService(ServiceDependencies serviceDependencies) : base(serviceDependencies) { }
    
    public CompleteAlbum GetById(Guid id)
    {
        return ExecuteInTransaction(d =>
        {
            var album = d.Album.Get(id) ?? throw new ArgumentException(nameof(id));

            var artist = d.Artist.Get(album.ArtistId)!;

            var completeAlbum =
                Builder.For<Album, CompleteAlbum>()
                    .Invoke(album);

            completeAlbum.Artist = artist.Name;
            
            return completeAlbum;
        });
    }

    public IEnumerable<CompleteAlbum> GetAll()
    {
        return ExecuteInTransaction(d =>
        {
            return d.Album
                .GetAll()
                .Join(d.Artist.GetAll(),
                    album => album.ArtistId,
                    artist => artist.Id,
                    (album, artist) =>
                    {
                        var completeAlbum = Builder.For<Album, CompleteAlbum>()
                            .Invoke(album);

                        completeAlbum.Artist = artist.Name;

                        return completeAlbum;
                    }
                );
        });
    }

    public Guid CreateAlbum(CompleteAlbum album)
    {
        return ExecuteInTransaction(d =>
        {
            var albumEntity =
                Builder.For<CompleteAlbum, Album>()
                    .Invoke(album);

            var artist = d.Artist
                .GetAll(a => a.Name == album.Artist)
                .SingleOrDefault()
                    ?? throw new ArgumentException($"Artist {album.Artist} not found", nameof(album));

            albumEntity.ArtistId = artist.Id;

            return d.Album.Create(albumEntity);
        });
    }
    
    public void UpdateAlbum(CompleteAlbum album)
    {
        ExecuteInTransaction(d =>
        {
            var existingAlbum = d.Album.Get(album.Id)
                ?? throw new ArgumentException($"Album {album.Id} not found", nameof(album));
            
            var albumEntity =
                Builder.For<CompleteAlbum, Album>(
                        existingAlbum, 
                        nameof(CompleteAlbum.Artist)
                    )
                    .Invoke(album);

            var artist = d.Artist
                             .GetAll(a => a.Name == album.Artist)
                             .SingleOrDefault()
                         ?? throw new ArgumentException($"Artist {album.Artist} not found", nameof(album));

            albumEntity.ArtistId = artist.Id;
            
            d.Album.Update(albumEntity);
        });
    }
    
    public void DeleteAlbum(Guid id)
    {
        ExecuteInTransaction(d => d.Album.Delete(id));
    }
}
