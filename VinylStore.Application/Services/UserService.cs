using VinylStore.Application.Services.Base;
using VinylStore.DataObjects.BusinessModels;
using VinylStore.DataObjects.Entities;
using VinylStore.Domain.Mapper;

namespace VinylStore.Application.Services;

public class UserService : BaseService
{
    public UserService(ServiceDependencies serviceDependencies) : base(serviceDependencies) { }

    public CompleteUser GetById(Guid id)
    {
        return ExecuteInTransaction(d =>
        {
            var user = d.User.Get(id) ?? throw new ArgumentException($"User {id} not found", nameof(id));

            var address = d.Address.Get(user.AddressId)!;

            var completeUser =
                Builder
                    .Aggregate<Address, User, CompleteUser>(address, user);
            
            return completeUser;
        });
    }

    public IEnumerable<CompleteUser> GetAll()
    {
        return ExecuteInTransaction(d =>
        {
            return d.User
                .GetAll()
                .Join(d.Address.GetAll(),
                    user => user.AddressId,
                    address => address.Id,
                    (user, address) =>
                        Builder.Aggregate<Address, User, CompleteUser>(address, user)
                );
        });
    }

    public void UpdateUser(CompleteUser user)
    {
        ExecuteInTransaction(d =>
        {
            var existingUser = d.User.Get(user.Id)
                ?? throw new ArgumentException($"User {user.Id} not found", nameof(user));

            var existingAddress = d.Address.Get(existingUser.AddressId)!;

            var updatedUser =
                Builder
                    .For<CompleteUser, User>(existingUser)
                    .Invoke(user);
            
            var updatedAddress =
                Builder
                    .For<CompleteUser, Address>(existingAddress, nameof(Address.Id))
                    .Invoke(user);
            
            d.User.Update(updatedUser);
            d.Address.Update(updatedAddress);
        });
    }
    
    public void DeleteUser(Guid id)
    {
        ExecuteInTransaction(d => d.User.Delete(id));
    }
}
