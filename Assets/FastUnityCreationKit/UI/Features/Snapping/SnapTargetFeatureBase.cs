using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;
using Sirenix.OdinInspector;

namespace FastUnityCreationKit.UI.Features.Snapping
{
    /// <summary>
    /// Represents a snap target - object that can be snapped to (for example inventory slot).
    /// This object does not support multi-snap instances.
    /// </summary>
    /// <typeparam name="TSelf">Type of the snap target.</typeparam>
    public abstract class SnapTargetFeatureBase<TSelf> : UIObjectBase, ISnapTarget<TSelf>, IOnObjectSnappedCallback<TSelf>
        where TSelf : SnapTargetFeatureBase<TSelf>, ISnapTarget<TSelf>, new()
    {
        /// <summary>
        /// If true, an object is snapped to this object.
        /// </summary>
        [ShowInInspector] [TabGroup("Debug")] [ReadOnly]
        public bool HasSnappedObject { get; private set; }
        
        /// <summary>
        /// If true, multiple objects can be snapped to this object.
        /// </summary>
        [ShowInInspector] [TabGroup("Debug")] [ReadOnly]
        public virtual bool MultipleSnapsPossible { get; private set; }
        
        /// <summary>
        /// Checks if it is possible to snap to the object.
        /// </summary>
        /// <param name="snapObject">Object you wish to snap to.</param>
        /// <returns>True if it is possible to snap to the object, false otherwise</returns>
        public virtual bool IsPossibleToSnap(SnapToFeatureBase<TSelf> snapObject) => !HasSnappedObject || MultipleSnapsPossible;
        
        public virtual void OnSnapBreak(SnapToFeatureBase<TSelf> objectBrokenFromSnap)
        {
            HasSnappedObject = false;
        }

        public virtual void OnSnap(SnapToFeatureBase<TSelf> objectSnapped)
        {
            HasSnappedObject = true;
        }
    }
}