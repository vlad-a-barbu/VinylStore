using VinylStore.DataAccess.Models.Base;

namespace VinylStore.DataAccess.Models;

public class Address : IEntity
{
    public Guid Id { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
    public virtual User User { get; init; } = null!;
}
