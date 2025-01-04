using System;
using FastUnityCreationKit.Core.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    [Preserve]
    public interface IOnTriggerStayCallback<in TComponentTypeOrInterface> :
        IOnTriggerStayCallback
    {
        void OnTriggerStayed([NotNull] Collider collider, [NotNull] TComponentTypeOrInterface other);

        [Preserve] [UsedImplicitly]
        internal void ProcessTriggerStay(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out TComponentTypeOrInterface other))
                OnTriggerStayed(collider, other);
        }
    }

    public interface IOnTriggerStayCallback
    {
        internal void _OnTriggerStay([NotNull] Collider collider)
        {
            // Get the generic interface
            Type genericInterface = typeof(IOnTriggerStayCallback<>);
            this.CallGenericCascadeInterfaces(genericInterface, nameof(IMethodMarker.ProcessTriggerStay), collider);
        }

        public interface IMethodMarker
        {
            internal void ProcessTriggerStay(Collider collider);
        }
    }
}