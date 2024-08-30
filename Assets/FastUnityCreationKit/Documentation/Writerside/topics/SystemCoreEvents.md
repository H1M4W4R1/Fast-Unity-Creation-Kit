# Events
Events is a package of classes and interfaces that are used to manage the events in the game. It is used to create and manage the events that can be triggered by the player or the game itself. This subsystem is designed to separate other systems from each other, so they can be easily replaced or extended.

## Concepts
Events are a way to communicate between different parts of the game. They are used to trigger actions in response to certain conditions, like player input, game state changes, etc. Events are a way to decouple different parts of the game, so they can be easily replaced or extended without affecting other parts of the game.
Thanks to this system this project is completely modular and modules are independent of each other.

The core of Events system is the `EventChannel<TSelf>` class. It is a base class for all event channels in the game. It contains methods to register and unregister event listeners, and to trigger the event. 
It also contains separate generic type to add ability to pass data with the event.

```C#
public abstract class EventChannel : EventChannelBase<EventChannelCallback>

public abstract class EventChannel<TChannelData> : EventChannelBase<EventChannelCallback<TChannelData>> 
        where TChannelData : IEventChannelData       
```
This represents a local event channel that can be attached to an object.

There are also global event channels that are used to broadcast events to all objects in the game. They are implemented as singletons, so they can be accessed from anywhere in the game.

```C#
public abstract class GlobalEventChannel<TSelf> : EventChannel
        where TSelf : GlobalEventChannel<TSelf>, new()

public abstract class GlobalEventChannel<TSelf, TChannelData> : EventChannel<TChannelData>
        where TSelf : GlobalEventChannel<TSelf, TChannelData>, new()
        where TChannelData : IEventChannelData
```

<note>
In most cases you will use GlobalEventChannel, as it is more flexible and easier to use.
Local events are a possibility if you need to trigger events only on a specific object or
link events to a specific object when that event is used to communicate between components
on the same object.
</note>
        

## Creating Events
To create an event you need to extend `GlobalEventChannel<TSelf>` or `EventChannel<TSelf>` via a custom class.
The `TSelf` type parameter should be the type of the class you are extending as.

Event Channels also support secondary type parameter `TData` which is the type of the data that will be passed with the event,
that type needs to extend `IEventChannelData` interface.

```C#
public class MyGlobalEventChannel : GlobalEventChannel<MyGlobalEventChannel>
{
    // You can also override Trigger method to add custom logic    
    public override void Trigger()
    {       
        Debug.Log("Hello world!");
    }
}
```

<note>
If you don't call base Trigger method in your custom Trigger method, the event will not be triggered on listeners and 
thus the created event can be considered a virtual event (event without listeners).
</note>

You can also create an event that passes data with it.

```C#
public readonly struct MyEventData : IEventChannelData
{
    public readonly string message;
    
    public MyEventData(string message)
    {
        this.message = message;
    }
}

public class MyGlobalEventChannelWithData : GlobalEventChannel<MyGlobalEventChannelWithData, MyEventData>
{

}
```

## Listening to Events
To listen to an event you need to access the event channel and subscribe to it.
For global events, you can use the static `RegisterEventListener` method to subscribe to the event.
But you can also use the instance of the event channel and call `RegisterListener` method directly.

```C#
public class MyListener : MonoBehaviour
{
    private void Start()
    {
        MyGlobalEventChannel.RegisterEventListener(ExampleCallback);
    }
    
    private void ExampleCallback()
    {
        Debug.Log("Event triggered!");
    }
}
```

<note>
If you want to pass data with the event, you need to subscribe to the event with a callback that accepts the data as a parameter.
</note>

```C#
public class MyListener : MonoBehaviour
{
    private void Start()
    {
        MyGlobalEventChannelWithData.RegisterEventListener(ExampleCallback);
    }
    
    private void ExampleCallback(MyEventData data)
    {
        Debug.Log("Event triggered with data: " + data.Message);
    }
}
```




## Triggering Events
To trigger an event you need to access the event channel and call the `Trigger` method.
Global events also have stacic `TriggerEvent` method that can be used to trigger the event from anywhere in the game.

```C#
public class MyTrigger : MonoBehaviour
{
    private void Start()
    {
        MyGlobalEventChannel.TriggerEvent();
    }
}
```

<note>
If you want to pass data with the event, you need to call the `Trigger` method with the data as a parameter.
</note>

```C#
public class MyTrigger : MonoBehaviour
{
    private void Start()
    {
        MyGlobalEventChannelWithData.TriggerEvent(new MyEventData("Hello world!"));
    }
}
```

## Unsubscribing from Events
To unsubscribe from an event you can access the instance and use the `UnregisterListener` method or
when using global events you can use the static `UnregisterEventListener` method.

For more reference go back up to the **_Listening to Events_** section.


## Handling local events
Local events are events that are only triggered on the object that the event channel is attached to.
You do everything almost the same as with global events, but you need to use `EventChannel<TSelf>` instead of `GlobalEventChannel<TSelf>`
and you need to call methods on the instance of the event channel.