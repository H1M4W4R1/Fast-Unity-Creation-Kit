using FastUnityCreationKit.UI.Abstract;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements.Base.Input
{
    /// <summary>
    /// Represents a slider element.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public abstract class UISliderBase : UIObjectBase
    {
        /// <summary>
        /// Internal reference to the slider component.
        /// </summary>
        [NotNull]
        private Slider _slider = null!;

        public override void Setup()
        {
            base.Setup();

            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(_OnValueChanged);
        }

        internal void _OnValueChanged(float value)
        {
            OnValueChanged(value);
        }

        /// <summary>
        /// Invoked when the slider value is changed.
        /// </summary>
        /// <param name="value">New value.</param>
        protected abstract void OnValueChanged(float value);

        /// <summary>
        /// Changes the interactable state of the slider.
        /// </summary>
        public void SetInteractable(bool interactable) => _slider.interactable = interactable;

        /// <summary>
        /// Sets the value of the slider.
        /// </summary>
        /// <param name="value">New value.</param>
        public void SetValue(float value) =>_slider.value = value;

        /// <summary>
        /// Sets the minimum value of the slider.
        /// </summary>
        public void SetMinValue(float value) => _slider.minValue = value;


        /// <summary>
        /// Sets the maximum value of the slider.
        /// </summary>
        public void SetMaxValue(float value) => _slider.maxValue = value;

        public void SimulateValueChange(float value) => _OnValueChanged(value);

        /// <summary>
        /// Gets the current value of the slider.
        /// </summary>
        /// <returns>Current value.</returns>
        public float GetValue() => _slider.value;
    }
}