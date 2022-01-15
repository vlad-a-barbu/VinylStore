using VinylStore.DataAccess.Models.Base;

namespace VinylStore.DataAccess.Models;

public class Vinyl : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }

    public Guid AlbumId { get; set; }
    
    public virtual Album Album { get; set; } = null!;
    public virtual ICollection<Purchase>? Purchases { get; set; }
}
