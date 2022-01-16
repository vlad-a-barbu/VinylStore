using System.Transactions;
using Autofac;
using VinylStore.Domain;

namespace VinylStore.Application.Base;

public class BaseService
{
    private readonly ServiceDependencies _serviceDependencies;

    protected BaseService(ServiceDependencies serviceDependencies)
    {
        _serviceDependencies = serviceDependencies;
    }
    
    protected TResult ExecuteInTransaction<TResult>(Func<DomainServices, TResult> func)
    {
        using var scope = _serviceDependencies.LifetimeScope.BeginLifetimeScope();

        var domain = scope.Resolve<DomainServices>();

        using var transaction = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 1, 0));

        var result = func(domain);
        
        transaction.Complete();

        return result;
    }
    
    protected void ExecuteInTransaction(Action<DomainServices> action)
    {
        using var scope = _serviceDependencies.LifetimeScope.BeginLifetimeScope();

        var domain = scope.Resolve<DomainServices>();

        using var transaction = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 1, 0));

        action(domain);
        
        transaction.Complete();
    }
}
