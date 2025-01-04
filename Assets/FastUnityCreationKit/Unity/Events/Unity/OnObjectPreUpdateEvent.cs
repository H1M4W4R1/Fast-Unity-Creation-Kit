using FastUnityCreationKit.Events;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    ///     Called when an object is pre-updated.
    /// </summary>
    [Preserve] [UsedImplicitly] public sealed class OnObjectPreUpdateEvent<TObjectType> :
        GlobalEventChannel<OnObjectPreUpdateEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }

    public static class OnObjectPreUpdateEvent
    {
        public static void TriggerEvent([NotNull] CKMonoBehaviour objectInstance)
        {
            EventAPI.TriggerGenericEventWithData(typeof(OnObjectPreUpdateEvent<>), objectInstance,
                objectInstance.GetType());
        }
    }
}