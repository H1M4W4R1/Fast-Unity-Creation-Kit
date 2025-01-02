# FastUnityCreationKit.Unity
This package is used to provide core functionality for 
Unity objects. 

## Scene setup
To set up scene you need to add FastMonoBehaviourManager
to the scene. This object will be used to manage all
FastMonoBehaviour objects.

## FastMonoBehaviour
FastMonoBehaviour is a base class for all objects that
will be managed by FastMonoBehaviourManager. It provides
functionality to update objects in a fast way and to
handle custom events and update logic.

This behaviour also supports `ISavableObject` interface
from `FastUnityCreationKit.Saving` package to allow
saving and loading of objects when save file is created or 
loaded (beware that object must exist on scene when save 
is loaded for it to receive the callback).

### Custom Update Mode
FastMonoBehaviour provides several update modes. 
- `MonoBehaviour` - update mode compatible with Unity's 
  MonoBehaviour.
- `UpdateWhenDisabled` - update mode that allows to update
  object even when it is disabled, object needs to be
  enabled at least once to start updating.
- `UpdateWhenTimePaused` - update mode that allows to update
  object even when time is paused.
- `Forbidden` - update mode that does not allow to update
  object at all.

To set custom update mode you need to override `UpdateMode`
property on FastMonoBehaviour.

You can check `UpdateMode` enum for more information.

### Custom UpdateTime
With FastMonoBehaviour DeltaTime is always provided to
`OnObjectUpdated` method that is provided by 
`IUpdateCallback`.

You can set which deltaTime will be provided to
`OnObjectUpdated` method by overriding 
`UpdateTimeConfig` property on FastMonoBehaviour.

Basic supported modes are:
- `DeltaTime` - normal deltaTime provided by Unity 
  (affected by `TimeAPI` scaling)
- `UnscaledDeltaTime` - deltaTime that is not affected by
  `TimeAPI` scaling nor pausing.
- `RealtimeSinceStartup` - deltaTime that is based on
  `Time.realtimeSinceStartup` (not affected by `TimeAPI` 
  scaling nor pausing).

This allows to easy definition of deltaTime for a group 
of objects within their abstraction.

For more information check `UpdateMode` enum.

## Callbacks
FastMonoBehaviour provides several callbacks that can be
overridden to provide custom logic.

### ICreateCallback
This is called when object is created and initialized.
If object supports `IInitializable` then this will execute
after initialization provided by that interface was done.

### IEnabledCallback
This is called when object is enabled.

### IDisableCallback
This is called when object is disabled.

## IPreUpdateCallback
This is called before object is updated. Provides
deltaTime based on `UpdateTimeConfig`. Behaviour can be
modified by changing `UpdateMode` to allow updating when
time is paused or object is disabled.

### IUpdateCallback
This is called when object is updated. Provides
deltaTime based on `UpdateTimeConfig`. Behaviour can be
modified by changing `UpdateMode` to allow updating when
time is paused or object is disabled.

### IPostUpdateCallback
This is called after object is updated. Provides
deltaTime based on `UpdateTimeConfig`. Behaviour can be
modified by changing `UpdateMode` to allow updating when
time is paused or object is disabled.

### IFixedUpdateCallback
This is called when object is updated in fixed update.

### IDestroyCallback
This is called when object is destroyed.

### IQuitCallback
This is called when application is quitting.

### IPersistent
Object is not destroyed when scene is changed
(automatically performs `DontDestroyOnLoad`).

## Advanced Callbacks
Also, some advanced callbacks are provided to support
commonly used features.

They require `PhysicsRaycaster` to be present on the scene
camera to work properly (unless `FastMonoBehaviour` is an
UI object).

### IClickable
Provides `OnClick` method that is called when object is
clicked.

### IDoubleClickable
Provides `OnDoubleClick` method that is called when object
is clicked twice in a short time.

Also supports `OnClick` method with default implementation
that is empty.

### IHoverable
Provides `OnHoverEnter` and `OnHoverExit` methods that are
called when object is hovered over (pointer enters/leaves
object).

### ISelectable
Provides `OnSelected` and `OnDeselected` methods that are 
called when object is selected/deselected.

Also provides `OnSelectionChanged` method that is called
when selection state is changed.

Has protected static property `SelectedObjects` property
that contains all selected objects (even if they are not
of the same type as current `FastMonoBehaviour`).

### ISupportsMultipleSelection
Can be used to allow multiple selection of objects.

## Handling Time
To change time scale you can use `TimeAPI` class that
provides static methods to change time scale and pause
time.

It is recommended to completely avoid using Unity's
`Time` class as it will cause **SEVERE ISSUES** with this
package.

### Pausing game
To pause game you can use `TimeAPI.PauseTime` method.
It outputs a `PauseObject` structure that represents
current handle to pause abstraction.

To resume time you can use `PauseObject.Resume` method with
pause object that was returned by `PauseTime`.

Game is paused as long as there is at least one pause
object that is not resumed.

You can force game to be unpaused by using
`TimeAPI.ForceResumeTime` method, which will resume all
paused objects. This is not recommended as it can cause
unexpected behaviour.

You can get current pause state by using `TimeAPI.
IsTimePaused`.

### Changing time scale
To change time scale you can use `TimeAPI.SetTimeScale`
method. It will change time scale for all objects that
are using `FastMonoBehaviour` package.

To get current time scale you can use `TimeAPI.TimeMultiplier`
property.

### Getting DeltaTime (and other time values)
TimeAPI provides several core time values that can be
used to get time values that are compatible with
this package.

- `TimeAPI.DeltaTime` - deltaTime that is affected by
  both time scale and pausing.
- `TimeAPI.UnscaledDeltaTime` - deltaTime that is not
  affected by time scale nor pausing.
- `TimeAPI.RealtimeSinceStartup` - time that is based
  on `Time.realtimeSinceStartup` (not affected by time
  scale nor pausing).
- `TimeAPI.FixedDeltaTime` - fixedDeltaTime that is
  affected by time scale and pausing.

