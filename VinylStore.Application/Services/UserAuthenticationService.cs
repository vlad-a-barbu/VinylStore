using VinylStore.Application.Security;
using VinylStore.Application.Services.Base;
using Role = VinylStore.DataAccess.EF.Models.Role;
using VinylStore.DataObjects.AuthenticationModels;
using VinylStore.Domain.Mapper;
using VinylStore.DataObjects.Entities;

namespace VinylStore.Application.Services;

public class UserAuthenticationService : BaseService
{
    public UserAuthenticationService(ServiceDependencies serviceDependencies) : base(serviceDependencies) { }

    public Guid RegisterClient(RegisterUser registerUser)
    {
        return Register(registerUser, Role.Client);
    }
    
    public Guid RegisterAdmin(RegisterUser registerUser)
    {
        return Register(registerUser, Role.Admin);
    }

    public AuthenticatedUser? Authenticate(LoginUser loginUser)
    {
        return ExecuteInTransaction(d =>
        {
            var user = d.User.GetAll(e => e.Email == loginUser.Email).SingleOrDefault()
                ?? throw new ArgumentException($"User {loginUser.Email} not found", nameof(loginUser));

            var isValid = PasswordManager.ValidatePassword(loginUser.Password, user.PasswordSalt, user.PasswordHash);

            return isValid ?
                    Builder
                        .For<User, AuthenticatedUser>()
                        .Invoke(user)
                    : null;
        });
    }
    
    private Guid Register(RegisterUser registerUser, Role role)
    {
        return ExecuteInTransaction(d =>
        {
            var address = Builder
                .For<RegisterUser, Address>()
                .Invoke(registerUser);
            
            var addressId = d.Address.Create(address);
            
            var user = Builder
                .For<RegisterUser, User>()
                .Invoke(registerUser);

            user.Role = role;
            user.AddressId = addressId;

            var password = PasswordManager.GenerateSaltedHash(registerUser.Password);

            user.PasswordHash = password.Hash;
            user.PasswordSalt = password.Salt;
            
            return d.User.Create(user);
        });
    }
}
