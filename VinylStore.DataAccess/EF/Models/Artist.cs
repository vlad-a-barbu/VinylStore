using VinylStore.DataAccess.EF.Models.Base;

namespace VinylStore.DataAccess.EF.Models;

public class Artist : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Album> Albums { get; init; } = null!;
}