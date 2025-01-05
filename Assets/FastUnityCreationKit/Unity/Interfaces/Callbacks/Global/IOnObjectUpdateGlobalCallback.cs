using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Unity.Events.Unity;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Global
{
    public interface IOnObjectUpdateGlobalCallback<TObjectType> : IOnObjectUpdateGlobalCallback
        where TObjectType : CKMonoBehaviour
    {
        void IOnObjectUpdateGlobalCallback.TriggerOnObjectUpdateEvent(CKMonoBehaviour behaviour)
        {
            if (behaviour is TObjectType castedBehaviour)
                OnObjectUpdateEvent<TObjectType>.TriggerEvent(castedBehaviour);
            else
                Guard<ValidationLogConfig>.Error($"Behaviour {behaviour.GetType()} is not of type {typeof(TObjectType)}");
        }
    }
    
    public interface IOnObjectUpdateGlobalCallback : IGlobalCallback
    {
        internal void TriggerOnObjectUpdateEvent([NotNull] CKMonoBehaviour behaviour) =>
            OnObjectUpdateEvent.TriggerEvent(behaviour);
    }
}