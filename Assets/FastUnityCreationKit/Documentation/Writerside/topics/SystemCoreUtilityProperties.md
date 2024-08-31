# Properties and configuration

Properties are used to define a generic structure for basic object information, especially things like names,
description, icons, prefabs, etc. This system is used to manage the properties of objects in the game.

## Concepts
All properties have a context in which they exist. The context is a class that implements the `IUsageContext` interface.
Thanks to this, the object can have multiple names/descriptions/icons/prefabs, etc. depending on the context in which it is used.

<note>
With the properties system you can have both an icon for a enemy that is displayed in the quest log and a image
that is displayed in bestiary.
</note>

## Usage
To use the properties system, you need to create a class that implements the `IUsageContext` interface.

```C#
public class BestiaryContext : IUsageContext{}

public class QuestLogContext : IUsageContext{}
```

Then you can use properties in your own classes.
```C#
public class Enemy : MonoBehaviour, IWithIcon<BestiaryContext>, IWithIcon<QuestLogContext>
{
    [SerializeField] private Sprite bestiaryIcon;
    [SerializeField] private Sprite questLogIcon;

    Sprite IWithIcon<BestiaryContext>.Icon => bestiaryIcon;
    Sprite IWithIcon<QuestLogContext>.Icon => questLogIcon;
}
```

Of course there are way more properties that you can use.

## Properties
The properties system contains the following properties:
* IWithName - Interface that allows you to get the name of the object.
* IWithLocalizedNames - Interface that allows you to get the localized names of the object (compatible with Unity.Localization package)
* IWithDescription - Interface that allows you to get the description of the object.
* IWithLocalizedDescription - Interface that allows you to get the localized description of the object (compatible with Unity.Localization package)
* IWithIcon - Interface that allows you to get the icon of the object.
* IWithPrefab - Interface that allows you to get the prefab of the object - this one requires to specify prefab type in the generic parameter.
* IWithConfiguration - Interface that allows you to get the configuration of the object.
* IWithAssetReference - Interface that allows you to get the asset reference for the object - compatible with Unity.Addressables package.

There are also some additional interfaces that require implementation of custom logic:
* IConditionallyRemovable - Interface that allows you to check if the object should be removed.

## About the configuration
By default, Fast Unity Creation Kit uses standard C# classes for logic processing which is not the best
way to integrate it with Unity. That's why the configuration is a `ScriptableObject` that can be created
in the Unity Editor. This way you can easily create a configuration for your object and assign it to the object.

To implement it within FastUnityCreationKit you need to create a class that implements the `IWithConfiguration` interface.
```C#
public class EnemyConfiguration : ScriptableObject
{
    [SerializeField] private LocalizedString name;
    [SerializeField] private LocalizedString description;
    [SerializeField] private Sprite icon;
}
```

Then you can use it in your class.
```C#
public abstract class Enemy : MonoBehaviour, IWithConfiguration<EnemyConfiguration>,
 IWithLocalizedName<BestiaryContext>, IWithLocalizedDescription<BestiaryContext>, 
 IWithIcon<BestiaryContext>
{
    [SerializeField] private EnemyConfiguration configuration;

    EnemyConfiguration IWithConfiguration<EnemyConfiguration>.Configuration => configuration;
    
    LocalizedString IWithLocalizedName<BestiaryContext>.Name => configuration.name;
    LocalizedString IWithLocalizedDescription<BestiaryContext>.Description => configuration.description;
    Sprite IWithIcon<BestiaryContext>.Icon => configuration.icon;
}
```

This solution allows for easy proxy-passing of the configuration to the object and way better
safety on exposing object data to other systems like UI as you can easily validate if desired
context is supported by the object and throw an exception if not.

<note>
If you want you can also skip implementation of configuration and hard-code the values in the object,
but this is not recommended as it is not a good practice to hard-code values in the code.

Alternatively you can use constants that are stored in single static class and use them with
the "Properties" system.
</note>

Example for the configuration with constants:
```C#
public static class EnemyConfigurationConstants
{
    public const string ENEMY_NAME_BESTIARY_WOLF = "Wolf";
    public const string ENEMY_DESCRIPTION_BESTIARY_WOLF = "A wolf is a large canine native to North America.";
}

public class Enemy : MonoBehaviour, IWithName<BestiaryContext>, IWithDescription<BestiaryContext>
{
    string IWithName<BestiaryContext>.Name => EnemyConfigurationConstants.ENEMY_NAME_BESTIARY_WOLF;
    string IWithDescription<BestiaryContext>.Description => EnemyConfigurationConstants.ENEMY_DESCRIPTION_BESTIARY_WOLF;
}
```

That allows for implementation of the configuration while avoiding the need to create a `ScriptableObject` for each 
type of object - this solution is intended for small projects where the configuration is not a big deal or
projects maintained mostly by software developers who can easily handle code-level configuration.

<note>
Property calls are often inlined by the compiler (JIT or IL2CPP), so there should be no performance overhead, but
this is not guaranteed as it was not properly tested (it is just an assumption based on
the observation and analysis of the generated code).
</note>