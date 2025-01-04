using FastUnityCreationKit.Unity.Events.Unity;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Global
{
    public interface IOnObjectEnabledGlobalCallback<TObjectType> : IOnObjectEnabledGlobalCallback
        where TObjectType : CKMonoBehaviour
    {
        void IOnObjectEnabledGlobalCallback.TriggerOnObjectEnabledEvent(CKMonoBehaviour behaviour)
        {
            if (behaviour is TObjectType castedBehaviour)
                OnObjectEnabledEvent<TObjectType>.TriggerEvent(castedBehaviour);
        }
    }
    
    public interface IOnObjectEnabledGlobalCallback : IGlobalCallback
    {
        internal void TriggerOnObjectEnabledEvent([NotNull] CKMonoBehaviour behaviour) =>
            OnObjectEnabledEvent.TriggerEvent(behaviour);
    }
}