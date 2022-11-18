namespace Salt.Extensions.DependencyInjection.Tests.BaseClass;

public abstract class NonSharedBaseClassServiceBase { }

[Implementation(ServiceLifetime.Singleton, Shared = false)]
public class NonSharedBaseClassService : NonSharedBaseClassServiceBase
{

}