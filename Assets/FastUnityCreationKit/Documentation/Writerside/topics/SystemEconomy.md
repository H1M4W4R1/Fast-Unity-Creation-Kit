# Economy

Economy is a subsystem responsible for handling resources in the game e.g. coins, health or anything
that user can spend to gain something. It is a very important part of the game and should be designed
carefully to make the game interesting and challenging.

## Global and Local resources
Resources are divided into two types: global and local. Global resources are singletons (exists only once)
and are recommended to be used for resources that are shared between all players or for resources that are
shared between multiple runs in rouge-like games. 

Local resources are resources that are unique per object - for example a local resource could be health of a
entity or coins available for single vendor.

## Creating resources
To create a resource you need to create a new class that extends `Resource`-type class.

```C#
public sealed class CoinsResource : GlobalResource<CoinsResource, int32>
{

}
```

First generic parameter is reference to the class itself (required only for global resources) and the second
parameter is the type of the resource. In this case it is `int32` which means that the resource is an 32-bit
integer.

Similarly, you can create a local resource:

```C#
public sealed class HealthResource : LocalResource<int32>
{

}
```

## Using resources
To use a global resource you can use the following code:

```C#
CoinsResource.Instance.Value.Add(10);
```

or
```C#
EconomyAPI.GetGlobalResource<CoinsResource>().Add(10);
```

To use a local resource you have to inform the system that specified object supports the resource:

```C#
public class EntityWithHealth : MonoBehaviour, IWithLocalResource<HealthResource>
{
    // This is implementation of HealthResource class-inherited interface
    HealthResource IWithLocalResource<HealthResource>.ResourceStorage { get; } =
            new HealthResource();
}
```

Then you can use the resource like this:

```C#
var entity = new EntityWithHealth();
if(entity.TryGetLocalResource<HealthResource>(out HealthResource health))
{
    health.SetValue(100);
}
```

## Supported resource operations
* Add(value) - adds specified value to the resource
* Take(value) - takes specified value from the resource
* Subtract(value) - subtracts specified value from the resource (same as Take)
* SetValue(value) - sets the value of the resource to specified value
* Amount - returns the current value of the resource
* TryTake(value) - tries to take specified value from the resource and returns true if the operation was successful
* HasEnough(value) - returns true if the resource has enough value to take specified amount
* Reset() - resets the resource to its default value

## Support for IWithMinLimit, IWitMaxLimit and IWithDefaultValue
Numeric interfaces are fully supported by the resource system. You can simply implement them onto your
resource to limit the value of the resource or to set the default value.

```C#
public sealed class CoinsResource : GlobalResource<CoinsResource, int32>, 
IWithMinLimit<int32>, IWithMaxLimit<int32>, IWithDefaultValue<int32>
{
   public int32 DefaultValue => 1000;
   public int32 MaxLimit => 10000;
   public int32 MinLimit => 0;
}
```