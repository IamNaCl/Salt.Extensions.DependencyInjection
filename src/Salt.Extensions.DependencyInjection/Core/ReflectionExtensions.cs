using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Contains extension methods related to reflection.
/// </summary>
static class ReflectionExtensions
{
    /// <summary>
    /// Gets all the implementation types from a given assembly.
    /// </summary>
    /// <param name="assembly">Assembly to get the implementation types from.</param>
    /// <returns>Enumerable of <seealso cref="Type"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="assembly"/> is null.</exception>
    public static IEnumerable<Type> GetImplementationTypes(this Assembly assembly)
    {
        if (assembly is null)
            throw new ArgumentNullException(nameof(assembly));

        // FIXME: Do we want to support structs here?
        return assembly.GetTypes()
                       .Where(_ => _.IsClass && !_.IsAbstract
                                && Attribute.IsDefined(_, typeof(ImplementationAttribute)));
    }

    /// <summary>
    /// Gets all the service interfaces defined for the given type.
    /// </summary>
    /// <param name="type">Type to get the interfaces from.</param>
    /// <param name="enforceServiceAttr">
    /// If <c>true</c>, filters out all the non-service definition interfaces, otherwise all interfaces are returned.
    /// </param>
    /// <returns>Enumerable of <seealso cref="Type"/>.</returns>
    public static IEnumerable<Type> GetServiceInterfaces(this Type type, bool enforceServiceAttr = true)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        var interfaces = type.GetInterfaces().AsEnumerable();
        if (enforceServiceAttr)
            interfaces = interfaces.Where(t => Attribute.IsDefined(t, typeof(ServiceAttribute)));

        return interfaces;
    }
}