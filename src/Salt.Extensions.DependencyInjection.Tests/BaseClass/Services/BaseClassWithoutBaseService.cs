namespace Salt.Extensions.DependencyInjection.Tests.BaseClass;

public abstract class BaseClassWithoutBaseServiceBase { }

[Implementation(ServiceLifetime.Singleton, WithBaseClass = false)]
public class BaseClassWithoutBaseService : BaseClassWithoutBaseServiceBase
{

}