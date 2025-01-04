using System;
using FastUnityCreationKit.Core.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    [Preserve]
    public interface IOnCollisionEnterCallback<in TComponentTypeOrInterface> :
        IOnCollisionEnterCallback
    {
        void OnCollisionEntered([NotNull] Collision collision, [NotNull] TComponentTypeOrInterface other);

        [Preserve] [UsedImplicitly]
        internal void ProcessCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out TComponentTypeOrInterface other))
                OnCollisionEntered(collision, other);
        }
    }

    public interface IOnCollisionEnterCallback
    {
        internal void _OnCollisionEnter([NotNull] Collision collision)
        {
            // Get the generic interface
            Type genericInterface = typeof(IOnCollisionEnterCallback<>);
            this.CallGenericCascadeInterfaces(genericInterface, nameof(IMethodMarker.ProcessCollisionEnter), collision);
        }

        public interface IMethodMarker
        {
            internal void ProcessCollisionEnter(Collision collision);
        }
    }
}