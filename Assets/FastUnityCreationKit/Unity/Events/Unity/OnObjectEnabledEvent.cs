using System;
using System.Reflection;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Events;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    /// Called when an object is enabled.
    /// </summary>
    [Preserve]
    [UsedImplicitly]
    public sealed class OnObjectEnabledEvent<TObjectType> :
        GlobalEventChannel<OnObjectEnabledEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }

    public static class OnObjectEnabledEvent
    {
        public static void TriggerEvent([NotNull] CKMonoBehaviour objectInstance) =>
            EventAPI.TriggerGenericEventWithData(typeof(OnObjectEnabledEvent<>), objectInstance,
                objectInstance.GetType());
    }
}