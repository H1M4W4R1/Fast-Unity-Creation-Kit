# Required Plugins
- DOTween Pro
- Odin Validator, Inspector and Serializer
- UniTask

# Required Unity Packages
- TextMesh Pro
- Unity Addressables
- Unity Burst
- Unity Collections
- Unity InputSystem
- Unity Mathematics

# Serialization
By default, **Creation Kit** does not support fully
automatic save serialization, so you need to implement
serialization of each object manually.

It's worth noting that **Creation Kit** does not use
Odin's serialization system for most of its objects,
and you should be very careful when using objects with 
[OdinSerialize] attributes with **Creation Kit** when
using them with MonoBehaviour objects (it's fine for
ScriptableObject's).