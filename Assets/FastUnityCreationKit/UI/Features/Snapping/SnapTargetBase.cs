using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;

namespace FastUnityCreationKit.UI.Features.Snapping
{
    public abstract class SnapTargetBase<TSelf> : UIObject, ISnapTarget, IOnObjectSnappedCallback<TSelf>
        where TSelf : SnapTargetBase<TSelf>, new()
    {
        /// <summary>
        /// If true, an object is snapped to this object.
        /// </summary>
        public bool HasSnappedObject { get; private set; }
        
        /// <summary>
        /// If true, multiple objects can be snapped to this object.
        /// </summary>
        public virtual bool MultipleSnapsPossible { get; private set; }
        
        /// <summary>
        /// Returns true if the object can be snapped to.
        /// </summary>
        public virtual bool CanBeSnappedTo => !HasSnappedObject || MultipleSnapsPossible;
        
        public virtual void OnSnapBreak(SnapToFeature<TSelf> objectBrokenFromSnap)
        {
            HasSnappedObject = false;
        }

        public virtual void OnSnap(SnapToFeature<TSelf> objectSnapped)
        {
            HasSnappedObject = true;
        }
    }
}