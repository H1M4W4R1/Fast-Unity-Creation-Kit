using FastUnityCreationKit.UI.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Interfaces
{
    /// <summary>
    /// Represents a callback for snapping.
    /// </summary>
    public interface ISnapCallback<in TSnapTarget>
        where TSnapTarget : UIObject, ISnapTarget<TSnapTarget>
    {
        /// <summary>
        /// Called when snapping to a target.
        /// </summary>
        /// <param name="target">Target to snap to.</param>
        public void OnSnap([NotNull] TSnapTarget target);
        
        /// <summary>
        /// Called when snap is being broken
        /// </summary>
        public void OnBreakSnap([NotNull] TSnapTarget previousSnapTarget);
    }
}