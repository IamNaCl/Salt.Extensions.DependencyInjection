# Salt.Extensions.DependencyInjection

Welcome to `Salt.Extensions.DependencyInjection`, a "no bs" assembly scanner extension for Microsoft's DI container,
just plug, register your assemblies into the service collection and play.

<sub>Note: If you want/need to have more granular experience, please check out
[Scrutor](https://github.com/khellang/Scrutor).</sub>

## Installation

Install the `Salt.Extensions.DependencyInjection` NuGet package on all the projects that should benefit from the
assembly scanning and service registration:

Package Manager Console:

    Install-Package Salt.Extensions.DependencyInjection

Or `dotnet` CLI:

    dotnet add package Salt.Extensions.DependencyInjection

In your executable project, make sure to add the `Microsoft.Extensions.DependencyInjection` package and create a service
collection to register your assemblies.

### How it works?

These are instructions for the simplest setup:

1. Create a service implementation and decorate it with the `Implementation` attribute:
    ```cs
    using Microsoft.Extensions.DependencyInjection;

    // This attribute marks the class as a service implementation, it also controls the registration behavior for
    // interfaces, check the ImplementationAttribute class.
    [Implementation(ServiceLifetime.Singleton)]
    public class MyService
    {
        // Service Implementation
    }
    ```

2. Register your assembly in the DI container:
    ```cs
    using Microsoft.Extensions.DependencyInjection;

    // ...
    var serviceCollection = ...;

    // This will scan the assembly where MyService is located and register all the types decorated with the
    // Implementation attribute.
    serviceCollection.AddAssembly<MyService>();

    // ...
    ```

3. To use it like normal:
    ```cs
    using Microsoft.Extensions.DependencyInjection;
    // ...

    var serviceProvider = serviceCollection.BuildServiceProvider();
    var myService = serviceProvider.GetRequiredService<MyService>();
    // Use myService as you see fit.

    // ...
    ```

### Why?

Well, I wanted a simple extension method, that's why this project is "no bs", meaning that there's no additional setup
other than adding the assembly types to the service collection.

This implementation also moves all the information about the service definition to the service implementation itself,
that way you don't have to open whatever file has all your `Add{Lifetime}()` methods.

And lastly, I started this project because it is a common practice for me to add some sort of automatic dependency
injection to my projects, and keep startup files as small as they can be, you can think of it as a readability
enhancement.

## License

License is MIT, please check the [`LICENSE`](./LICENSE) file for more information.