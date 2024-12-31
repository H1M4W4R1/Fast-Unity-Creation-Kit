using FastUnityCreationKit.UI.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Interfaces
{
    /// <summary>
    /// Represents a callback for snapping.
    /// 
    /// This callback is performed on all components in object being moved and it's children
    /// that implement it. 
    /// </summary>
    public interface ISnapCallback<in TSnapTarget>
        where TSnapTarget : UIObjectBase, ISnapTarget<TSnapTarget>
    {
        /// <summary>
        /// Called when snapping to a target.
        /// </summary>
        /// <param name="target">Target to snap to.</param>
        public void OnSnap([NotNull] TSnapTarget target);
        
        /// <summary>
        /// Called when snap is being broken
        /// </summary>
        public void OnSnapBreak([NotNull] TSnapTarget previousSnapTarget);
    }
}