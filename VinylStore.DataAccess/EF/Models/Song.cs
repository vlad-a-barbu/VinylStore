using VinylStore.DataAccess.EF.Models.Base;

namespace VinylStore.DataAccess.EF.Models;

public class Song : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public Guid GenreId { get; set; }
    public Guid AlbumId { get; set; }
    
    public virtual Genre Genre { get; set; } = null!;
    public virtual Album Album { get; set; } = null!;
}
