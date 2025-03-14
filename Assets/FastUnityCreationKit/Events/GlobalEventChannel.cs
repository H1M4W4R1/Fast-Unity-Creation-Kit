﻿using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Singleton;
using FastUnityCreationKit.Events.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Events
{
    /// <summary>
    ///     Represents the global event channel.
    ///     Global event channels are used to send events between systems and only one instance of the event channel is
    ///     created.
    /// </summary>
    public abstract class GlobalEventChannel<TSelf> : EventChannel, ISingleton<TSelf>
        where TSelf : GlobalEventChannel<TSelf>, new()
    {
        /// <summary>
        ///     Instance of the event channel.
        /// </summary>
        [NotNull] public static TSelf Instance => ISingleton<TSelf>.GetInstance();

        /// <summary>
        ///     Trigger the event channel.
        /// </summary>
        public static void TriggerEvent()
        {
            Instance.Trigger();
        }

        /// <summary>
        ///     Trigger the event channel asynchronously.
        /// </summary>
        public static async UniTask TriggerEventAsync()
        {
            await Instance.TriggerAsync();
        }

        /// <summary>
        ///     Register the listener to the event channel.
        /// </summary>
        /// <param name="listener"></param>
        public static void RegisterEventListener([NotNull] EventChannelCallbackAsync listener)
        {
            Instance.RegisterListener(listener);
        }

        /// <summary>
        ///     Unregister the listener from the event channel.
        /// </summary>
        /// <param name="listener"></param>
        public static void UnregisterEventListener([NotNull] EventChannelCallbackAsync listener)
        {
            Instance.UnregisterListener(listener);
        }
    }

    /// <summary>
    ///     Generic version of the <see cref="GlobalEventChannel{TSelf}" /> that is capable of sending data.
    /// </summary>
    public abstract class GlobalEventChannel<TSelf, TChannelData> : EventChannel<TChannelData>, ISingleton<TSelf>
        where TSelf : GlobalEventChannel<TSelf, TChannelData>, new()
        where TChannelData : notnull
    {
        /// <summary>
        ///     Instance of the event channel.
        /// </summary>
        [NotNull] public static TSelf Instance => ISingleton<TSelf>.GetInstance();

        /// <summary>
        ///     Trigger the event channel with the data.
        /// </summary>
        /// <param name="data">Channel data that is sent through the event channel.</param>
        public static void TriggerEvent([NotNull] TChannelData data)
        {
            Instance.Trigger(data);
        }

        /// <summary>
        ///     Trigger the event channel with the data asynchronously.
        /// </summary>
        /// <param name="data">Channel data that is sent through the event channel.</param>
        public static async UniTask TriggerEventAsync([NotNull] TChannelData data)
        {
            await Instance.TriggerAsync(data);
        }

        /// <summary>
        ///     Register the listener to the event channel.
        /// </summary>
        /// <param name="listener"></param>
        public static void RegisterEventListener([NotNull] EventChannelCallbackAsync<TChannelData> listener)
        {
            Instance.RegisterListener(listener);
        }

        /// <summary>
        ///     Unregister the listener from the event channel.
        /// </summary>
        /// <param name="listener"></param>
        public static void UnregisterEventListener([NotNull] EventChannelCallbackAsync<TChannelData> listener)
        {
            Instance.UnregisterListener(listener);
        }

        /// <summary>
        ///     Remove all listeners from the event channel.
        /// </summary>
        public static void RemoveAllEventListeners()
        {
            Instance.RemoveAllListeners();
        }
    }
}