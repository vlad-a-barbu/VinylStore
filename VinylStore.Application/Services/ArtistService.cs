using VinylStore.Application.Services.Base;
using VinylStore.DataObjects.Entities;

namespace VinylStore.Application.Services;

public class ArtistService : BaseService
{
    public ArtistService(ServiceDependencies serviceDependencies) : base(serviceDependencies) { }
    
    public Artist GetById(Guid id)
    {
        return ExecuteInTransaction(d => d.Artist.Get(id))
               ?? throw new ArgumentException(nameof(id));
    }

    public IEnumerable<Artist> GetAll()
    {
        return ExecuteInTransaction(d => d.Artist.GetAll());
    }

    public Guid CreateArtist(Artist artist)
    {
        return ExecuteInTransaction(d => d.Artist.Create(artist));
    }
    
    public void UpdateArtist(Artist artist)
    {
        ExecuteInTransaction(d => d.Artist.Update(artist));
    }
    
    public void DeleteArtist(Guid id)
    {
        ExecuteInTransaction(d => d.Artist.Delete(id));
    }
}