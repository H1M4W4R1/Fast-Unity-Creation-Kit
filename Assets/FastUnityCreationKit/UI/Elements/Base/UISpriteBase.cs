using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements.Base
{
    /// <summary>
    ///     Represents generic sprite renderer. Can be for example your icon or image.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public abstract class UISpriteBase : UIObjectWithContextBase<Sprite>, IRenderable<Sprite>
    {
        /// <summary>
        ///     Cached reference to Unity's Image component.
        /// </summary>
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [NotNull] private Image _image = null!;

        /// <summary>
        ///     External access to Image component. Useful for custom postprocessing.
        /// </summary>
        [NotNull] protected Image Image => _image;

        public virtual void Render(Sprite dataContext)
        {
            _image.sprite = dataContext;
        }

        public override void Setup()
        {
            base.Setup();
            _image = GetComponent<Image>();
        }
    }
}