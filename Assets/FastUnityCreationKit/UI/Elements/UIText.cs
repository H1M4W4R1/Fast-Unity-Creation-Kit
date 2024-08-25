using FastUnityCreationKit.UI.Abstract;
using TMPro;
using UnityEngine;

namespace FastUnityCreationKit.UI.Elements
{
    /// <summary>
    /// Represents a text element in the UI.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class UIText<TSelf> : UIObject, IUIObjectWithSetup, IRenderable
        where TSelf : UIText<TSelf>
    {
        /// <summary>
        /// Reference to the Unity text element.
        /// </summary>
        protected TextMeshProUGUI unityText;
        
        /// <summary>
        /// Local setup of the text element.
        /// </summary>
        protected virtual void TextSetup()
        {
            
        }
        
        /// <summary>
        /// Local tear down of the text element.
        /// </summary>
        protected virtual void TextTearDown()
        {
            
        }
        
        public void Setup()
        {
            unityText = GetComponent<TextMeshProUGUI>();
            TextSetup();
        }

        public void TearDown()
        {
            TextTearDown();
        }

        /// <summary>
        /// Text must be rendered.
        /// </summary>
        public abstract void Render();
    }
}