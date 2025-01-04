using System.Collections.Generic;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Events.Abstract
{
    /// <summary>
    ///     Represents the event channel that is used to send events between systems.
    ///     This is an internal class and should not be used directly.
    /// </summary>
    public abstract class EventChannelBase<TListenerCallback>
    {
        /// <summary>
        ///     List of all listeners that are registered to the event channel.
        /// </summary>
        [NotNull] [ItemNotNull] protected readonly List<TListenerCallback> listeners = new();

        /// <summary>
        ///     Count of the listeners that are registered to the event channel.
        /// </summary>
        public int ListenerCount => listeners.Count;

        /// <summary>
        ///     Check if the event channel has any listeners.
        /// </summary>
        public bool HasAnyListeners => listeners.Count > 0;

        /// <summary>
        ///     Remove all listeners from the event channel.
        /// </summary>
        public void RemoveAllListeners()
        {
            listeners.Clear();
        }

        /// <summary>
        ///     Register the listener to the event channel.
        /// </summary>
        public virtual void RegisterListener([NotNull] TListenerCallback listener)
        {
            // Ensure that the listener is not already registered.
            if (!listeners.Contains(listener)) listeners.Add(listener);
        }

        /// <summary>
        ///     Unregister the listener from the event channel.
        /// </summary>
        public virtual void UnregisterListener([NotNull] TListenerCallback listener)
        {
            // Ensure that the listener is registered.
            if (listeners.Contains(listener)) listeners.Remove(listener);
        }
    }
}