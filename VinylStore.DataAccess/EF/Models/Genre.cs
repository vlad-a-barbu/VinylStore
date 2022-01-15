using VinylStore.DataAccess.EF.Models.Base;

namespace VinylStore.DataAccess.EF.Models;

public class Genre : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Song> Songs { get; set; } = null!;
}
