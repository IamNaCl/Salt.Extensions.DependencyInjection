namespace Salt.Extensions.DependencyInjection.Tests.Shared;

public interface INonServiceInterface { }

[Service]
public interface IServiceInterface1 { }

[Implementation(ServiceLifetime.Singleton)]
public class InterfaceService : INonServiceInterface, IServiceInterface1
{

}