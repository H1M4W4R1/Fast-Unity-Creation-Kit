# Core

Core is a package of commonly used classes and interfaces that are used by other systems in Fast Unity Creation Kit. 

The subsystems of the Core system are:
* [Events](SystemCoreEvents.md) - The events system is used to manage the events in the game. It is used to create and manage
    the events that can be triggered by the player or the game itself. This subsystem is designed to separate other
    systems from each other, so they can be easily replaced or extended.
* [Identification](SystemCoreIdentification.md) - The identification system is used to manage the identification of objects in the game.
    It is used to create and manage the identification of objects in the game - players, enemies, NPCs, in-world destructible objects, etc.
* [Numerics](SystemCoreNumerics.md) - The numerics system is used to manage the numerical values in the game. It is used as
    an overlay to C# built-in types to provide easier and more flexible way to work with numbers via interfaces like `INumber`.
* [Priority](Priority.md) - The priority system is used to manage the priority of objects in the game. It is used to create
    prioritized lists of objects, so they are automatically sorted.
* [Utility](Utility.md) - The utility system is used to manage object properties. It can handle object with
    name, description, icons, prefabs, etc.
* [Values](SystemCoreValues.md) - The values system is used to manage the values of objects in the game. It is used to create 
    dynamically modifiable values - for example maximum health that can be affected by status effects or equipment.