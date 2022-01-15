using VinylStore.DataAccess.Models.Base;

namespace VinylStore.DataAccess.Models;

public class Artist : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Album> Albums { get; init; } = null!;
}