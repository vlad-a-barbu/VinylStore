using VinylStore.Application.Base;
using VinylStore.DataObjects;

namespace VinylStore.Application;

public class UserService : BaseService
{
    public UserService(ServiceDependencies serviceDependencies) : base(serviceDependencies) { }

    public User GetById(Guid id)
    {
        return ExecuteInTransaction(d => d.User.Get(id))
               ?? throw new ArgumentException(nameof(id));
    }

    public IEnumerable<User> GetAll()
    {
        return ExecuteInTransaction(d => d.User.GetAll());
    }

    public void CreateUser(User user)
    {
        ExecuteInTransaction(d => d.User.Create(user));
    }
    
    public void UpdateUser(User user)
    {
        ExecuteInTransaction(d => d.User.Update(user));
    }
    
    public void DeleteUser(Guid id)
    {
        ExecuteInTransaction(d => d.User.Delete(id));
    }
}
