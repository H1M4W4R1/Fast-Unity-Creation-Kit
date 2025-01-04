using FastUnityCreationKit.Unity.Events.Unity;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Global
{
    public interface IOnObjectDisabledGlobalCallback<TObjectType> : IOnObjectDisabledGlobalCallback
        where TObjectType : CKMonoBehaviour
    {
        void IOnObjectDisabledGlobalCallback.TriggerOnObjectDisabledEvent(CKMonoBehaviour behaviour)
        {
            if (behaviour is TObjectType castedBehaviour)
                OnObjectDisabledEvent<TObjectType>.TriggerEvent(castedBehaviour);
        }
    }
    
    public interface IOnObjectDisabledGlobalCallback : IGlobalCallback
    {
        internal void TriggerOnObjectDisabledEvent([NotNull] CKMonoBehaviour behaviour) =>
            OnObjectDisabledEvent.TriggerEvent(behaviour);
    }
}