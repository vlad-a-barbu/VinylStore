using VinylStore.DataAccess.EF.Models.Base;

namespace VinylStore.DataObjects.Entities;

public class Genre : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; init; } = null!;
}
