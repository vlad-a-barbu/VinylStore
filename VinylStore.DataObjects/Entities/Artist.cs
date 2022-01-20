using VinylStore.DataAccess.EF.Models.Base;

namespace VinylStore.DataObjects.Entities;

public class Artist : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;   
}
