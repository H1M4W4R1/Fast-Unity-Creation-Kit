# Status

Status is a subsystem responsible for handling status effects like poison, slowness etc.
In general status effects are considered to be either permanent or temporary. Permanent status effects are
applied to the entity and stay there until removed.

## Object with status
The core of the system is `IObjectWithStatus` interface which is responsible for representing an object that can have status effects.
It contains methods to apply, remove, update and check status effects.

To create a custom object with status you need to create a class that implements the `IObjectWithStatus` interface.
```C#
public sealed class ExampleEntity : IObjectWithStatus
{
    // Entity implementation
}
```

<note>
It is recommended to use the `IObjectWithStatus` interface with a generic type to indicate that the entity supports specific status,
however it is not required and may get ugly when you have a lot of status effects.

Alternatively you can use IWithBannedStatus interface to indicate that the entity does not support specific status
(this exists as alternative).
</note>

## Status effect
The core of the system is IStatus interface which is responsible for representing a status effect.
It contains callbacks which are called when the status is applied, removed or updated.

To create a custom status effect you need to create a class that implements the `IStatus` interface.
```C#
public sealed class StunnedStatus : IStatus
{
    public void OnStatusAdded(IObjectWithStatus entity)
    {
        // Apply entity stun
    }

    public void OnStatusRemoved(IObjectWithStatus entity)
    {
        // Remove entity stun
    }
}
```

This type of status is considered to be a boolean-type status which can be either applied or removed,
however cannot be stacked or 'filled'.

## Percentage status effect
The percentage status effect is a type of status effect that is applied to a value and modifies it by a percentage.
This type of status effect can be used for any status that has a duration (or any other percentage-based effect).

It has additional two callbacks that are called when the percentage reaches the minimum or maximum value.

To create a custom percentage status effect you need to create a class that implements `IPercentageStatus` and implement the abstract methods.
```C#
// Overcharge is a status that grants entity a temporary speed increase by 50%
// If entity kills an enemy while overcharged, the percentage is increased by 10%
// If entity overcharges to 100%, it gets stunned until the percentage drops to 0%
public sealed class OverChargeStatus : IPercentageStatus
{
    float IPercentageStatus.Percentage { get; set; }

    public override void OnStatusAdded(IObjectWithStatus entity)
    {
        // Apply entity speed increase by 50%
    }

    public override void OnStatusRemoved(IObjectWithStatus entity)
    {
        // Remove entity speed increase        
    }
    
    public void OnMaxPercentageReached(IObjectWithStatus objectWithStatus)
    {
        // Apply entity stun status
    }
    
    public void OnMinPercentageReached(IObjectWithStatus objectWithStatus)
    {
        // Remove entity stun status
    }
}
```

<note>
Percentage status is limited to 0-100% range.
</note>

## Stackable status effect
The stackable status effect is a type of status effect that can be stacked on top of each other.
This type of status effect can be used for any status that can be stacked or 'filled' where a
good example would be a speed buff that can be stacked multiple times.

It has a few additional callbacks that are called when status stack is changed, or maximum/minimum stack is reached.
Stack can go negative, however, it is not recommended to use it in that way.

To create a custom stackable status effect you need to create a class that implements `IStackableStatus` and implement the abstract methods.
```C#
// SpeedBoost is a status that grants entity a temporary speed increase by 10%
// If entity kills an enemy while speed boosted, the stack is increased by 1
// If entity gets hit while speed boosted, the stack is decreased by 1
public sealed class SpeedBoostStatus : IStackableStatus
{
    int32 IStackableStatus.StackCount { get; set; }

    public override void OnStatusAdded(IObjectWithStatus entity)
    {
        // Do nothing, stack changed callback will be called
    }

    public override void OnStatusRemoved(IObjectWithStatus entity)
    {
        // Do nothing, stack changed callback will be called
    }

    public void OnStackChanged(IObjectWithStatus objectWithStatus, int amount)
    {
        // Apply entity speed increase or decrease depending on the amount
    }

    public void OnMaxStackReached(IObjectWithStatus objectWithStatus)
    {
    }

    public void OnMinStackReached(IObjectWithStatus objectWithStatus)
    {
    }
}
```

<note>
Stack changed callback is called also when the status is added (then it has one stack).
</note>
<note>
When status is removed by force all StackChanged callbacks are called with respective values.
</note>

## Stackable Percentage status effect
Sometimes you want to combine properties of stackable and percentage status effects.
This can be good for example for poison status which deals damage over time and can be stacked, but
also is reduced over time.

<warning>
Do not create stackable percentage status effects without IStackablePercentageStatus interface as it
may lead to issues with the system - IStackablePercentageStatus interface implements logic for a few methods from
other interfaces and is required for proper functionality that will meet your expectations.
</warning>

