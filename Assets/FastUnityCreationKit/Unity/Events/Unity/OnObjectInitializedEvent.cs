using FastUnityCreationKit.Events;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    /// <summary>
    ///     Event that is called when an object is initialized (before creation)
    /// </summary>
    [Preserve] [UsedImplicitly] public sealed class OnObjectInitializedEvent<TObjectType> :
        GlobalEventChannel<OnObjectInitializedEvent<TObjectType>, TObjectType>
        where TObjectType : CKMonoBehaviour
    {
    }

    public static class OnObjectInitializedEvent
    {
        public static void TriggerEvent([NotNull] CKMonoBehaviour objectInstance)
        {
            EventAPI.TriggerGenericEventWithData(typeof(OnObjectInitializedEvent<>), objectInstance,
                objectInstance.GetType());
        }
    }
}