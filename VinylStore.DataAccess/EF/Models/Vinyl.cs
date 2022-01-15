using VinylStore.DataAccess.EF.Models.Base;

namespace VinylStore.DataAccess.EF.Models;

public class Vinyl : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }

    public Guid AlbumId { get; set; }
    
    public virtual Album Album { get; set; } = null!;
    public virtual ICollection<Purchase>? Purchases { get; set; }
}
