﻿using FastUnityCreationKit.UI.Abstract;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements.Base.Input
{
    /// <summary>
    ///     Represents a toggle element.
    /// </summary>
    [RequireComponent(typeof(Toggle))] public abstract class UIToggleBase : UIObjectBase
    {
        /// <summary>
        ///     Internal reference to the toggle.
        /// </summary>
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [NotNull] private Toggle _toggle = null!;

        public override void Setup()
        {
            base.Setup();

            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(_OnValueChanged);
        }

        internal void _OnValueChanged(bool value)
        {
            OnValueChanged(value);
        }

        /// <summary>
        ///     Event that is invoked when the toggle value is changed.
        /// </summary>
        protected abstract void OnValueChanged(bool value);

        /// <summary>
        ///     Changes the interactable state of the toggle.
        /// </summary>
        /// <param name="interactable">Interactable state.</param>
        public void SetInteractable(bool interactable)
        {
            _toggle.interactable = interactable;
        }

        /// <summary>
        ///     Simulates a value change on the toggle.
        /// </summary>
        public void SimulateValueChange(bool value)
        {
            _OnValueChanged(value);
        }

        /// <summary>
        ///     Sets the value of the toggle.
        /// </summary>
        /// <param name="value">New value.</param>
        public void SetValue(bool value)
        {
            _toggle.isOn = value;
        }

        /// <summary>
        ///     Gets the value of the toggle.
        /// </summary>
        /// <returns>Current value.</returns>
        public bool GetValue()
        {
            return _toggle.isOn;
        }
    }
}