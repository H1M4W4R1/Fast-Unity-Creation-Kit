using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    public interface IOnTriggerEnterCallback<in TComponentTypeOrInterface> : IOnTriggerEnterCallback
    {
        void OnTriggerEnter([NotNull] Collider other, [NotNull] TComponentTypeOrInterface otherComponent);

        void IOnTriggerEnterCallback._OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out TComponentTypeOrInterface otherComponent))
                OnTriggerEnter(other, otherComponent);
        }
        
    }
    
    public interface IOnTriggerEnterCallback
    {
        internal void _OnTriggerEnter([NotNull] Collider other);
    }
}