using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Features.Snapping;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Interfaces
{
    /// <summary>
    ///     Represents a snap target.
    /// </summary>
    public interface ISnapTarget<TSelf>
        where TSelf : UIObjectBase, ISnapTarget<TSelf>
    {
        public bool HasSnappedObject { get; }

        public bool MultipleSnapsPossible { get; }

        /// <summary>
        ///     Checks if it is possible to snap to the object.
        /// </summary>
        bool IsPossibleToSnap([NotNull] SnapToFeatureBase<TSelf> snapObject);
    }
}