using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Unity.Events.Unity;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Global
{
    public interface IOnObjectInitializedGlobalCallback<TObjectType> : IOnObjectInitializedGlobalCallback
        where TObjectType : CKMonoBehaviour
    {
        void IOnObjectInitializedGlobalCallback.TriggerOnObjectInitializedEvent(CKMonoBehaviour behaviour)
        {
            if (behaviour is TObjectType castedBehaviour)
                OnObjectInitializedEvent<TObjectType>.TriggerEvent(castedBehaviour);
            else
                Guard<ValidationLogConfig>.Error($"Behaviour {behaviour.GetType()} is not of type {typeof(TObjectType)}");
        }
    }
    
    public interface IOnObjectInitializedGlobalCallback : IGlobalCallback
    {
        internal void TriggerOnObjectInitializedEvent([NotNull] CKMonoBehaviour behaviour) =>
            OnObjectInitializedEvent.TriggerEvent(behaviour);
    }
}