# Identification

Identification is a package that contains basic identifier and identifiable object framework.
Identifiers are `readonly structs` to ensure that they are immutable and can be used as keys.

## Core interfaces

### IIdentifier
Represents an identifier - a unique or non-unique identifier of an object.

### IUniqueIdentifier
Represents a unique identifier - an identifier that is unique within a context.

### INumberIdentifier
Represents a number identifier - an identifier that is a number as a value.
`TNumber` is a type of the number that inherits from `INumber` interface from
`Core.Numerics` package.

### IIdentifiable
Represents an object that can be identified by an identifier. `TIdentifier` is a type of the identifier
which inherits from `IIdentifier` interface.

### IUniqueIdentifiable
Represents an object that can be identified by a unique identifier. `TIdentifier` is a type of the identifier
which inherits from `IIdentifier` interface. This interface does not require `TIdentifier` to inherit from
`IUniqueIdentifier` interface as it is in programmer's responsibility to ensure that the identifier is unique
within a given context.

<warning>
While using IUniqueIdentifiable interface, it is programmer's responsibility to ensure that the identifier is unique within a given context.
</warning>

## Implemented identifiers

### Snowflake128
A 128-bit identifier that is generated using the Snowflake algorithm. It is guaranteed to be unique within a given context.
First 64 bits are a timestamp, next 32 bits are identifier data (e.g. worker ID), next 16 bits are another pack of
identifier data (e.g. process ID), next 8 bits are reserved and remaining 8 bits indicates if the identifier is null.

### Numeric identifiers
* ID8 - A simple 8-bit number used as an identifier.
* ID16 - A simple 16-bit number used as an identifier.
* ID32 - A simple 32-bit number used as an identifier.
* ID64 - A simple 64-bit number used as an identifier.

## Creating custom identifiers
You can also create custom identifiers by implementing `IIdentifier` interface.
An example of a custom identifier is shown below:

```C#
public readonly struct CustomIdentifier : IIdentifier
{
    // It is not recommended to use managed types within identifiers.
    // However this is just an example for demonstration purposes.
    public readonly string value;

    public CustomIdentifier(string value)
    {
        this.value = value;
    }
}
```

<note>
It is also recommended to implement IEquatable{TSelf} interface for custom identifiers.
</note>

<warning>
It is not recommended to use managed types within identifiers as it may lead to performance issues.
</warning>