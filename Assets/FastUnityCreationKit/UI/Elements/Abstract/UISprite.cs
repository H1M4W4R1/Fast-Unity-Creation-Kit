using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements.Abstract
{
    /// <summary>
    /// Represents generic sprite renderer. Can be for example your icon or image.
    /// </summary>
    public abstract class UISprite : UIObject, IRenderable<Sprite> 
    {
        /// <summary>
        /// Cached reference to Unity's Image component.
        /// </summary>
        private Image _image;
        
        /// <summary>
        /// External access to Image component. Useful for custom postprocessing.
        /// </summary>
        protected Image Image => _image;
        
        public override void Setup()
        {
            base.Setup();
            _image = GetComponent<Image>();
        }

        public virtual void Render(Sprite dataContext)
        {
            _image.sprite = dataContext;
        }
    }
}