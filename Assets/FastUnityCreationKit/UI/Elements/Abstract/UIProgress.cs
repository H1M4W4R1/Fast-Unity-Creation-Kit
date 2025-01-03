using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;

namespace FastUnityCreationKit.UI.Elements.Abstract
{
    /// <summary>
    /// Represents a progress element (usually a progress bar)
    /// </summary>
    public abstract class UIProgress : UIObjectWithContextBase<float>, IRenderable<float>
    {
        protected const float PROGRESS_MULTIPLIER = 100f;
        
        /// <summary>
        /// Renders the progress element.
        /// </summary>
        /// <param name="dataContext">Current progress in range [0, 1]</param>
        public abstract void Render(float dataContext);
    }
}