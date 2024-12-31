using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Elements.Callbacks;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements.Abstract
{
    /// <summary>
    /// Represents a toggle element.
    /// </summary>
    public abstract class UIToggle : UIObjectBase
    {
        /// <summary>
        /// Internal reference to the toggle.
        /// </summary>
        private Toggle _toggle;

        public override void Setup()
        {
            base.Setup();
            
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(_OnValueChanged);
        }

        internal void _OnValueChanged(bool value)
        {
            // Perform callback to the provider if it exists
            IToggleValueChangedProviderCallback provider = GetProviderByType<IToggleValueChangedProviderCallback>();
            provider?.OnToggleValueChanged(this, value);
            
            OnValueChanged(value);
        }
        
        /// <summary>
        /// Event that is invoked when the toggle value is changed.
        /// </summary>
        protected abstract void OnValueChanged(bool value);
        
        /// <summary>
        /// Changes the interactable state of the toggle.
        /// </summary>
        /// <param name="interactable">Interactable state.</param>
        public void SetInteractable(bool interactable) => _toggle.interactable = interactable;

        /// <summary>
        /// Simulates a value change on the toggle.
        /// </summary>
        public void SimulateValueChange(bool value) => _OnValueChanged(value);

        /// <summary>
        /// Sets the value of the toggle.
        /// </summary>
        /// <param name="value">New value.</param>
        public void SetValue(bool value) => _toggle.isOn = value;
        
        /// <summary>
        /// Gets the value of the toggle.
        /// </summary>
        /// <returns>Current value.</returns>
        public bool GetValue() => _toggle.isOn;
    }
}