namespace Salt.Extensions.DependencyInjection.Tests.BaseClass;

public abstract class SharedBaseClassServiceBase { }

[Implementation(ServiceLifetime.Singleton)]
public class SharedBaseClassService : SharedBaseClassServiceBase
{

}