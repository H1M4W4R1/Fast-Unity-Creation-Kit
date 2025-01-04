using System;
using FastUnityCreationKit.Core.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    [Preserve]
    public interface IOnTriggerEnterCallback<in TComponentTypeOrInterface> :
        IOnTriggerEnterCallback
    {
        void OnTriggerEntered([NotNull] Collider collider, [NotNull] TComponentTypeOrInterface other);

        [Preserve] [UsedImplicitly]
        internal void ProcessTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out TComponentTypeOrInterface other))
                OnTriggerEntered(collider, other);
        }
    }

    public interface IOnTriggerEnterCallback
    {
        internal void _OnTriggerEnter([NotNull] Collider collider)
        {
            // Get the generic interface
            Type genericInterface = typeof(IOnTriggerEnterCallback<>);
            this.CallGenericCascadeInterfaces(genericInterface, nameof(IMethodMarker.ProcessTriggerEnter), collider);
        }

        public interface IMethodMarker
        {
            internal void ProcessTriggerEnter(Collider collider);
        }
    }
}