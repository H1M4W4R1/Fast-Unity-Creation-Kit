using FastUnityCreationKit.UI.Elements.Abstract;
using FastUnityCreationKit.UI.Utility;
using FastUnityCreationKit.Utility.Logging;
using UnityEngine;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements.Base.Progress
{
    [RequireComponent(typeof(Image))]
    public abstract class UISpriteFillProgressBase : UIProgress
    {
        private Image _image;

        public override void Setup()
        {
            base.Setup();
            _image = GetComponent<Image>();
            
            // Check if image type is filled, if not it's good to have an error message.
            if (_image.type != Image.Type.Filled)
            {
                Guard<UserInterfaceLogConfig>.Error("UISpriteFillProgressBase requires Image component to be of type Filled." +
                                                    $"Found unsupported type: {_image.type} on {name}");
                
            }
        }

        public override void Render(float dataContext)
        {
            _image.fillAmount = dataContext;
        }
    }
}