using FastUnityCreationKit.UI.Abstract;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements.Base.Input
{
    /// <summary>
    ///     Represents a button.
    /// </summary>
    [RequireComponent(typeof(Button))] public abstract class UIButtonBase : UIObjectBase
    {
        /// <summary>
        ///     Internal reference to the button.
        /// </summary>
        [NotNull]
        // ReSharper disable once NullableWarningSuppressionIsUsed
        private Button _button = null!;

        public override void Setup()
        {
            base.Setup();

            _button = GetComponent<Button>();
            _button.onClick.AddListener(_OnClick);
        }

        internal void _OnClick()
        {
            OnClick();
        }

        /// <summary>
        ///     Event that is invoked when the button is clicked.
        /// </summary>
        protected abstract void OnClick();

        /// <summary>
        ///     Changes the interactable state of the button.
        /// </summary>
        /// <param name="interactable">Interactable state.</param>
        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        /// <summary>
        ///     Simulates a click on the button.
        /// </summary>
        public void SimulateClick()
        {
            _OnClick();
        }
    }
}