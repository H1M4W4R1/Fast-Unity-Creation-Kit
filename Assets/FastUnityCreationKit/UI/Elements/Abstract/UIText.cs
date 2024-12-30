using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;
using TMPro;
using UnityEngine;

namespace FastUnityCreationKit.UI.Elements.Abstract
{
    [RequireComponent(typeof(TextMeshProUGUI))]
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

        public virtual void Render(string dataContext)
        {
            _textMeshProUGUI.text = dataContext;
        }
    }
}