using System;
using System.Reflection;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Events;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    /// Event that is called when an object is created.
    /// </summary>
    public sealed class OnObjectCreatedEvent<TObjectType> :
        GlobalEventChannel<OnObjectCreatedEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {

    }

    public static class OnObjectCreatedEvent
    {
        /// <summary>
        /// Trigger the event for the specified type.
        /// </summary>
        public static void Trigger(CKMonoBehaviour objectInstance)
        {
            // Get type from instance to reduce parameter count.
            Type withType = objectInstance.GetType();
            
#if UNITY_EDITOR
            if (!withType.IsSubclassOf(typeof(CKMonoBehaviour)))
                Guard<ValidationLogConfig>.Error($"{withType.FullName} is not a subclass of CKMonoBehaviour.");
#endif
            
            // Convert the object instance to the correct type.
            object convertedInstance = Convert.ChangeType(objectInstance, withType);
            
            // Get the method to trigger the event.
            MethodInfo method = typeof(OnObjectCreatedEvent<>)
                .MakeGenericType(withType)
                .GetMethod(nameof(Trigger));
            
            method?.Invoke(null, new[] {convertedInstance});
        }
    }
}