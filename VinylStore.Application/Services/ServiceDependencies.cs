using Autofac;

namespace VinylStore.Application.Services;

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
