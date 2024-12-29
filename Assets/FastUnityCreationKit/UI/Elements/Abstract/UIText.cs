using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;
using TMPro;
using UnityEngine;

namespace FastUnityCreationKit.UI.Elements.Abstract
{
    public abstract class UIText : UIObject, IRenderable<string> 
    {
        /// <summary>
        /// Internal reference to the TextMeshProUGUI component.
        /// </summary>
        private TextMeshProUGUI _textMeshProUGUI;
        
        /// <summary>
        /// Exposes the TextMeshProUGUI component for child classes to use.
        /// </summary>
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