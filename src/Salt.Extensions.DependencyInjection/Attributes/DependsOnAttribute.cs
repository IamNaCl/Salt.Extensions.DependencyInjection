namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Attribute class that when applied to a <see cref="AssemblyModule"/>, specifies the dependencies for the specified
/// assembly module.
/// </summary>
/// <remarks>
///   <list type="number">
///     <item>
///       The types marked as dependencies in this attribute must inherit from the <see cref="AssemblyModule"/> class,
///       otherwise a <see cref="InvalidDependencyException" /> will be thrown.
///     </item>
///     <item>
///       The types marked as dependencies cannot form a circular reference, otherwise a
///       <see cref="CircularDependencyException"/> will be thrown.
///     </item>
///   </list>
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public sealed class DependsOnAttribute : Attribute
{
    /// <summary>
    /// Gets all the assembly modules that the current assembly module needs to work correctly.
    /// </summary>
    public Type[] DependencyAssemblies { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DependsOnAttribute"/> class.
    /// </summary>
    /// <param name="dependencyAssemblies">Array of dependencies.</param>
    public DependsOnAttribute(params Type[] dependencyAssemblies) => DependencyAssemblies = dependencyAssemblies;
}