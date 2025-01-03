using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    public interface IOnCollisionStayCallback<in TComponentTypeOrInterface> : IOnCollisionStayCallback
    {
        void OnCollisionStay([NotNull] Collision collision, [NotNull] TComponentTypeOrInterface other);

        void IOnCollisionStayCallback._OnCollisionStay(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out TComponentTypeOrInterface other))
                OnCollisionStay(collision, other);
        }
        
    }
    
    public interface IOnCollisionStayCallback
    {
        internal void _OnCollisionStay([NotNull] Collision collision);
    }
}