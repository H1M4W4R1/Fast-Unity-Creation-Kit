namespace FastUnityCreationKit.Core.Events.Abstract
{
    /// <summary>
    /// Represents callback that is invoked when the event channel is triggered.
    /// </summary>
    public delegate void EventChannelCallback();
    
    /// <summary>
    /// Represents callback that is invoked when the event channel is triggered.
    /// Also contains the data that is sent through the event channel.
    /// </summary>
    public delegate void EventChannelCallback<in TEventData>(TEventData data) where TEventData : IEventChannelData;
}