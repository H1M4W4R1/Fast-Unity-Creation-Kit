using FastUnityCreationKit.Events;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    [Preserve] [UsedImplicitly] public sealed class OnObjectPostUpdateEvent<TObjectType> :
        GlobalEventChannel<OnObjectPostUpdateEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }

    public static class OnObjectPostUpdateEvent
    {
        public static void TriggerEvent([NotNull] CKMonoBehaviour objectInstance)
        {
            EventAPI.TriggerGenericEventWithData(typeof(OnObjectPostUpdateEvent<>), objectInstance,
                objectInstance.GetType());
        }
    }
}