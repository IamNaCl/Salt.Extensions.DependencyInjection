namespace Microsoft.Extensions.DependencyInjection;

delegate IServiceCollection AddServiceClassFunc(IServiceCollection services, Type implementationType);

delegate IServiceCollection AddServiceInterfaceFunc(IServiceCollection services, Type svcType, Type implType);

delegate IServiceCollection AddServiceFactoryFunc(IServiceCollection services,
                                                  Type type,
                                                  Func<IServiceProvider, object> factory);