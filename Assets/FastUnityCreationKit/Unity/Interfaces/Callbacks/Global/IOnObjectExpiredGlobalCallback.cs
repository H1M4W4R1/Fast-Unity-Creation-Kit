using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Unity.Events.Unity;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Global
{
    public interface IOnObjectExpiredGlobalCallback<TObjectType> : IOnObjectExpiredGlobalCallback
        where TObjectType : CKMonoBehaviour
    {
        void IOnObjectExpiredGlobalCallback.TriggerOnObjectExpiredEvent(CKMonoBehaviour behaviour)
        {
            if (behaviour is TObjectType castedBehaviour)
                OnObjectExpiredEvent<TObjectType>.TriggerEvent(castedBehaviour);
            else
                Guard<ValidationLogConfig>.Error($"Behaviour {behaviour.GetType()} is not of type {typeof(TObjectType)}");
        }
    }
    
    public interface IOnObjectExpiredGlobalCallback : IGlobalCallback
    {
        internal void TriggerOnObjectExpiredEvent([NotNull] CKMonoBehaviour behaviour) =>
            OnObjectExpiredEvent.TriggerEvent(behaviour);
    }
}