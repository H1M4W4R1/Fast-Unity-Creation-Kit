using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Unity.Events.Unity;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Global
{
    public interface IOnObjectDestroyedGlobalCallback<TObjectType> : IOnObjectDestroyedGlobalCallback
        where TObjectType : CKMonoBehaviour
    {
        void IOnObjectDestroyedGlobalCallback.TriggerOnObjectDestroyedEvent(CKMonoBehaviour behaviour)
        {
            if (behaviour is TObjectType castedBehaviour)
                OnObjectDestroyedEvent<TObjectType>.TriggerEvent(castedBehaviour);
            else
                Guard<ValidationLogConfig>.Error($"Behaviour {behaviour.GetType()} is not of type {typeof(TObjectType)}");
        }
    }
    
    public interface IOnObjectDestroyedGlobalCallback : IGlobalCallback
    {
        internal void TriggerOnObjectDestroyedEvent([NotNull] CKMonoBehaviour behaviour) =>
            OnObjectDestroyedEvent.TriggerEvent(behaviour);
    }
}