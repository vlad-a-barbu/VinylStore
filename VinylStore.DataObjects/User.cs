using VinylStore.DataAccess.EF.Models;

namespace VinylStore.DataObjects;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Role Role { get; set; }
    public Guid? AddressId { get; set; }
}
