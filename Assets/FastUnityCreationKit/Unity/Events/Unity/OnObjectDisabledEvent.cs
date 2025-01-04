using System;
using System.Reflection;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Events;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    /// Called when an object is disabled.
    /// </summary>
    [Preserve]
    [UsedImplicitly]
    public sealed class OnObjectDisabledEvent<TObjectType> :
        GlobalEventChannel<OnObjectDisabledEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }

    public static class OnObjectDisabledEvent
    {
        public static void TriggerEvent(CKMonoBehaviour objectInstance)
        {
            Type withType = objectInstance.GetType();

#if UNITY_EDITOR
            if (!withType.IsSubclassOf(typeof(CKMonoBehaviour)))
                Guard<ValidationLogConfig>.Error(
                    $"{withType.FullName} is not a subclass of {nameof(CKMonoBehaviour)}.");
#endif

            object convertedInstance = Convert.ChangeType(objectInstance, withType);

            MethodInfo method = typeof(OnObjectDisabledEvent<>)
                .MakeGenericType(withType)
                .GetMethod(nameof(TriggerEvent), BindingFlags.Public | BindingFlags.Static);

            method?.Invoke(null, new[] {convertedInstance});
        }
    }
}