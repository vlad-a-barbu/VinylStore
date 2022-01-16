using Autofac;
using VinylStore.Domain;

namespace VinylStore.Application;

public class ServiceDependencies
{
    public ILifetimeScope LifetimeScope { get; }

    public ServiceDependencies(
        ILifetimeScope lifetimeScope
    )
    {
        LifetimeScope = lifetimeScope;
    }
}
