# Initialization

Initialization is a package that contains the `IInitializable` interface responsible for initializing objects in the game. 
Any initializable object should implement this interface and provide custom logic calls to request initialization when needed.

## Implementation
You need to implement the `IInitializable` interface in your class to make it initializable.
    
Example for regular C# class:
```C#
public class MyInitializable : IInitializable
{
    bool IInitializable.InternalInitializationStatusStorage { get; set; }

    void IInitializable.OnInitialize()
    {
        // Initialization logic
        Debug.Log("MyInitializable initialized");
    }
    
    public MyInitializable()
    {
        // Request initialization
        this.Initialize();
    }
}
```

Example for Unity MonoBehaviour:
```C#
public class MyInitializable : MonoBehaviour, IInitializable
{
    bool IInitializable.InternalInitializationStatusStorage { get; set; }

    void IInitializable.OnInitialize()
    {
        // Initialization logic
        Debug.Log("MyInitializable initialized");
    }
    
    private void Awake()
    {
        // Request initialization
        this.Initialize();
    }
}
```

Property InternalInitializationStatusStorage on the interface is used to store the initialization status of the object.
This is an internal property and should not be accessed directly, for that purposes you should use
`IsInitialized` property.

## Initialization with multiple entry paths
Initialization can be requested multiple times, but it will be executed only once.
This is useful when you need to initialize an object from multiple entry points, and
you don't know which one will be called first.

Also, if you want to avoid using this.Initialize() method you can use
`EnsureInitialized()` method that will initialize the object if it's not initialized yet.
This method is clearer and more readable in some cases - when you want to ensure that the object is initialized,
but you are not sure if it was already initialized.

Using `EnsureInitialized()` method requires casing the object to `IInitializable` interface.