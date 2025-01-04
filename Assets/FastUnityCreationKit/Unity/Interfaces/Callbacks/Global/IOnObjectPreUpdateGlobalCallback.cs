using FastUnityCreationKit.Unity.Events.Unity;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Global
{
    public interface IOnObjectPreUpdateGlobalCallback<TObjectType> : IOnObjectPreUpdateGlobalCallback
        where TObjectType : CKMonoBehaviour
    {
        void IOnObjectPreUpdateGlobalCallback.TriggerOnObjectPreUpdateEvent(CKMonoBehaviour behaviour)
        {
            if (behaviour is TObjectType castedBehaviour)
                OnObjectPreUpdateEvent<TObjectType>.TriggerEvent(castedBehaviour);
        }
    }
    
    public interface IOnObjectPreUpdateGlobalCallback : IGlobalCallback
    {
        internal void TriggerOnObjectPreUpdateEvent([NotNull] CKMonoBehaviour behaviour) =>
            OnObjectPreUpdateEvent.TriggerEvent(behaviour);
    }
}