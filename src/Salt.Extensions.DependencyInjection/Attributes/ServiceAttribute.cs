namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Attribute class that when applied to an interface, marks the interface as available for registration using the
/// automatic assembly scanner.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
public sealed class ServiceAttribute : Attribute
{
}