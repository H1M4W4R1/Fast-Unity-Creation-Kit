using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Context;
using FastUnityCreationKit.UI.Data.Interfaces;
using FastUnityCreationKit.UI.Interfaces;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements.Basic
{
    public abstract class UISprite<TSelfUIObject, TDataContextSealed> : UIObject<TSelfUIObject>,
        IRenderable<TSelfUIObject, TDataContextSealed> 
        where TDataContextSealed : DataContext<TDataContextSealed>, new()
        where TSelfUIObject : UIText<TSelfUIObject, TDataContextSealed>, new()
    {
        private Image _image;
        
        protected Image Image => _image;
        
        public override void Setup()
        {
            base.Setup();
            _image = GetComponent<Image>();
        }

        public virtual void Render<TTargetContext>(TTargetContext usingDataContext)
        {
            // Get sprite context
            if(usingDataContext is ISpriteContext spriteContext)
               _image.sprite = spriteContext.Image;
        }
    }
}