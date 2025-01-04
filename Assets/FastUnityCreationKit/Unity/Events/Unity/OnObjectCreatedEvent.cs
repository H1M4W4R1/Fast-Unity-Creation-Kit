using FastUnityCreationKit.Events;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    ///     Event that is called when an object is created.
    /// </summary>
    [Preserve] [UsedImplicitly] public sealed class OnObjectCreatedEvent<TObjectType> :
        GlobalEventChannel<OnObjectCreatedEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }

    public static class OnObjectCreatedEvent
    {
        public static void TriggerEvent([NotNull] CKMonoBehaviour objectInstance)
        {
            EventAPI.TriggerGenericEventWithData(typeof(OnObjectCreatedEvent<>), objectInstance,
                objectInstance.GetType());
        }
    }
}