using FastUnityCreationKit.Events;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    ///     Event that is called when an object is created.
    /// </summary>
    [Preserve] [UsedImplicitly] public sealed class OnObjectExpiredEvent<TObjectType> :
        GlobalEventChannel<OnObjectExpiredEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }

    public static class OnObjectExpiredEvent
    {
        public static void TriggerEvent([NotNull] CKMonoBehaviour objectInstance)
        {
            EventAPI.TriggerGenericEventWithData(typeof(OnObjectExpiredEvent<>), objectInstance,
                objectInstance.GetType());
        }
    }
}