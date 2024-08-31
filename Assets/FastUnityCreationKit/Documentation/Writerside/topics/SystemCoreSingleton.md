# Singleton

Singleton is an easy way to create a class that has only one instance and provides a global point of access to it.

## Example implementations

```C#
public sealed class SingletonExample : ISingleton<SingletonExample>
{
    
}

public sealed class MonoSingletonExample : IMonoBehaviourSingleton<MonoSingletonExample>
{
    
}
```

## Acquiring the singleton instance

```C#
SingletonExample singleton = ISingleton<SingletonExample>.GetInstance();
MonoSingletonExample monoSingleton = IMonoBehaviourSingleton<MonoSingletonExample>.GetInstance();
```

This is way simpler than creating a singleton class with a static instance and a static method to access it.

<warning>
It is not recommended to add custom static Instance properties to singleton classes that implement the ISingleton interface
as it may lead to undefined behaviour as ISingleton instance already contains a protected static Instance property.

This implementation/usage is not supported and any issues related to it will not be addressed.
</warning>

