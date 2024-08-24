using System.Collections.Generic;

namespace FastUnityCreationKit.Core.Events.Abstract
{
    /// <summary>
    /// Represents the event channel that is used to send events between systems.
    /// This is an internal class and should not be used directly.
    /// </summary>
    public abstract class EventChannelBase<TListenerCallback>
    {
        /// <summary>
        /// List of all listeners that are registered to the event channel.
        /// </summary>
        protected List<TListenerCallback> listeners = new List<TListenerCallback>();

        /// <summary>
        /// Register the listener to the event channel.
        /// </summary>
        public virtual void RegisterListener(TListenerCallback listener)
        {
            // Ensure that the listener is not already registered.
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        /// <summary>
        /// Unregister the listener from the event channel.
        /// </summary>
        public virtual void UnregisterListener(TListenerCallback listener)
        {
            // Ensure that the listener is registered.
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}