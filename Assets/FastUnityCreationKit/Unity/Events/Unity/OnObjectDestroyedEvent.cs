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
        public static void TriggerEvent([NotNull] CKMonoBehaviour objectInstance) =>
            EventAPI.TriggerGenericEventWithData(typeof(OnObjectDestroyedEvent<>), objectInstance,
                objectInstance.GetType());
    }
}