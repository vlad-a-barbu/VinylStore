using VinylStore.DataAccess.EF.Models;
using VinylStore.DataAccess.EF.Models.Base;

namespace VinylStore.DataObjects.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public Role Role { get; set; }
    public Guid AddressId { get; set; }
}

