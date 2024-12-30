using System;
using FastUnityCreationKit.UI.Elements.Abstract;

namespace FastUnityCreationKit.UI.Elements.Utility.Internal.Carousel
{
    /// <summary>
    /// Represents a button that is used in the carousel.
    /// </summary>
    public sealed class CarouselButton : UIButton
    {
        /// <summary>
        /// Internal action that is invoked when the button is pressed.
        /// </summary>
        internal Action onButtonPressed;
        
        protected override void OnClick()
        {
            onButtonPressed?.Invoke();
        }
    }
}