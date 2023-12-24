# Vehicle Module

The vehicle module offer a base implementation of DI usage and Factories to create your own vehicle instances out of the box. It allows you to extend a basic implementation and even extend the factory to create your specific type.

[!INCLUDE [requirements](~/includes/requirements.md)]
## Getting started

Start by installing the latest version from Nuget.
You will need to install both server-side and client-side packages for the module to work properly.

[![nuget](https://img.shields.io/nuget/v/AltV.Atlas.Vehicles.Server?style=for-the-badge)](https://www.nuget.org/packages/AltV.Atlas.Vehicles.Server/)
[![nuget](https://img.shields.io/nuget/v/AltV.Atlas.Vehicles.Client?style=for-the-badge)](https://www.nuget.org/packages/AltV.Atlas.Vehicles.Client/)

### Initialization
A quick and simple example:

> [!NOTE]
> These steps have to be done for both client-side and server-side!

```cs
var builder = Host.CreateDefaultBuilder( );

builder.ConfigureServices( (context, services) =>
{
    services.RegisterVehicleModule( ); // <--- Register the vehicle module
} );
    
var host = builder.UseConsoleLifetime( ).Build( );
_ = host.Services.InitializeVehicleModule( ); // <-- Initialize the vehicle module

await host.RunAsync();
```

### ONLY Server-side
If you consider extending the ``AtlasVehicleFactory`` dont forget to use the following overload:

```cs
var builder = Host.CreateDefaultBuilder( );

builder.ConfigureServices( (context, services) =>
{
    services.RegisterVehicleModule<ExtendedVehicleFactory>( ); // <--- Register the vehicle module with your own factory
} );
    
var host = builder.UseConsoleLifetime( ).Build( );
_ = host.Services.InitializeVehicleModule( ); // <-- Initialize the vehicle module

await host.RunAsync();
```

Additionally, make sure you override the altV vehicle factory with ours:
```cs
public override IEntityFactory<IVehicle> GetVehicleFactory( )
{
    return _bootstrapper.Value.Services.GetService<IEntityFactory<IAtlasVehicle>>( ); // Or wherever you've registered it
}
```
> [!TIP]
> If you still have problems initializing the module you can check out the vehicle module example in our [boilerplate](https://github.com/altv-atlas/Boilerplate/blob/master/AltV.Atlas.Boilerplate.Server/Bootstrapper.cs)!

## Using the module
In the vehicle module you have three implementations from us right out of the box which are as follows:

| Class  | Description  | Link |
| ------------- | ------------- |
| AtlasBaseVehicle | Default implementation with some extra functions and properties | https://github.com/altv-atlas/Vehicles.Server/blob/master/AltV/Entities/AtlasVehicleBase.cs |
| AtlasTuningVehicle | Offers additional functions and properties that make tuning easier | https://github.com/altv-atlas/Vehicles.Server/blob/master/Entities/AtlasTuningVehicle.cs |
| AtlasPlayerVehicle | Offers a properie to determin an owner | https://github.com/altv-atlas/Vehicles.Server/blob/master/Entities/AtlasPlayerVehicle.cs |


You can use those if they are fitting your needs or extend them as you can see in the following step.


### Creating a new vehicle class

### Server-side
On server-side you have to create a new class that inherits from the AtlasVehicleBase
```cs
using AltV.Atlas.Vehicles.Server.AltV.Entities;
using AltV.Net;

namespace AltV.Atlas.Boilerplate.Server.Features.Vehicles.Overrides;

public class ExtendedVehicle : AtlasVehicleBase
{
    public string NumberPlate
    {
        set => NumberplateText = value;
    }

    public ExtendedVehicle( ICore core, IntPtr nativePointer, uint id ) : base( core, nativePointer, id )
    {
        Console.WriteLine( "ITS AN EXTENDED VEHICLE!" );
    }
}
```

You can also inherit from our other vehicle implementations this is just an example.

### Extending the Atlas Vehicle Factory

### Server-side
You can also extend our default vehicle factory so you can add your own methods to create vehicles

> [!NOTE]
> If you extend the base vehicle factory dont forget to use the ``RegisterVehicleModule<ExtendedVehicleFactory>`` overload to register the module as stated above!
>(~/articles/vehicle-module.md#Initialization)


```cs
using AltV.Atlas.Vehicles.Server.Factories;
using AltV.Net;
using AltV.Net.Data;
using Microsoft.Extensions.Logging;
namespace AltV.Atlas.Boilerplate.Server.Features.Vehicles.Overrides;

public class ExtendedVehicleFactory( ILogger<AtlasVehicleFactory> logger, IServiceProvider serviceProvider ) : AtlasVehicleFactory( logger, serviceProvider )
{
    public async Task<ExtendedVehicle> CreateVehicleAsync( string model, Position position, Rotation rotation )
    {
        var vehicle = await CreateVehicleAsync<ExtendedVehicle>( Alt.Hash( model ), position, rotation );
        vehicle.NumberPlate = "We extended it";

        return vehicle;
    }
}
```

> [!IMPORTANT]
> You have to use the ``CreateVehicleAsync`` from the base factory for inheritance to work!

### Examples
You can find examples for all our modules in our boilerplates [client-side](https://github.com/altv-atlas/Boilerplate/tree/master/AltV.Atlas.Boilerplate.Client) and [server-side](https://github.com/altv-atlas/Boilerplate/tree/master/AltV.Atlas.Boilerplate.Server)