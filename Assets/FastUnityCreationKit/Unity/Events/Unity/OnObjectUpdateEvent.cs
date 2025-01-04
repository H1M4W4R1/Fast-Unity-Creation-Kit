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
    public sealed class OnObjectUpdateEvent<TObjectType> :
        GlobalEventChannel<OnObjectUpdateEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }

    public static class OnObjectUpdateEvent
    {
        public static void TriggerEvent([NotNull] CKMonoBehaviour objectInstance) =>
            EventAPI.TriggerGenericEventWithData(typeof(OnObjectUpdateEvent<>), objectInstance,
                objectInstance.GetType());
    }
}