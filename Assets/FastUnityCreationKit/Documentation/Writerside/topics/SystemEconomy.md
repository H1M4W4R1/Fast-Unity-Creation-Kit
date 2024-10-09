# Economy

Economy is a subsystem responsible for handling resources in the game e.g. coins, health or anything
that user can spend to gain something. It is a very important part of the game and should be designed
carefully to make the game interesting and challenging.

## Global and Local resources
Resources are divided into two types: global and local. Global resources are singletons (exists only once)
and are recommended to be used for resources that are shared between all players or for resources that are
shared between multiple runs in rouge-like games. 

Local resources are resources that are unique per object - for example a local resource could be health of an
entity or coins available for single vendor.

## Creating resources
To create a resource you need to create a new class that extends `Resource`-type class.

```C#
public sealed class CoinsResource : GlobalResource<CoinsResource>
{

}
```

The generic parameter is reference to the class itself (required only for global resources).
<note>All resources have base type of int32!</note>

Similarly, you can create a local resource:

```C#
public sealed class HealthResource : LocalResource
{

}
```

## Using resources
To use a global resource you can use the following code:

```C#
EconomyAPI.AddGlobalResource<CoinsResource>(10);
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
entity.AddLocalResource<HealthResource>(100);
entity.TakeLocalResource<HealthResource>(10);
```

<note>
All values provided in add/take etc. resource functions are floats and automatically converted to
the type of the resource. API assumes that resource underlying type is a number that implements
ISupportsFloatConversion interface.
</note>

## Context-based usage
You can also use resources in context-based way. This is useful when you want to use resources in a
specific context e.g. when you received gold by killing that monster, and you want to pass monster instance
to event invoked by the resource system.

```C#
public sealed class MonsterKilledResourceContext : IAddResourceContext
{
    // This is access point to local economy, will be automatically set by the system
    public IWithLocalEconomy Economy { get; set; } = null;
    
    // This is amount of gold that you want to add to the player
    public int32 Amount { get; set; } = 15;
    
    // This is the monster that was killed
    public Monster LinkedMonster { get; private set; }
    
    public MonsterKilledResourceContext(Monster monster)
    {
        LinkedMonster = monster;
    }
}
```
<warning>If you're using class as context base the Economy property will be automatically updated to last entity you've used!</warning>

Then you can use the context like this:
```C#
var monster = new Monster();
var context = new MonsterKilledResourceContext(monster);

// This will add 15 gold to the player indicating that the gold was received from killing the monster
// in event called by the resource system
EconomyAPI.AddGlobalResource<CoinsResource>(context);
```

## Supported resource operations
* Add(value) - adds specified value to the resource
* Take(value) - takes specified value from the resource
* SetValue(value) - sets the value of the resource to specified value
* Amount - returns the current value of the resource (placed in the resource object, you need to get it first)
* TryTake(value) - tries to take specified value from the resource and returns true if the operation was successful
* HasEnough(value) - returns true if the resource has enough value to take specified amount
* Reset() - resets the resource to its default value (placed in the resource object, you need to get it first)

## Support for IWithMinLimit, IWitMaxLimit and IWithDefaultValue
Numeric interfaces are fully supported by the resource system. You can simply implement them onto your
resource to limit the value of the resource or to set the default value.

```C#
public sealed class CoinsResource : GlobalResource<CoinsResource>, 
IWithMinLimit<int32>, IWithMaxLimit<int32>, IWithDefaultValue<int32>
{
   public int32 DefaultValue => 1000;
   public int32 MaxLimit => 10000;
   public int32 MinLimit => 0;
}
```