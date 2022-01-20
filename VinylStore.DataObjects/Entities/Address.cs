namespace VinylStore.DataObjects.Entities;

public class Address
{
    public Guid Id { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
}
