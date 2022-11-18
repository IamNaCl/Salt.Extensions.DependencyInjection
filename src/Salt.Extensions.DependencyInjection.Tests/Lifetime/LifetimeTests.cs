namespace Salt.Extensions.DependencyInjection.Tests.Lifetime;

[TestClass]
[TestCategory("Lifetime Tests for Service Implementations.")]
public class LifetimeTests : BaseTestClass
{
    [TestMethod]
    public void SingletonService_UseSameInstance()
    {
        var service1 = ServiceProvider.GetRequiredService<SingletonService>();
        var service2 = ServiceProvider.GetRequiredService<SingletonService>();

        Assert.AreSame(service1, service2);
    }

    [TestMethod]
    public void TransientService_UseDifferentInstances()
    {
        var service1 = ServiceProvider.GetRequiredService<TransientService>();
        var service2 = ServiceProvider.GetRequiredService<TransientService>();

        Assert.AreNotSame(service1, service2);
    }

    [TestMethod]
    public void ScopedService_AreDifferentAcrossScopes()
    {
        using var scope1 = ServiceProvider.CreateScope();
        var scope1Service1 = scope1.ServiceProvider.GetRequiredService<ScopedService>();
        var scope1Service2 = scope1.ServiceProvider.GetRequiredService<ScopedService>();

        Assert.AreSame(scope1Service1, scope1Service2);

        using var scope2 = ServiceProvider.CreateScope();
        var scope2Service1 = scope2.ServiceProvider.GetRequiredService<ScopedService>();
        var scope2Service2 = scope2.ServiceProvider.GetRequiredService<ScopedService>();

        Assert.AreSame(scope2Service1, scope2Service2);

        Assert.AreNotSame(scope1Service1, scope2Service1);
    }
}