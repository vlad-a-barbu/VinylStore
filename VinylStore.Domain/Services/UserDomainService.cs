using VinylStore.DataAccess;
using VinylStore.DataObjects.Entities;
using VinylStore.Domain.Base;
using VinylStore.Domain.Mapper;
using EFModels = VinylStore.DataAccess.EF.Models;

namespace VinylStore.Domain.Services;

public class UserDomainService : IDomainService<User>
{
    private readonly UnitOfWork _uow;

    public UserDomainService(UnitOfWork uow)
    {
        _uow = uow;
    }

    public User? Get(Guid id)
    {
        var user = _uow.Users.Get(id);

        return user is null ? null :
            Builder
                .For<EFModels.User, User>()
                .Invoke(user);
    }

    public IEnumerable<User> GetAll(Func<User, bool>? filter = null)
    {
        var users = _uow.Users
            .GetAll()
            .ToList()
            .Select(user => 
                Builder
                    .For<EFModels.User, User>()
                    .Invoke(user)
            );

        return filter is null ? users : users.Where(filter);
    }
    
    public Guid Create(User user)
    {
        var entity = Builder
            .For<User, EFModels.User>()
            .Invoke(user);
        
        _uow.Users.Insert(entity);
        _uow.SaveChanges();

        return entity.Id;
    }

    public void Update(User user)
    {
        var entity = _uow.Users.Get(user.Id)
                     ?? throw new ArgumentException($"User {user.Id} not found", nameof(user));
        
        _uow.Users.Update(
            Builder
                .For<User, EFModels.User>(entity)
                .Invoke(user)
        );
        
        _uow.SaveChanges();
    }

    public void Delete(Guid id)
    {
        _uow.Users.Delete(id);
        
        _uow.SaveChanges();
    }
}
