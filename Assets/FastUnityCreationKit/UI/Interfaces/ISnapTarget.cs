using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Features.Snapping;

namespace FastUnityCreationKit.UI.Interfaces
{
    /// <summary>
    /// Represents a snap target.
    /// </summary>
    public interface ISnapTarget<TSelf> where TSelf : UIObject, ISnapTarget<TSelf>
    {
        public bool HasSnappedObject { get; }
        
        public bool MultipleSnapsPossible { get; }
        
        /// <summary>
        /// Checks if it is possible to snap to the object.
        /// </summary>
        bool IsPossibleToSnap(SnapToFeature<TSelf> snapObject);
    }
}