using FastUnityCreationKit.UI.Elements.Abstract;
using FastUnityCreationKit.Core.Logging;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements.Base.Progress
{
    [RequireComponent(typeof(Image))]
    public abstract class UISpriteFillProgressBase : UIProgress
    {
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [NotNull] private Image _image = null!;

        public override void Setup()
        {
            base.Setup();
            _image = GetComponent<Image>();

            // Check if image type is filled, if not it's good to have an error message.
            if (_image.type != Image.Type.Filled)
            {
                Guard<UserInterfaceLogConfig>.Warning(
                    "UISpriteFillProgressBase requires Image component to be of type Filled." +
                    $"Found unsupported type: {_image.type} on {name}. " +
                    $"Changing image mode to Filled.");
                _image.type = Image.Type.Filled;
            }
        }

        public override void Render(float dataContext) =>
            _image.fillAmount = dataContext;
    }
}