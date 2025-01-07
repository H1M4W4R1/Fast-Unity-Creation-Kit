using System;
using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;

namespace FastUnityCreationKit.UI.Elements.Abstract
{
    /// <summary>
    ///     Represents a progress element (usually a progress bar)
    /// </summary>
    public abstract class UIProgress : UIObjectWithContextBase<float>, IProgress<float>
    {
        protected const float PROGRESS_MULTIPLIER = 100f;

        /// <summary>
        ///     Reports the progress value and renders it in UI.
        ///     This should be supported even if the object does not have a data context provider.
        /// </summary>
        public void Report(float value) => Render(value);
    }
}