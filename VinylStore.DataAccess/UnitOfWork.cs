using VinylStore.DataAccess.EF;
using VinylStore.DataAccess.EF.Models;
using VinylStore.DataAccess.Repositories;

namespace VinylStore.DataAccess;

public class UnitOfWork
{
    private readonly VinylStoreContext _context;

    public UnitOfWork(VinylStoreContext context) => _context = context;

    private GenericRepository<Genre>? _genres;
    public GenericRepository<Genre> Genres => _genres ??= new GenericRepository<Genre>(_context);
    
    private GenericRepository<Song>? _songs;
    public GenericRepository<Song> Songs => _songs ??= new GenericRepository<Song>(_context);
    
    private GenericRepository<Album>? _albums;
    public GenericRepository<Album> Albums => _albums ??= new GenericRepository<Album>(_context);
    
    private GenericRepository<Artist>? _artists;
    public GenericRepository<Artist> Artists => _artists ??= new GenericRepository<Artist>(_context);
    
    private GenericRepository<Vinyl>? _vinyls;
    public GenericRepository<Vinyl> Vinyls => _vinyls ??= new GenericRepository<Vinyl>(_context);
    
    private GenericRepository<Purchase>? _purchases;
    public GenericRepository<Purchase> Purchases => _purchases ??= new GenericRepository<Purchase>(_context);
    
    private GenericRepository<User>? _users;
    public GenericRepository<User> Users => _users ??= new GenericRepository<User>(_context);
    
    private GenericRepository<Address>? _addresses;
    public GenericRepository<Address> Addresses => _addresses ??= new GenericRepository<Address>(_context);

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
