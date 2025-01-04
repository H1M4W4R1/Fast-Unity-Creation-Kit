using FastUnityCreationKit.Unity.Events.Unity;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Global
{
    public interface IOnObjectCreatedGlobalCallback<TObjectType> : IOnObjectCreatedGlobalCallback
        where TObjectType : CKMonoBehaviour
    {
        void IOnObjectCreatedGlobalCallback.TriggerOnObjectCreatedEvent(CKMonoBehaviour behaviour)
        {
            if (behaviour is TObjectType castedBehaviour)
                OnObjectCreatedEvent<TObjectType>.TriggerEvent(castedBehaviour);
        }
    }
    
    public interface IOnObjectCreatedGlobalCallback : IGlobalCallback
    {
        internal void TriggerOnObjectCreatedEvent([NotNull] CKMonoBehaviour behaviour) =>
            OnObjectCreatedEvent.TriggerEvent(behaviour);
    }
}