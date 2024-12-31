using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Elements.Callbacks;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements.Abstract
{
    /// <summary>
    /// Represents a button.
    /// </summary>
    public abstract class UIButton : UIObjectBase
    {
        /// <summary>
        /// Internal reference to the button.
        /// </summary>
        private Button _button;

        public override void Setup()
        {
            base.Setup();
            
            _button = GetComponent<Button>();
            _button.onClick.AddListener(_OnClick);
        }

        internal void _OnClick()
        {
            // Perform callback to the provider if it exists
            IButtonClickedProviderCallback provider = GetProviderByType<IButtonClickedProviderCallback>();
            provider?.OnButtonClicked(this);
            
            OnClick();
        }
        
        /// <summary>
        /// Event that is invoked when the button is clicked.
        /// </summary>
        protected abstract void OnClick();
        
        /// <summary>
        /// Changes the interactable state of the button.
        /// </summary>
        /// <param name="interactable">Interactable state.</param>
        public void SetInteractable(bool interactable) => _button.interactable = interactable;

        /// <summary>
        /// Simulates a click on the button.
        /// </summary>
        public void SimulateClick() => _OnClick();
    }
}