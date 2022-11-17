using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods for the <seealso cref="IServiceCollection" /> data type.
/// </summary>
public static partial class ServiceCollectionAutomaticExtensions
{
    /// <summary>
    /// Scans the assembly of the provided <paramref name="assembly" /> for service implementations.
    /// </summary>
    /// <param name="services">
    /// Instance of <see cref="IServiceCollection"/> where the services will be registered.
    /// </param>
    /// <param name="assembly">Assembly to scan for services.</param>
    /// <returns>This instance of <see cref="IServiceCollection"/> for chained calls.</returns>
    /// <remarks>
    /// No changes will be made if the provided assembly has been already registered.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="services"/> or <paramref name="assembly"/> is null.
    /// </exception>
    public static IServiceCollection AddAssembly(this IServiceCollection services, Assembly assembly)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        // Assembly is already registered, no need to do anything.
        if (IsAssemblyRegistered(services, assembly))
            return services;

        AddImplementations(services, assembly.GetImplementationTypes());

        // Register the assembly at the end.
        services.AddSingleton<Assembly>(assembly);
        return services;
    }

    /// <summary>
    /// Scans the assembly of the type provided in the <typeparamref name="T"/> type parameter for service
    /// implementations.
    /// </summary>
    /// <param name="services">
    /// Instance of <see cref="IServiceCollection"/> where the services will be registered.
    /// </param>
    /// <typeparam name="T">Data type of the assembly module to be added.</typeparam>
    /// <returns>This instance of <see cref="IServiceCollection"/> for chained calls.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="services"/> is null.</exception>
    /// <remarks>
    /// No changes will be made if the provided assembly has been already registered.
    /// </remarks>
    public static IServiceCollection AddAssembly<T>(this IServiceCollection services)  =>
        AddAssembly(services, typeof(T).Assembly);
}