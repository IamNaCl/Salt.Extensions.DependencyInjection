namespace Salt.Extensions.DependencyInjection.Tests.BaseClass;

[TestClass]
[TestCategory("Shared/Non Shared/Registration Test Methods for Services with Base classes.")]
public class BaseClassTests : BaseTestClass
{
    [TestMethod]
    public void SharedBaseClass_RegistersBothClasses()
    {
        var impl = ServiceProvider.GetRequiredService<SharedBaseClassService>();
        var baseClass = ServiceProvider.GetRequiredService<SharedBaseClassServiceBase>();

        // They're parent and child, but point to the same instance.
        Assert.AreSame(impl, baseClass);
    }

    [TestMethod]
    public void NonSharedBaseClass_RegistersBothClassesSeparately()
    {
        var impl = ServiceProvider.GetRequiredService<NonSharedBaseClassService>();
        var baseClass = ServiceProvider.GetRequiredService<NonSharedBaseClassServiceBase>();

        // They're parent and child, but point to the same instance.
        Assert.AreNotSame(impl, baseClass);
    }

    [TestMethod]
    public void BaseClass_DoesNotAddBaseClass()
    {
        var impl = ServiceProvider.GetRequiredService<BaseClassWithoutBaseService>();
        Action action = () => ServiceProvider.GetRequiredService<BaseClassWithoutBaseServiceBase>();

        Assert.IsNotNull(impl);
        Assert.ThrowsException<InvalidOperationException>(action);
    }
}