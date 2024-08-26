using FastUnityCreationKit.UI.Abstract;
using JetBrains.Annotations;
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
        /// Should be automatically assigned in the <see cref="Setup"/> method.
        /// </summary>
        [NotNull] protected TextMeshProUGUI unityText = default!;
        
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