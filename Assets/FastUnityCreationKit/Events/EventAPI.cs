using System;
using System.Reflection;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Events
{
    /// <summary>
    /// API used to trigger global events.
    /// </summary>
    public static class EventAPI
    {
        /// <summary>
        /// Trigger global event channel.
        /// </summary>
        public static void TriggerEvent<TEventType>()
            where TEventType : GlobalEventChannel<TEventType>, new() =>
            GlobalEventChannel<TEventType>.TriggerEvent();

        /// <summary>
        /// Trigger global event channel asynchronously.
        /// </summary>
        public static UniTask TriggerEventAsync<TEventType>()
            where TEventType : GlobalEventChannel<TEventType>, new() =>
            GlobalEventChannel<TEventType>.TriggerEventAsync();

        /// <summary>
        /// Trigger global event channel with data.
        /// </summary>
        public static void TriggerEventWithData<TEventType, TChannelData>([NotNull] TChannelData data)
            where TEventType : GlobalEventChannel<TEventType, TChannelData>, new() =>
            GlobalEventChannel<TEventType, TChannelData>.TriggerEvent(data);

        /// <summary>
        /// Trigger global event channel with data asynchronously.
        /// </summary>
        public static UniTask TriggerEventWithDataAsync<TEventType, TChannelData>([NotNull] TChannelData data)
            where TEventType : GlobalEventChannel<TEventType, TChannelData>, new() =>
            GlobalEventChannel<TEventType, TChannelData>.TriggerEventAsync(data);

        /// <summary>
        /// Trigger global event channel using specified type.
        /// </summary>
        public static void TriggerGenericEvent([NotNull] Type eventGenericType, 
            [NotNull] params Type[] genericParams)
        {
            // Get method info
            MethodInfo method = eventGenericType.MakeGenericType(genericParams)
                .GetMethod(nameof(TriggerEvent), BindingFlags.Public | BindingFlags.Static);

            // Invoke method if it exists.
            method?.Invoke(null, null);
        }

        /// <summary>
        /// Trigger global event channel with data using specified type.
        /// </summary>
        public static void TriggerGenericEventWithData<TChannelData>([NotNull] Type eventGenericType,
            [NotNull] TChannelData data,
            [NotNull] params Type[] genericParams)
        {
            // Get method info
            MethodInfo method = eventGenericType.MakeGenericType(genericParams)
                .GetMethod(nameof(TriggerEvent), BindingFlags.Public | BindingFlags.Static);

            // Invoke method if it exists.
            method?.Invoke(null, new object[] {data});
        }

        /// <summary>
        /// Trigger global event channel asynchronously using specified type.
        /// </summary>
        public static UniTask TriggerGenericEventAsync([NotNull] Type eventGenericType, 
            [NotNull] params Type[] genericParams)
        {
            // Get method info
            MethodInfo method = eventGenericType.MakeGenericType(genericParams)
                .GetMethod(nameof(TriggerEventAsync), BindingFlags.Public | BindingFlags.Static);

            // Invoke method if it exists.
            return (UniTask) (method?.Invoke(null, null) ?? UniTask.CompletedTask);
        }

        /// <summary>
        /// Trigger global event channel with data asynchronously using specified type.
        /// </summary>
        public static UniTask TriggerGenericEventWithDataAsync<TChannelData>([NotNull] Type eventGenericType,
            [NotNull] TChannelData data,
            [NotNull] params Type[] genericParams)
        {
            // Get method info
            MethodInfo method = eventGenericType.MakeGenericType(genericParams)
                .GetMethod(nameof(TriggerEventAsync), BindingFlags.Public | BindingFlags.Static);

            // Invoke method if it exists.
            return (UniTask) (method?.Invoke(null, new object[] {data}) ?? UniTask.CompletedTask);
        }
    }
}