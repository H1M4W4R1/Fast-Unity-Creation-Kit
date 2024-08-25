using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Elements.Events;
using UnityEngine;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements
{
    /// <summary>
    /// Represents a button.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class UIButton<TSelf> : UIObject, IUIObjectWithSetup
        where TSelf : UIButton<TSelf>
    {
        /// <summary>
        /// Reference to the Unity button.
        /// </summary>
        protected Button unityButton;
        
        /// <summary>
        /// Set up of the button.
        /// </summary>
        protected virtual void ButtonSetup()
        {
            
        }
        
        /// <summary>
        /// Tear down of the button.
        /// </summary>
        protected virtual void ButtonTearDown()
        {
            
        }
        
        /// <inheritdoc/>
        public void Setup()
        {
            unityButton = GetComponent<Button>();
            ButtonSetup();
            
            // Register the event
            unityButton.onClick.AddListener(_OnClick);
        }

        /// <inheritdoc/>
        public void TearDown()
        {
            ButtonTearDown();
        }

        /// <summary>
        /// Internal event handler for the button click.
        /// Forwards the event to the <see cref="OnClick"/> method.
        /// Also triggers <see cref="ButtonClickEventChannel{TSelf}.TriggerEvent"/>.
        /// </summary>
        private void _OnClick()
        {
            OnClick();
            ButtonClickEventChannel<TSelf>.TriggerEvent();
        }
        
        /// <summary>
        /// Event handler for the button click.
        /// </summary>
        public abstract void OnClick();
    }
}