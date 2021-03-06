using VinylStore.DataAccess.EF.Models.Base;

namespace VinylStore.DataAccess.EF.Models;

public class Purchase : IEntity
{
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    public decimal Discount { get; set; }

    public Guid UserId { get; set; }
    
    public virtual User User { get; set; } = null!;
    public virtual ICollection<Vinyl> Vinyls { get; set; } = null!;
}
