using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    public interface IOnTriggerExitCallback<in TComponentTypeOrInterface> : IOnTriggerExitCallback
    {
        void OnTriggerExit([NotNull] Collider other, [NotNull] TComponentTypeOrInterface otherComponent);

        void IOnTriggerExitCallback._OnTriggerExit(Collider other)
        {
            if(other.gameObject.TryGetComponent(out TComponentTypeOrInterface otherComponent))
                OnTriggerExit(other, otherComponent);
        }
        
    }
    
    public interface IOnTriggerExitCallback 
    {
        internal void _OnTriggerExit([NotNull] Collider other);
    }
}