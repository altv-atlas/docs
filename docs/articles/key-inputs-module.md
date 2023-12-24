# Key Inputs Module

The key inputs module offers you the use of serverside key up and key down events which you can subscribe to and trigger your desired actions.
You can add specific keys to listen for that not every keystroke gets sent to the server.

[!INCLUDE [requirements](~/includes/requirements.md)]
## Getting started

Start by installing the latest version from Nuget.
You will need to install both server-side and client-side packages for the module to work properly.

![platform](https://img.shields.io/badge/SERVER--SIDE-15dbfc) [![nuget](https://img.shields.io/nuget/v/AltV.Atlas.KeyInputs.Server?style=for-the-badge)](https://www.nuget.org/packages/AltV.Atlas.KeyInputs.Server/)
![platform](https://img.shields.io/badge/CLIENT--SIDE-15dbfc) [![nuget](https://img.shields.io/nuget/v/AltV.Atlas.KeyInputs.Client?style=for-the-badge)](https://www.nuget.org/packages/AltV.Atlas.KeyInputs.Client/)

### Initialization
A quick and simple example:

> [!NOTE]
> These steps have to be done for both client-side and server-side!

```cs
var builder = Host.CreateDefaultBuilder( );

builder.ConfigureServices( (context, services) =>
{
    services.RegisterKeyInputModule( ); // <--- Register the key inputs module
} );
    
var host = builder.UseConsoleLifetime( ).Build( );
_ = host.Services.InitializeKeyInputModule( ); // <-- Initialize the key inputs module

await host.RunAsync();
```

``On client-side you have to provide a list of keys you want to listen for on module initialization.``

> [!TIP]
> If you still have problems initializing the module you can check out the scaleforms module example in our [boilerplate](https://github.com/altv-atlas/Boilerplate/blob/master/AltV.Atlas.Boilerplate.Client/Bootstrapper.cs)!

## Using the module
You can use the module both on client-side and server-side.

### Server-side

```cs
using AltV.Atlas.IoC.Attributes;
using AltV.Atlas.KeyInputs.Server.Events;
using AltV.Atlas.KeyInputs.Shared.Enums;
using AltV.Net.Elements.Entities;

[Injectable(InstantiateOnBoot = true)]
public class PlayerKeyDownEvent
{
    private readonly AtlasKeyInputEvents _atlasKeyInputEvents;

    public PlayerKeyDownEvent( AtlasKeyInputEvents atlasKeyInputEvents )
    {
        _atlasKeyInputEvents = atlasKeyInputEvents;
        _atlasKeyInputEvents.OnPlayerKeyDown += OnPlayerKeyDown;
    }

    private void OnPlayerKeyDown( IPlayer player, AtlasKey key )
    {
        if( key != AtlasKey.F4 )
            return;

        player.Kick();
    }
}

```
### Examples
You can find examples for all our modules in our boilerplates [client-side](https://github.com/altv-atlas/Boilerplate/tree/master/AltV.Atlas.Boilerplate.Client) and [server-side](https://github.com/altv-atlas/Boilerplate/tree/master/AltV.Atlas.Boilerplate.Server)