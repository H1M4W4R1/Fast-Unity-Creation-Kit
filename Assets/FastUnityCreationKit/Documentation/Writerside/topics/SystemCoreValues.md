# Values

Values is a subsystem for handling custom values assigned to objects. Even if it supports adding static
values to object instances, it is mainly used for dynamic values that are calculated on the fly.

## Low-level Interfaces
* IValue - represents a value and has a property to return the current value.
* IModifiableValue - represents a value that can be modified on the fly either by arithmetic or modifiers.
* IModifier - represents a modifier that can be applied to a value.

## High-level API
### StaticValue
An abstract class that takes `INumber` as a type parameter. It is used to represent a non-changing value
assigned to an object. It is rarely used and was implemented to provide compatibility with the dynamic
values via the `IValue` interface.

<note>
It is highly recommended to avoid using `StaticValue` and use `ModifiableValue` instead.
</note>

### ModifiableValue
An abstract class that takes `INumber` as a type parameter. It is used to represent a value that can be
modified on the fly. It is the most commonly used value type.

It has been designed to allow for easy modification of the value by using arithmetic operations or
applying modifiers. This value type is used by features like Resources, Stats, and other dynamic values.

<note>
It is heavily recommended to use `ModifiableValue` for any entity property like speed, max health etc.
as it allows for easy modification and handling of modifiers that can be applied to the value by
things like buffs, debuffs, equipment, etc.
</note>

## Creating a custom value
To create a custom value, you need to create a class that inherits from `ModifiableValue` and
implement the abstract methods.

```C#
public sealed class MaxHealthValue : ModifiableValue<int32>
{

}
```

You can also set up an initial value in the constructor, however that approach is not recommended.

Value set in the constructor:
```C#
public sealed class MaxHealthValue : ModifiableValue<int32>
{
    public MaxHealthValue()
    {
        SetBaseValue(100);    
    }
}
```

<note>
It is recommended to set the initial value using IWithDefaultValue interface from the Numerics package.
</note>

Example of setting the initial value using IWithDefaultValue:
```C#
public sealed class MaxHealthValue : ModifiableValue<int32>, IWithDefaultValue<int32>
{
    public MaxHealthValue()
    {
        SetBaseValue(this.GetDefaultValue());
    }

    public int32 DefaultValue => 100;
}
```

Of course according to that also the `IWithMinLimit` and `IWithMaxLimit` interfaces can be used to set the limits of the value.
Limits are respected both by arithmetic operations and modifiers.

Example value with limits:
```C#
    public class ExampleLimitedValue : ModifiableValue<float32>, IWithMinLimit<float32>, IWithMaxLimit<float32>
    {
        public float32 MinLimit => -10f;
        public float32 MaxLimit => 10f;
    }
```

## Modifiers
Modifiers are used to modify the value of a `ModifiableValue`. They can be added and removed at any time.
There are some basic modifiers implemented:
* AddFlatModifier - adds a flat value to the value (is executed as first modifier with priority 536_870_912)
* MultiplyModifier - multiplies the value by a given factor (is executed as second modifier with priority 1_073_741_824)
* AddModifier - adds a value to the value (is executed as third modifier with priority 2_147_483_648)

All modifiers support IWithPriority interface to set the priority of the modifier. The lower the priority value, the earlier the modifier is applied.
Those classes are abstract and should be inherited to create custom modifiers (to give context to the modifier).

Example of a custom modifier:
```C#
public sealed class ExampleModifier : AddModifier<float32>
{
    public ExampleModifier(float32 value) : base(value)
    {
    }
}
```

Example usage:
```C#
var maxHealth = new MaxHealthValue();
maxHealth.AddModifier(new ExampleModifier(10));
```

This allows for easy interpretation of the modifiers (unfortunately at quite large overhead cost, but nothing
forbids you to create a few simple modifiers and use them in the code instead of having modifier for each different
context).

Removing modifiers is also possible:
```C#
maxHealth.RemoveModifier<ExampleModifier>();
```

## Advanced modifier creation
You can also create way more advanced modifiers with custom logic. To do that you need to create a class that
implements the `IModifier` interface.

```C#
public sealed class ExampleModifier : IModifier<float32>
{
    public uint Priority => 3_000_000_000; // Last in the queue (usually)
    
     public void Apply<TNumberType>(IModifiableValue<TNumberType> value) 
            where TNumberType : struct, INumber
        {
            if(value is not MaxHealthValue)
                return;
        
            if(amount is TNumberType valueToAdd)
                value.Multiply((float32) 0.5f);
        }

        public void Remove<TNumberType>(IModifiableValue<TNumberType> value) 
            where TNumberType : struct, INumber
        {
            if(value is not MaxHealthValue)
                return;
        
            if(amount is TNumberType valueToAdd)
                value.Multiply((float32) 2f);   
        }

        public int CompareTo(IPrioritySupport other) => Priority.CompareTo(other.Priority);
}
```

This example is a purely custom modifier that halves the value of the `MaxHealthValue` when applied and doubles it when removed.
It also can be applied only to the `MaxHealthValue` modifiable value thanks to additional safety check.

## Arithmetic operations
All arithmetic operations are supported by the `ModifiableValue` class. They are implemented as methods that
take a value to apply the operation to.

```C#
var maxHealth = new MaxHealthValue();
maxHealth.Add(10);
maxHealth.Multiply(2);
maxHealth.Divide(2);
maxHealth.Subtract(10);
```

<warning>
<b>Modifiers and arithmetic operations are not compatible!</b>

Arithmetic operations are designed to modify values that are somewhat predictable and can be easily
calculated. Modifiers are designed to modify values in a more complex dynamic way with custom logic.
</warning>

<note>
A good example for arithmetic operations is the Economy subsystem where you can easily add or subtract
resource value thanks to it being a ModifiableValue.
</note>

## Conditionally removable modifiers
There are also some cases when you want to remove a modifier for example after a certain time. To do that
you can use the `IConditionallyRemovable` interface from Core.Utility package.

```C#
public sealed class ExampleModifier : AddModifier<float32>, IConditionallyRemovable
{
    public ExampleModifier(float32 value) : base(value)
    {
    }

    public bool IsRemovalConditionMet()
    {
        // This is just an example, you can use any condition here
        // it's just for god sake simplicity, you can also take custom
        // time from your game manager or any other source
        return DateTime.UtcNow.Year > 2025;
    }
}
```

This way you can easily remove the modifier after a certain time or when any other condition is met.
Dynamically removable modifiers are always validated when the modifier is applied or removed or
when the value is acquired.