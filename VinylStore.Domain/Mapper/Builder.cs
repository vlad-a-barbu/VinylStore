using System.Linq.Expressions;

namespace VinylStore.Domain.Mapper;

public class Builder
{
    public static Func<TSource, TDestination> For<TSource, TDestination>
        (TDestination? existingEntity = null, params string[] ignoredProperties) where TDestination : class
    {
        var source = Expression.Parameter(typeof(TSource));
        var destination = Expression.Variable(typeof(TDestination));

        var initializationDescriptor = 
            existingEntity is null ?
                Expression.Assign(
                    destination,
                    Expression.New(typeof(TDestination))
                )
                : Expression.Assign(
                    destination,
                    Expression.Constant(existingEntity)
                );

        var mappingDescriptors = new List<Expression> { initializationDescriptor };
        
        var assignationDescriptors = 
            source.Type
                .GetProperties()
                .Where(p => !ignoredProperties.Contains(p.Name))
                .Select(prop =>
                    AssignationDescriptor(
                        source, destination, prop.Name
                    ));
        
        mappingDescriptors.AddRange(assignationDescriptors);
        
        // return value
        mappingDescriptors.Add(destination);
        
        return Expression.Lambda<Func<TSource, TDestination>>(
            Expression.Block(
                new[] { destination },
                mappingDescriptors
            ), source
        ).Compile();
    }
    
    public static TDestination Aggregate<TSource1, TSource2, TDestination> (TSource1 source1, TSource2 source2) 
        where TDestination : class
    {
        var tempResult =
            For<TSource1, TDestination>()
                .Invoke(source1);

        var result =
            For<TSource2, TDestination>(tempResult)
                .Invoke(source2);

        return result;
    }
    
    private static Expression AssignationDescriptor(
        Expression source,
        Expression destination,
        string property
    ) => Expression.Call(
        typeof(Builder), nameof(Assign), new[] { source.Type, destination.Type },
        source, destination, Expression.Constant(property)
    );
    
    private static void Assign<TSource, TDestination>(TSource source, TDestination destination, string property)
    {
        var sourceValue = typeof(TSource).GetProperty(property)!.GetValue(source);

        var destinationProperty = typeof(TDestination).GetProperty(property);

        if (sourceValue is not null &&
            destinationProperty is not null &&
            sourceValue.GetType() == destinationProperty.PropertyType)
        {
            typeof(TDestination).GetProperty(property)!.SetValue(destination, sourceValue);
        }
    }
}
