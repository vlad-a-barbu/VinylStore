using VinylStore.DataAccess.EF.Models.Base;

namespace VinylStore.DataAccess.EF.Models;

public class Album : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly ReleaseDate { get; set; }

    public Guid ArtistId { get; set; }
    
    public virtual Artist Artist { get; set; } = null!;
    public virtual ICollection<Song> Songs { get; set; } = null!;
    public virtual ICollection<Vinyl> Vinyls { get; set; } = null!;
}
