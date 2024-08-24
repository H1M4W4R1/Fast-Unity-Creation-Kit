using FastUnityCreationKit.Core.Events.Abstract;

namespace FastUnityCreationKit.Core.Events
{
    /// <summary>
    /// Represents the global event channel.
    /// Global event channels are used to send events between systems and only one instance of the event channel is created.
    /// </summary>
    public abstract class GlobalEventChannel<TSelf> : EventChannel
        where TSelf : GlobalEventChannel<TSelf>, new()
    {
        /// <summary>
        /// Instance of the event channel.
        /// </summary>
        public static readonly TSelf Instance = new TSelf();
        
        /// <summary>
        /// Trigger the event channel.
        /// </summary>
        public static void TriggerEvent() => Instance.Trigger();
        
        /// <summary>
        /// Register the listener to the event channel.
        /// </summary>
        /// <param name="listener"></param>
        public static void RegisterEventListener(EventChannelCallback listener) => Instance.RegisterListener(listener);
        
        /// <summary>
        /// Unregister the listener from the event channel.
        /// </summary>
        /// <param name="listener"></param>
        public static void UnregisterEventListener(EventChannelCallback listener) => Instance.UnregisterListener(listener);
    }
    
    /// <summary>
    /// Generic version of the <see cref="GlobalEventChannel{TSelf}"/> that is capable of sending data.
    /// </summary>
    public abstract class GlobalEventChannel<TSelf, TChannelData> : EventChannel<TChannelData>
        where TSelf : GlobalEventChannel<TSelf, TChannelData>, new()
        where TChannelData : IEventChannelData
    {
        /// <summary>
        /// Instance of the event channel.
        /// </summary>
        public static readonly TSelf Instance = new TSelf();
        
        /// <summary>
        /// Trigger the event channel with the data.
        /// </summary>
        /// <param name="data"></param>
        public static void TriggerEvent(TChannelData data) => Instance.Trigger(data);
        
        /// <summary>
        /// Register the listener to the event channel.
        /// </summary>
        /// <param name="listener"></param>
        public static void RegisterEventListener(EventChannelCallback<TChannelData> listener) => Instance.RegisterListener(listener);
        
        /// <summary>
        /// Unregister the listener from the event channel.
        /// </summary>
        /// <param name="listener"></param>
        public static void UnregisterEventListener(EventChannelCallback<TChannelData> listener) => Instance.UnregisterListener(listener);
    }
}