using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;

namespace FastUnityCreationKit.UI.Features.Snapping
{
    /// <summary>
    /// Represents a snap target - object that can be snapped to (for example inventory slot).
    /// This object does not support multi-snap instances.
    /// </summary>
    /// <typeparam name="TSelf">Type of the snap target.</typeparam>
    public abstract class SnapTargetBase<TSelf> : UIObject, ISnapTarget<TSelf>, IOnObjectSnappedCallback<TSelf>
        where TSelf : SnapTargetBase<TSelf>, ISnapTarget<TSelf>, new()
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
        
        /// <summary>
        /// Checks if it is possible to snap to the object.
        /// </summary>
        /// <param name="snapObject">Object you wish to snap to.</param>
        /// <returns>True if it is possible to snap to the object, false otherwise</returns>
        public virtual bool IsPossibleToSnap(SnapToFeature<TSelf> snapObject) => CanBeSnappedTo;
        
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