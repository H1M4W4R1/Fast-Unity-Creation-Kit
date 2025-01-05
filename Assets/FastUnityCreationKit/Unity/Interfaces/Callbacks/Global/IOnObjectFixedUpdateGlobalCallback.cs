using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Unity.Events.Unity;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Global
{
    public interface IOnObjectFixedUpdateGlobalCallback<TObjectType> : IOnObjectFixedUpdateGlobalCallback
        where TObjectType : CKMonoBehaviour
    {
        void IOnObjectFixedUpdateGlobalCallback.TriggerOnObjectFixedUpdateEvent(CKMonoBehaviour behaviour)
        {
            if (behaviour is TObjectType castedBehaviour)
                OnObjectFixedUpdateEvent<TObjectType>.TriggerEvent(castedBehaviour);
            else
                Guard<ValidationLogConfig>.Error($"Behaviour {behaviour.GetType()} is not of type {typeof(TObjectType)}");
        }
    }
    
    public interface IOnObjectFixedUpdateGlobalCallback : IGlobalCallback
    {
        internal void TriggerOnObjectFixedUpdateEvent([NotNull] CKMonoBehaviour behaviour) =>
            OnObjectFixedUpdateEvent.TriggerEvent(behaviour);
    }
}