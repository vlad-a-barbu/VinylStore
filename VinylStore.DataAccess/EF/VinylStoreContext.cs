using Microsoft.EntityFrameworkCore;
using VinylStore.DataAccess.EF.Models;

namespace VinylStore.DataAccess.EF;

public class VinylStoreContext : DbContext
{
    public VinylStoreContext(DbContextOptions<VinylStoreContext> options) : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Song> Songs { get; set; } = null!;
    public DbSet<Artist> Artists { get; set; } = null!;
    public DbSet<Album> Albums { get; set; } = null!;
    public DbSet<Vinyl> Vinyls { get; set; } = null!;
    public DbSet<Purchase> Purchases { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Song>()
            .HasOne(s => s.Genre)
            .WithMany(g => g.Songs);

        modelBuilder.Entity<Album>()
            .HasOne(a => a.Artist)
            .WithMany(a => a.Albums);

        modelBuilder.Entity<Album>()
            .HasMany(a => a.Songs)
            .WithOne(s => s.Album);

        modelBuilder.Entity<Vinyl>()
            .HasOne(v => v.Album)
            .WithMany(a => a.Vinyls);

        modelBuilder.Entity<Purchase>()
            .HasMany(p => p.Vinyls)
            .WithMany(v => v.Purchases);

        modelBuilder.Entity<Purchase>(
            e => e.Property(
                p => p.Discount
            ).HasColumnType("decimal(18,2)")
        );
        
        modelBuilder.Entity<Vinyl>(
            e => e.Property(
                p => p.Price
            ).HasColumnType("decimal(18,2)")
        );
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Purchases)
            .WithOne(p => p.User);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Address)
            .WithOne(a => a.User)
            .HasForeignKey<User>(u => u.AddressId);
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
