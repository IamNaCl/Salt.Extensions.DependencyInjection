namespace Salt.Extensions.DependencyInjection.Tests;

public abstract class BaseTestClass
{
    public IServiceProvider ServiceProvider { get; }

    public BaseTestClass() =>
        ServiceProvider = new ServiceCollection().AddAssembly<BaseTestClass>().BuildServiceProvider();
}