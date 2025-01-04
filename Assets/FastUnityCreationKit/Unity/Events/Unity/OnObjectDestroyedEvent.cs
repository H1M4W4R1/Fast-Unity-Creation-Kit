using System;
using System.Reflection;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Events;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    /// Called when an object is destroyed.
    /// </summary>
    [Preserve]
    [UsedImplicitly]
    public sealed class
        OnObjectDestroyedEvent<TObjectType> : GlobalEventChannel<OnObjectDestroyedEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }

    public static class OnObjectDestroyedEvent
    {
        /// <summary>
        /// Trigger the event for the specified type.
        /// </summary>
        public static void TriggerEvent(CKMonoBehaviour objectInstance)
        {
            // Get type from instance to reduce parameter count.
            Type withType = objectInstance.GetType();

#if UNITY_EDITOR
            if (!withType.IsSubclassOf(typeof(CKMonoBehaviour)))
                Guard<ValidationLogConfig>.Error(
                    $"{withType.FullName} is not a subclass of {nameof(CKMonoBehaviour)}.");
#endif

            // Convert the object instance to the correct type.
            object convertedInstance = Convert.ChangeType(objectInstance, withType);

            // Get the method to trigger the event.
            MethodInfo method = typeof(OnObjectDestroyedEvent<>)
                .MakeGenericType(withType)
                .GetMethod(nameof(TriggerEvent), BindingFlags.Public | BindingFlags.Static);

            method?.Invoke(null, new[] {convertedInstance});
        }
    }
}