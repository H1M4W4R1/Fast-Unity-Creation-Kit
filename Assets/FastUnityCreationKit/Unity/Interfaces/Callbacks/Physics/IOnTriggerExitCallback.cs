using System;
using FastUnityCreationKit.Core.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    [Preserve]
    public interface IOnTriggerExitCallback<in TComponentTypeOrInterface> :
        IOnTriggerExitCallback
    {
        void OnTriggerExited([NotNull] Collider collider, [NotNull] TComponentTypeOrInterface other);

        [Preserve] [UsedImplicitly]
        internal void ProcessTriggerExit(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out TComponentTypeOrInterface other))
                OnTriggerExited(collider, other);
        }
    }

    public interface IOnTriggerExitCallback
    {
        internal void _OnTriggerExit([NotNull] Collider collider)
        {
            // Get the generic interface
            Type genericInterface = typeof(IOnTriggerExitCallback<>);
            this.CallGenericCascadeInterfaces(genericInterface, nameof(IMethodMarker.ProcessTriggerExit), collider);
        }

        public interface IMethodMarker
        {
            internal void ProcessTriggerExit(Collider collider);
        }
    }
}