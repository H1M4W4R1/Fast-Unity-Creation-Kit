using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Events.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Events
{
    /// <summary>
    ///     Represents the event channel that is used to send data between systems.
    ///     This should be used instead of C# events to decouple systems.
    ///     <br /><br />
    ///     Channels can send data between systems without them knowing about each other.
    ///     Data is sent through the channel and the systems that are listening to the channel receive the data, then
    ///     those systems can process the data and act accordingly.
    ///     If you don't need to send data, use <see cref="EventChannel" />.
    /// </summary>
    /// <remarks>
    ///     This system will cause a lot of overhead, however it allows for complete decoupling of systems
    ///     and thus will make the code more maintainable and easier to understand.
    /// </remarks>
    public abstract class EventChannel<TChannelData> : EventChannelBase<EventChannelCallbackAsync<TChannelData>>
        where TChannelData : notnull
    {
        /// <summary>
        ///     Trigger the event channel with the data.
        /// </summary>
        public virtual void Trigger([NotNull] TChannelData data)
        {
            // Loops through all listeners and invokes them.
            for (int index = 0; index < listeners.Count; index++)
            {
                EventChannelCallbackAsync<TChannelData> listener = listeners[index];
                listener.Invoke(data);
            }
        }

        /// <summary>
        ///     Trigger the event channel with the data asynchronously.
        /// </summary>
        /// <param name="data">The data that is sent through the channel.</param>
        public virtual async UniTask TriggerAsync([NotNull] TChannelData data)
        {
            // Loops through all listeners and invokes them.
            for (int index = 0; index < listeners.Count; index++)
            {
                EventChannelCallbackAsync<TChannelData> listener = listeners[index];
                await listener.Invoke(data);
            }
        }
    }

    /// <summary>
    ///     Represents the event channel that is used to send events between systems.
    ///     This should be used instead of C# events to decouple systems.
    ///     <br /><br />
    ///     This channel cannot send data, for that use <see cref="EventChannel{TChannelData}" />.
    /// </summary>
    public abstract class EventChannel : EventChannelBase<EventChannelCallbackAsync>
    {
        /// <summary>
        ///     Trigger the event channel.
        /// </summary>
        public virtual void Trigger()
        {
            // Loops through all listeners and invokes them.
            for (int index = 0; index < listeners.Count; index++)
            {
                EventChannelCallbackAsync listener = listeners[index];
                listener.Invoke();
            }
        }

        /// <summary>
        ///     Trigger the event channel asynchronously.
        /// </summary>
        public virtual async UniTask TriggerAsync()
        {
            // Loops through all listeners and invokes them.
            for (int index = 0; index < listeners.Count; index++)
            {
                EventChannelCallbackAsync listener = listeners[index];
                await listener.Invoke();
            }
        }
    }
}