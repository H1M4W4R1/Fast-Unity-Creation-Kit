using System;
using System.Reflection;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Events;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    /// Called when an object is disabled.
    /// </summary>
    public sealed class OnObjectDisabledEvent<TObjectType> : 
        GlobalEventChannel<OnObjectDisabledEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }
    
    public static class OnObjectDisabledEvent
    {
        public static void Trigger(CKMonoBehaviour objectInstance)
        {
            Type withType = objectInstance.GetType();
            
#if UNITY_EDITOR
            if (!withType.IsSubclassOf(typeof(CKMonoBehaviour)))
                Guard<ValidationLogConfig>.Error($"{withType.FullName} is not a subclass of CKMonoBehaviour.");
#endif
            
            object convertedInstance = Convert.ChangeType(objectInstance, withType);
            
            MethodInfo method = typeof(OnObjectDisabledEvent<>)
                .MakeGenericType(withType)
                .GetMethod(nameof(Trigger));
            
            method?.Invoke(null, new[] {convertedInstance});
        }
    }
}