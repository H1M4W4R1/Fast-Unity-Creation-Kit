using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Physics
{
    public interface IOnTriggerStayCallback<in TComponentTypeOrInterface> : IOnTriggerStayCallback
    {
        void OnTriggerStay([NotNull] Collider other, [NotNull] TComponentTypeOrInterface otherComponent);

        void IOnTriggerStayCallback._OnTriggerStay(Collider other)
        {
            if(other.gameObject.TryGetComponent(out TComponentTypeOrInterface otherComponent))
                OnTriggerStay(other, otherComponent);
        }
        
    }
    
    public interface IOnTriggerStayCallback
    {
        internal void _OnTriggerStay([NotNull] Collider other);
    }
}