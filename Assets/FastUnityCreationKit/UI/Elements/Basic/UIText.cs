using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Context;
using FastUnityCreationKit.UI.Data.Interfaces;
using FastUnityCreationKit.UI.Interfaces;
using TMPro;

namespace FastUnityCreationKit.UI.Elements.Basic
{
    public abstract class UIText<TSelfUIObject, TDataContextSealed> : UIObject<TSelfUIObject>, 
        IRenderable<TSelfUIObject, TDataContextSealed> 
        where TDataContextSealed : DataContext<TDataContextSealed>, new()
        where TSelfUIObject : UIObject<TSelfUIObject>, new()
    {
        private TextMeshProUGUI _textMeshProUGUI;

        public override void Setup()
        {
            base.Setup();
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        public void Render<TTargetDataContext>(TTargetDataContext usingDataContext)
        {
            // Check if data context is IStringContext and set text
            if(usingDataContext is IStringContext stringContext)
                _textMeshProUGUI.text = stringContext.LocalizedText;
        }
    }
}