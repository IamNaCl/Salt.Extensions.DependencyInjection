using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class ServiceCollectionAutomaticExtensions
{
    /// <summary>
    /// Checks whetehr a given assembly has been added to the service collection.
    /// </summary>
    /// <param name="assembly">Assembly used for lookup.</param>
    /// <returns>
    /// <c>true</c> if the provided assembly has been added to the service collection, otherwise <c>false</c>.
    /// </returns>
    private static bool IsAssemblyRegistered(IServiceCollection services, Assembly assembly) =>
        services.Any(_ => _.ServiceType == typeof(Assembly) && assembly.Equals(_.ImplementationInstance));

    /// <summary>
    /// Adds the base class of the provided implementation type.
    /// </summary>
    /// <param name="services">Service collection where all the services will be added.</param>
    /// <param name="lifetime">Service lifetime.</param>
    /// <param name="impl">Implementation type.</param>
    /// <param name="shared">Whether to share the same instance of a service for all interfaces.</param>
    private static void AddBaseClass(IServiceCollection services, ServiceLifetime lifetime, Type impl,
                                     bool shared, bool includeBaseClass)
    {
        var addFactoryFunc = lifetime.GetAddServiceFactoryFunc();
        var addSvcImplFunc = lifetime.GetAddServiceInterfaceFunc();
        var baseClass = impl.BaseType;

        if (includeBaseClass && baseClass is object)
        {
            if (shared)
                addFactoryFunc.Invoke(services, baseClass, _ => _.GetRequiredService(impl));
            // This is the first time I've seen an actual else case make sense - Weird flex but ok lol
            else
                addSvcImplFunc.Invoke(services, baseClass, impl);
        }
    }

    /// <summary>
    /// Adds all the service interfaces defined for a given service.
    /// </summary>
    /// <param name="services">Service collection where all the services will be added.</param>
    /// <param name="lifetime">Service lifetime.</param>
    /// <param name="implType">Implementation type.</param>
    /// <param name="interfaces">Collection of interfaces.</param>
    /// <param name="shared">Whether to share the same instance of a service for all interfaces.</param>
    private static void AddInterfaces(IServiceCollection services, ServiceLifetime lifetime, Type implType,
                                      Type[] interfaces, bool shared)
    {
        var addFactoryFunc = lifetime.GetAddServiceFactoryFunc();
        var addSvcImplFunc = lifetime.GetAddServiceInterfaceFunc();

        // The default action just invokes the Add{Lifetime}() method.
        Action<Type, Type> addFunc = (service, impl) => addSvcImplFunc.Invoke(services, service, impl);
        // But if the instance should be shared, the function will use a factory that gets the service type.
        if (shared)
            addFunc = (svc, impl) => addFactoryFunc.Invoke(services, svc, sp => sp.GetRequiredService(implType));

        // Then register all interfaces for the given type.
        foreach (var itf in interfaces)
            addFunc.Invoke(itf, implType);
    }

    /// <summary>
    /// Adds all the service implementations found on a given assembly.
    /// </summary>
    /// <param name="services">Service collection to register the services.</param>
    /// <param name="implementations">Implementation types.</param>
    private static void AddImplementations(IServiceCollection services, IEnumerable<Type> implementations)
    {
        foreach (var implType in implementations)
        {
            var attr = implType.GetCustomAttribute<ImplementationAttribute>();
            // Can we turn this into a span of sorts?
            var interfaces = implType.GetServiceInterfaces(attr.EnforceServiceInterfaces).ToArray();

            // Register the implementation itself, regardless of interfaces.
            var addImplFunc = attr.Lifetime.GetAddServiceClassFunc();
            addImplFunc.Invoke(services, implType);

            // Registers the base class, if present.
            AddBaseClass(services, attr.Lifetime, implType, attr.Shared, attr.WithBaseClass);

            // Then register all the interfaces.
            AddInterfaces(services, attr.Lifetime, implType, interfaces, attr.Shared);
        }
    }
}