# Scaleforms Module

The scaleforms module offers you the option to draw/hide gta scaleforms for the specific player.
At the moment its only possible to draw an industrial button menu but we already have more ideas for the future.

[!INCLUDE [requirements](~/includes/requirements.md)]
## Getting started

Start by installing the latest version from Nuget.
You will need to install both server-side and client-side packages for the module to work properly.

![platform](https://img.shields.io/badge/SERVER--SIDE-15dbfc) [![nuget](https://img.shields.io/nuget/v/AltV.Atlas.Scaleforms.Server?style=for-the-badge)](https://www.nuget.org/packages/AltV.Atlas.Scaleforms.Server/)
![platform](https://img.shields.io/badge/CLIENT--SIDE-15dbfc) [![nuget](https://img.shields.io/nuget/v/AltV.Atlas.Scaleforms.Client?style=for-the-badge)](https://www.nuget.org/packages/AltV.Atlas.Scaleforms.Client/)

### Initialization
A quick and simple example:

> [!NOTE]
> This has to be done on client-side!

```cs
var builder = Host.CreateDefaultBuilder( );

builder.ConfigureServices( (context, services) =>
{
    services.RegisterScaleformModule( ); // <--- Register the scaleforms module
} );
    
var host = builder.UseConsoleLifetime( ).Build( );
_ = host.Services.InitializeScaleformModule( ); // <-- Initialize the scaleforms module

await host.RunAsync();
```

> [!TIP]
> If you still have problems initializing the module you can check out the scaleforms module example in our [boilerplate](https://github.com/altv-atlas/Boilerplate/blob/master/AltV.Atlas.Boilerplate.Client/Bootstrapper.cs)!

## Using the module
You can use the module both on client-side and server-side.

### Client-side

```cs
    var myButtons = List<IndustrialButton>( )
    {
        new IndustrialButton
        {
            InstructionalButtonsString = Alt.Natives.GetControlInstructionalButtonsString( 0, 21, true ),
            DisplayText = "Atlas"
        }
    };

    AtlasScaleform.DrawIndustrialMenuEveryTick( myButtons ); // this has to be used when you are not in an everytick

```

### Server-side

```cs
    var myButtons = List<IndustrialButton>( )
    {
        new IndustrialButton
        {
            InstructionalButtonsString = Alt.Natives.GetControlInstructionalButtonsString( 0, 21, true ),
            DisplayText = "Atlas"
        }
    };

    player.DrawIndustrialMenuScaleform( myButtons );

    //if you later wanna hide the menu you can call

    player.HideIndustrialMenuScaleform();

```
### Examples
You can find examples for all our modules in our boilerplates [client-side](https://github.com/altv-atlas/Boilerplate/tree/master/AltV.Atlas.Boilerplate.Client) and [server-side](https://github.com/altv-atlas/Boilerplate/tree/master/AltV.Atlas.Boilerplate.Server)