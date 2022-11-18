namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Attribute class that when applied to a class or struct, marks the type as a service implementation that can be
/// automatically discovered during the assembly scan.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class ImplementationAttribute : Attribute
{
    /// <summary>
    /// Gets the lifetime defined for this service implementation.
    /// </summary>
    public ServiceLifetime Lifetime { get; }

    /// <summary>
    /// Gets or sets whether the same instance of this implementation should be shared across all interfaces that are
    /// decorated with the <see cref="ServiceAttribute"/> class or not (defaults to <c>true</c>).
    /// </summary>
    /// <value>
    /// If <c>true</c> all service interfaces decorated with the <see cref="ServiceAttribute"/> class will share the
    /// same instance of the implementation, otherwise, every service will have its own instance.
    /// </value>
    public bool Shared { get; set; } = true;

    /// <summary>
    /// Gets or sets whether only interfaces decorated with the <see cref="ServiceAttribute"/> class will be registered
    /// to the service collection.
    /// </summary>
    public bool EnforceServiceInterfaces { get; set; } = false;

    /// <summary>
    /// Gets or sets whether the base class of this type should be registered (defaults to <c>true</c>).
    /// </summary>
    public bool WithBaseClass { get; set; } = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImplementationAttribute" /> class.
    /// </summary>
    /// <param name="lifetime">Lifetime of the service implementation.</param>
    public ImplementationAttribute(ServiceLifetime lifetime) => Lifetime = lifetime;
}