To create a custom stackable percentage status effect you need to create a class that implement `IStackablePercentageStatus` and implement the abstract methods.
```C#
// Poison is a status that deals 10% of entity health as damage over time
public sealed class PoisonStatus : IStackablePercentageStatus
{
    int32 IStackableStatus.StackCount { get; set; }
    float IPercentageStatus.Percentage { get; set; }

    public override void OnStatusAdded(IObjectWithStatus entity)
    {
    }

    public override void OnStatusRemoved(IObjectWithStatus entity)
    {
    }

    public void OnStackChanged(IObjectWithStatus objectWithStatus, int amount)
    {
    }

    public void OnMaxStackReached(IObjectWithStatus objectWithStatus)
    {
    }

    public void OnMinStackReached(IObjectWithStatus objectWithStatus)
    {
    }
}
```

<warning>
Stackable percentage status percentage is limited to 0-100% range. Stack is not limited,
however negative stack behaviour is considered undefined and should be avoided as any
issues related to it probably won't be addressed.
</warning>

<note>
If stackable status percentage gets up to 100% then the percentage is reset to 0% and stack is increased by 1.
Same applies if percentage is decreased to 0% then stack is decreased by 1 and percentage is reset to 100% unless
stack is 0.
</note>

## Automatic status removal

Statues are removed automatically if:
- Status is a percentage status and percentage is 0% after update.
- Status is a stackable status and stack is 0 after update.
- Status is a stackable percentage status and percentage reaches 0% and stack is 0 after update.

## Applying status
To apply a status to an entity you need to call the `ApplyStatus` method on the entity.
```C#   
IObjectWithStatus entity = ...;
IStatus status = new StunnedStatus();
entity.AddStatus(status);
```

## Removing status
To remove a status from an entity you need to call the `RemoveStatus` method on the entity.
```C#
IObjectWithStatus entity = ...;
entity.RemoveStatus<StunnedStatus>();
```

## Updating status
To update a status you need to call the respective method on the entity.
```C#
IObjectWithStatus entity = ...;
entity.IncreaseStatusPercentage<OverChargeStatus>(10);
entity.DecreaseStatusPercentage<OverChargeStatus>(10);
entity.IncreaseStatusStack<SpeedBoostStatus>(1);
entity.DecreaseStatusStack<SpeedBoostStatus>(1);
entity.IncreaseStatusStack<PoisonStatus>(1);
entity.DecreaseStatusStack<PoisonStatus>(1);
entity.IncreaseStatusPercentage<PoisonStatus>(10);
entity.DecreaseStatusPercentage<PoisonStatus>(10);
```

<note>
If status is not present on the entity, the methods will automatically apply it if necessary.
</note>

## Checking status
To check if an entity has a status you need to call the `HasStatus` method on the entity.
```C#
IObjectWithStatus entity = ...;
bool isStunned = entity.HasStatus<StunnedStatus>();
```

## Getting status instance
To get the status instance from the entity you need to call the `GetStatus` method on the entity.
```C#
IObjectWithStatus entity = ...;
IStatus status = entity.GetStatus<StunnedStatus>();
```

## Getting status stack count
To get the status stack count from the entity you need to call the `GetStatusStackCount` method on the entity.
```C#
IObjectWithStatus entity = ...;
int stackCount = entity.GetStatusStackCount<SpeedBoostStatus>();
```

There is also an alternative way to get the status stack count from the entity.
```C#
IObjectWithStatus entity = ...;
int stackCount = entity.GetAmountOfTimesStatusIsAdded<SpeedBoostStatus>();
```

<note>
This method allows to check either for stackable or non-stackable status.
</note>

## Getting status percentage
To get the status percentage from the entity you need to call the `GetStatusPercentage` method on the entity.
```C#
IObjectWithStatus entity = ...;
float percentage = entity.GetStatusPercentage<OverChargeStatus>();
```

## Advanced status support checking
Also, a custom interface exists to indicate that entity supports specific status.
```C#
public class ExampleEntity : IObjectWithStatus<StunnedStatus>
{
    // Entity implementation
}
```

This allows to use an additional method to check if the entity supports the status.
```C#
IObjectWithStatus entity = ...;
bool canBeStunned = entity.IsStatusExplicitlySupported<StunnedStatus>();
```

Also, a simple method to check if the entity supports status at all (checks only if the
status is not banned, not if it is explicitly supported via `IObjectWithStatus` generic interface).
```C#
IObjectWithStatus entity = ...;
bool canHaveStatus = entity.IsStatusSupported<StunnedStatus>();
```

<note>
This can be useful when you want to check if the entity supports a specific status without checking the type.
For example: when your boss cannot be stunned, but other entities can.
</note>

Also, a custom interface exists to indicate that entity does not support specific status.
```C#
public class ExampleEntity : IWithBannedStatus<StunnedStatus>
{
    // Entity implementation
}
```

<warning>
IWithBannedStatus takes precedence over IObjectWithStatus, so even if entity indicates that status is
supported, it will not be applied if IWithBannedStatus for the status exists, even if it is deeper
in the inheritance hierarchy.
</warning>