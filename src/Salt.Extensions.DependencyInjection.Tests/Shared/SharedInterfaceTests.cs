namespace Salt.Extensions.DependencyInjection.Tests.Shared;

[TestClass]
[TestCategory("Test methods for services that are to be shared across service types.")]
public class SharedInterfaceTests : BaseTestClass
{
    [TestMethod]
    public void SharedService_RegistersAsServiceInterfaces()
    {
        var service1 = ServiceProvider.GetRequiredService<ISharedService1>();
        var service2 = ServiceProvider.GetRequiredService<ISharedService2>();
        var impl = ServiceProvider.GetRequiredService<SharedService>();

        Assert.AreSame(impl, service1);
        Assert.AreSame(impl, service2);
    }

    [TestMethod]
    public void SharedService_IsNotRegisteredAsNonServiceInterface()
    {
        Assert.ThrowsException<InvalidOperationException>(() => ServiceProvider.GetRequiredService<INonService>());
    }

    [TestMethod]
    public void InterfaceService_RegistersAsAllInterfaces()
    {
        var service1 = ServiceProvider.GetRequiredService<INonServiceInterface>();
        var service2 = ServiceProvider.GetRequiredService<IServiceInterface1>();
        var impl = ServiceProvider.GetRequiredService<InterfaceService>();

        Assert.AreSame(impl, service1);
        Assert.AreSame(impl, service2);
    }
}