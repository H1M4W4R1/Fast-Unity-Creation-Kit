# Numerics

Numerics is a low-level API for working with numbers in Fast Unity Creation Kit. It provides a set of interfaces and classes that can be used to work with numbers in a more flexible way than the built-in C# types.
Thanks to included interfaces and primitive structures you can easily extend the functionality of the built-in types and create your own types that can be used in the same way as the built-in types.

## Concepts
The generic concept of the Numerics API is that all numbers can be cast to their underlying type and back without
any cost. This is done thanks to explicit structure implementations with pointer/reinterpret casting, which compiled by
IL2CPP compiler or Burst compiler will be optimized to the same code as built-in types.

For example:
```C#
public void Test()
{
    float32 a = 1.0f;
    float32 b = 2.0f;
    
    float32 c = a + b;
}
```
Will be compiled to same code as:
```C#
public void Test()
{
    float a = 1.0f;
    float b = 2.0f;
    
    float c = a + b;
}
```
Because at assembly-level types do not exist.
This allows for way more flexible numbers approach and `INumber` interface used to provide
an easy way to require a number as a generic type in your methods or classes.

## Interfaces
### INumber
This is base interface for all numbers in Numerics API. It provides basic arithmetic operations, however those 
operations are only used if direct number type is not known - if you perform an operation where
at least one of the operands is interface itself.

Those math operations requires both numbers to support generic version of `ISupportsFloatConversion` interface.

### ISupportsFloatConversion
Represents a number that can be converted to float (or double). This is used to provide a way to convert any number to float
or double, which is required for basic arithmetic operations on unknown types. This is especially useful for
`INumber` interface, where you can't know the exact type of the number and thus for modifiable values / modifiers.

### IWithMaxLimit
Represents that an object has maximum limit. This is not supported by numbers themselves, but is a numeric-related utility
interface that can be used to provide maximum limit for a number. An example usage can be status stacking with a limit of
stack count - like burning status can stack up to 5 times.

### IWithMinLimit
Same as `IWithMaxLimit`, but for minimum limit.

### IWithDefaultValue
Represents that an object has default value. This is not supported by numbers themselves, but is a numeric-related utility
interface that can be used to provide default value for a number. An example could be a modifiable value that has default value
like health points with default value of 100.

<warning>
In case of <b>IWithMaxLimit</b>, <b>IWithMinLimit</b> and <b>IWithDefaultValue</b> interfaces, you should implement them in your class
and provide custom logic for handling those limits and default values. The interfaces are only utility markers used 
to indicate your intentions.
</warning>

### IFloatingPointNumber
Marker interface indicating that number is floating point number.

### ISignedNumber
Marker interface indicating that number is signed number.

### IUnsignedNumber
Marker interface indicating that number is unsigned number.

### IVectorizedNumber
Marker interface indicating that number is vectorized number - tries to use SIMD/AVX instructions for calculations.

## Supported numbers
The implemented numbers are:
* `float32` - 32-bit floating point number with internal value of `float`
* `float64` - 64-bit floating point number with internal value of `double`
* `int8` - 8-bit signed integer with internal value of `sbyte`
* `int16` - 16-bit signed integer with internal value of `short`
* `int32` - 32-bit signed integer with internal value of `int`
* `int64` - 64-bit signed integer with internal value of `long`
* `uint8` - 8-bit unsigned integer with internal value of `byte`
* `uint16` - 16-bit unsigned integer with internal value of `ushort`
* `uint32` - 32-bit unsigned integer with internal value of `uint`
* `uint64` - 64-bit unsigned integer with internal value of `ulong`
* `v128` - 128-bit vectorized number with internal value of `int4`
* `v256` - 256-bit vectorized number with internal value of `int4x2`
* `v512` - 512-bit vectorized number with internal value of `int4x4`

<note>
If you're unfamiliar with <b>int4</b>, <b>int4x2</b> and <b>int4x4</b> types, they are numbers that are included in
Unity.Mathematics package and are used to provide SIMD/AVX instructions for calculations. They are not directly
supported by Numerics API, but are used to provide vectorized numbers.
</note>

