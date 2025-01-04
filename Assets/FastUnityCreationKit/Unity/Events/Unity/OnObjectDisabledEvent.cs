using FastUnityCreationKit.Events;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    ///     Called when an object is disabled.
    /// </summary>
    [Preserve] [UsedImplicitly] public sealed class OnObjectDisabledEvent<TObjectType> :
        GlobalEventChannel<OnObjectDisabledEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }

    public static class OnObjectDisabledEvent
    {
        public static void TriggerEvent([NotNull] CKMonoBehaviour objectInstance)
        {
            EventAPI.TriggerGenericEventWithData(typeof(OnObjectDisabledEvent<>), objectInstance,
                objectInstance.GetType());
        }
    }
}