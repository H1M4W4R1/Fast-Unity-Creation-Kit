﻿using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Unity.Events.Unity;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Global
{
    public interface IOnObjectPostUpdateGlobalCallback<TObjectType> : IOnObjectPostUpdateGlobalCallback
        where TObjectType : CKMonoBehaviour
    {
        void IOnObjectPostUpdateGlobalCallback.TriggerOnObjectPostUpdateEvent(CKMonoBehaviour behaviour)
        {
            if (behaviour is TObjectType castedBehaviour)
                OnObjectPostUpdateEvent<TObjectType>.TriggerEvent(castedBehaviour);
            else
                Guard<ValidationLogConfig>.Error($"Behaviour {behaviour.GetType()} is not of type {typeof(TObjectType)}");
        }
    }
    
    public interface IOnObjectPostUpdateGlobalCallback : IGlobalCallback
    {
        internal void TriggerOnObjectPostUpdateEvent([NotNull] CKMonoBehaviour behaviour) =>
            OnObjectPostUpdateEvent.TriggerEvent(behaviour);
    }
}