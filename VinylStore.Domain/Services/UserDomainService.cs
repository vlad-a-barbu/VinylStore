using VinylStore.DataAccess;
using VinylStore.DataObjects;
using EFModels = VinylStore.DataAccess.EF.Models;

namespace VinylStore.Domain.Services;

public class UserDomainService : IDomainService<User>
{
    private readonly UnitOfWork _uow;

    public UserDomainService(UnitOfWork uow) => _uow = uow;
    
    public User? GetById(Guid id)
    {
        var user = _uow.Users.Get(id);
        
        return user is null ? null : MapToDomainUser(user);
    }

    public IEnumerable<User> GetAll(Func<User, bool>? filter = null)
    {
        var users = 
            _uow.Users
                .GetAll()
                .ToList()
                .Select(MapToDomainUser);

        return filter is null ? users : users.Where(filter);
    }

    public void Create(User user)
    {
        throw new NotImplementedException();
    }

    public void Update(User user)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    private EFModels.User MapToEFModel(User user) => new EFModels.User
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        Role = user.Role,
        AddressId = user.AddressId
    };
    
    private User MapToDomainUser(EFModels.User user) => new User
    {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        Role = user.Role,
        AddressId = user.AddressId
    };
}
