# Vehicle Module

The vehicle module offer a base implementation of DI usage and Factories to create your own vehicle instances out of the box. It allows you to extend a basic implementation and even extend the factory to create your specific type.

[!INCLUDE [requirements](~/includes/requirements.md)]
## Getting started

Start by installing the latest version from Nuget.
You will need to install both server-side and client-side packages for the module to work properly.

[![nuget](https://img.shields.io/nuget/v/AltV.Atlas.Peds?style=for-the-badge)](https://www.nuget.org/packages/AltV.Atlas.Vehicles.Server/)
[![nuget](https://img.shields.io/nuget/v/AltV.Atlas.Peds.Client?style=for-the-badge)](https://www.nuget.org/packages/AltV.Atlas.Vehicles.Client/)

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

* AtlasBaseVehicle
* AtlasTuningVehicle
* AtlasPlayerVehicle

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
> If you want to extend the factory you should have your own vehicle class and register it as the default creation type in the RegisterVehicleModule method via one of its overloads!