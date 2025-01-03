using System;
using FastUnityCreationKit.Utility.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    [Preserve]
    public interface IOnCollisionStayCallback<in TComponentTypeOrInterface> :
        IOnCollisionStayCallback
    {
        void OnCollisionStayed([NotNull] Collision collision, [NotNull] TComponentTypeOrInterface other);

        [Preserve] [UsedImplicitly]
        internal void ProcessCollisionStay(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out TComponentTypeOrInterface other))
                OnCollisionStayed(collision, other);
        }
    }

    public interface IOnCollisionStayCallback
    {
        internal void _OnCollisionStay([NotNull] Collision collision)
        {
            // Get the generic interface
            Type genericInterface = typeof(IOnCollisionStayCallback<>);
            this.CallGenericCascadeInterfaces(genericInterface, nameof(IMethodMarker.ProcessCollisionStay), collision);
        }

        public interface IMethodMarker
        {
            internal void ProcessCollisionStay(Collision collision);
        }
    }
}