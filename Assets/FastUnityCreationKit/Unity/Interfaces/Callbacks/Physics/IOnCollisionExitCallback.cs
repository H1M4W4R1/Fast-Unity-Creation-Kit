using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    public interface IOnCollisionExitCallback<in TComponentTypeOrInterface> : IOnCollisionExitCallback
    {
        void OnCollisionExit([NotNull] Collision collision, [NotNull] TComponentTypeOrInterface other);

        void IOnCollisionExitCallback._OnCollisionExit(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out TComponentTypeOrInterface other))
                OnCollisionExit(collision, other);
        }
        
    }
    
    public interface IOnCollisionExitCallback
    {
        internal void _OnCollisionExit([NotNull] Collision collision);
    }
}