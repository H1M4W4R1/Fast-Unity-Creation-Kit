using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;
using TMPro;
using UnityEngine;

namespace FastUnityCreationKit.UI.Elements.Abstract
{
    public abstract class UIText<TSelfUIObject> : UIObject<TSelfUIObject>, 
        IRenderable<TSelfUIObject, string> 
        where TSelfUIObject : UIText<TSelfUIObject>, new()
    {
        private TextMeshProUGUI _textMeshProUGUI;
        
        protected TextMeshProUGUI Text => _textMeshProUGUI;

        public override void Setup()
        {
            base.Setup();
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        public virtual void Render<TTargetDataContext>(TTargetDataContext usingDataContext)
        {
            // Check if data context is IStringContext and set text
            if(usingDataContext is string stringContext)
                _textMeshProUGUI.text = stringContext;
            else
                Debug.LogError($"Invalid data context type {usingDataContext.GetType().Name} for {GetType().Name}.");
        }
    }
}