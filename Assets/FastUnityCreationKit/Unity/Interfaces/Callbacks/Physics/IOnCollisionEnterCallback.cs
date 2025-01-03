using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    public interface IOnCollisionEnterCallback<in TComponentTypeOrInterface> : IOnCollisionEnterCallback 
    {
        void OnCollisionEnter([NotNull] Collision collision, [NotNull] TComponentTypeOrInterface other);

        void IOnCollisionEnterCallback._OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out TComponentTypeOrInterface other))
                OnCollisionEnter(collision, other);
        }
        
    }
    
    public interface IOnCollisionEnterCallback
    {
        internal void _OnCollisionEnter([NotNull] Collision collision);
    }
}