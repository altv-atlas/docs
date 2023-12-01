# Ped Traffic Module

Want to see random vehicles drive around in your server?
The ped traffic module offers a quick and easy implementation to enable synced ped traffic in your server!

[!INCLUDE [requirements](~/includes/requirements.md)]
## Getting started

> [!NOTE]
> This module REQUIRES the ped module AND vehicle module installed on both client and server-side.
> For more info, please visit [the ped module docs](~/articles/ped-module.md) and [the vehicle module docs](~/articles.vehicle-module.md).

Start by installing the latest version from our discord ([requires patreon Premium Member tier](https://www.patreon.com/AltvAtlas)).
You will need to install both server-side and client-side packages for the module to work properly.

Once you've downloaded both the client and server-side packages, you need to add them as reference to your C# projects.

Add the ``AltV.Atlas.Peds.Traffic.Server.dll`` to your server-side project, and the ``AltV.Atlas.Peds.Traffic.Client.dll`` to your client-side project. 

### Initialization
A quick and simple example:

> [!NOTE]
> These steps have to be done for both client-side and server-side!

```cs
var builder = Host.CreateDefaultBuilder( );

builder.ConfigureServices( (context, services) =>
{
    services.RegisterPedTrafficModule( ); // <--- Register the ped traffic module
} );
    
var host = builder.UseConsoleLifetime( ).Build( );

_ = host.Services.InitializePedTrafficModule(); // Initialize the ped traffic module

await host.RunAsync();
```

#### AppSettings

> [!TIP]
> We highly recommend you use our boilerplate for this module, as it has built-in appsettings.json loading and everything else you need to run this module.
> You can still use this module without our boilerplate, but you will have to add appsettings.json loading on your own.

Add the following to your appsettings.json (default settings):
```json
  "TrafficSettings": {
    "MaxTrafficVehiclesInStreamDistance": 30,
    "MinimumTrafficNodesToGenerate": 10,
    "SpawnRadius": 160,
    "MinimumSpawnDistanceFromPlayer": 80,
    "CleanupIntervalMs": 60000,
    "TrafficSpawnIntervalMs": 1000
  }
```

These values can be changed to modify the behaviour of our ped traffic module.

| Name  | Default | Description |
| ------------- | ------------- |
| MaxTrafficVehiclesInStreamDistance | 30 | Maximum amount of PED vehicles that can be within stream distance of a player |
| MinimumTrafficNodesToGenerate | 10 | Minimum amount of nodes it should generate near the player to spawn a vehicle at |
| SpawnRadius | 160 | Radius around the player at which a vehicle can spawn |
| MinimumSpawnDistanceFromPlayer | 80 | Minimum distance from the player before a position is considered valid for a ped to spawn at |
| CleanupIntervalMs | 60000 | The server does a cleanup of invalid peds/vehicles every X milliseconds. This can be adjusted with this value |
| TrafficSpawnIntervalMs | 1000 | The interval at which valid locations for traffic peds are calculated. If you see vehicles spawning on top of eachother, try increasing this value |
