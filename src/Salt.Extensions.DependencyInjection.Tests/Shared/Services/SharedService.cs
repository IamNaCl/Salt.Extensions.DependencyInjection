namespace Salt.Extensions.DependencyInjection.Tests.Shared;

[Service]
public interface ISharedService1 { }

[Service]
public interface ISharedService2 { }

public interface INonService { }

[Implementation(ServiceLifetime.Singleton, EnforceServiceInterfaces = true)]
public class SharedService : ISharedService1, ISharedService2, INonService
{

}