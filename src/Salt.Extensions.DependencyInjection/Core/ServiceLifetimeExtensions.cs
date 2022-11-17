namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for the <see cref="ServiceLifetime"/> data type.
/// </summary>
static class ServiceLifetimeExtensions
{
    /// <summary>
    /// Gets a delegate object that allows to register a service class into a service collection.
    /// </summary>
    /// <param name="lifetime">Service lifetime to get the function.</param>
    /// <returns>Delegate object of the <see cref="AddServiceClassFunc"/> type.</returns>
    public static AddServiceClassFunc GetAddServiceClassFunc(this ServiceLifetime lifetime) => lifetime switch
    {
        ServiceLifetime.Singleton => ServiceCollectionServiceExtensions.AddSingleton,
        ServiceLifetime.Scoped => ServiceCollectionServiceExtensions.AddScoped,
        ServiceLifetime.Transient => ServiceCollectionServiceExtensions.AddTransient,
        _ => throw new InvalidOperationException($"Invalid service lifetime provided (Lifetime: '{lifetime}').")
    };

    /// <summary>
    /// Gets a delegate object that allows to register a service class + its interface into a service collection.
    /// </summary>
    /// <param name="lifetime">Service lifetime to get the function.</param>
    /// <returns>Delegate object of the <see cref="AddServiceInterfaceFunc"/> type.</returns>
    public static AddServiceInterfaceFunc GetAddServiceInterfaceFunc(this ServiceLifetime lifetime) => lifetime switch
    {
        ServiceLifetime.Singleton => ServiceCollectionServiceExtensions.AddSingleton,
        ServiceLifetime.Scoped => ServiceCollectionServiceExtensions.AddScoped,
        ServiceLifetime.Transient => ServiceCollectionServiceExtensions.AddTransient,
        _ => throw new InvalidOperationException($"Invalid service lifetime provided (Lifetime: '{lifetime}').")
    };

    public static AddServiceFactoryFunc GetAddServiceFactoryFunc(this ServiceLifetime lifetime) => lifetime switch
    {
        ServiceLifetime.Singleton => ServiceCollectionServiceExtensions.AddSingleton,
        ServiceLifetime.Scoped => ServiceCollectionServiceExtensions.AddScoped,
        ServiceLifetime.Transient => ServiceCollectionServiceExtensions.AddTransient,
        _ => throw new InvalidOperationException($"Invalid service lifetime provided (Lifetime: '{lifetime}').")
    };
}