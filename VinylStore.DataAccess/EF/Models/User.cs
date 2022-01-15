using VinylStore.DataAccess.EF.Models.Base;

namespace VinylStore.DataAccess.EF.Models;

public class User : IEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;

    public Guid? AddressId { get; set; }

    public virtual Role Role { get; set; }
    public virtual Address? Address { get; set; }
    public virtual ICollection<Purchase>? Purchases { get; set; }
}
