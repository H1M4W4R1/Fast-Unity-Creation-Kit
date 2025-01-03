using System;
using FastUnityCreationKit.Utility.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    [Preserve]
    public interface IOnCollisionExitCallback<in TComponentTypeOrInterface> :
        IOnCollisionExitCallback
    {
        void OnCollisionExited([NotNull] Collision collision, [NotNull] TComponentTypeOrInterface other);

        [Preserve] [UsedImplicitly]
        internal void ProcessCollisionExit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out TComponentTypeOrInterface other))
                OnCollisionExited(collision, other);
        }
    }

    public interface IOnCollisionExitCallback
    {
        internal void _OnCollisionExit([NotNull] Collision collision)
        {
            // Get the generic interface
            Type genericInterface = typeof(IOnCollisionExitCallback<>);
            this.CallGenericCascadeInterfaces(genericInterface, nameof(IMethodMarker.ProcessCollisionExit), collision);
        }

        public interface IMethodMarker
        {
            internal void ProcessCollisionExit(Collision collision);
        }
    }
}