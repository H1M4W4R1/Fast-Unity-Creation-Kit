using FastUnityCreationKit.Events;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    [Preserve] [UsedImplicitly] public sealed class OnObjectFixedUpdateEvent<TObjectType> :
        GlobalEventChannel<OnObjectFixedUpdateEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }

    public static class OnObjectFixedUpdateEvent
    {
        public static void TriggerEvent([NotNull] CKMonoBehaviour objectInstance)
        {
            EventAPI.TriggerGenericEventWithData(typeof(OnObjectFixedUpdateEvent<>), objectInstance,
                objectInstance.GetType());
        }
    }
}