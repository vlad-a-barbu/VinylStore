using VinylStore.DataAccess.EF.Models;

namespace VinylStore.DataObjects.BusinessModels;

public class CompleteUser
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Role Role { get; set; }
    public string Country { get; set; } = null!;
    public string City { get; set; } = null!;
    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
}
