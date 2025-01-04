using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Context.Data.Text
{
    /// <summary>
    /// Provides raw string data to the renderer.
    /// This can be used to quickly update e.g. Character Name which will be constant string stored
    /// inside some other structure.
    /// </summary>
    public sealed class RawStringContextProvider : StringContextBaseProvider
    {
        /// <summary>
        /// Text that will be rendered.
        /// </summary>
        [TitleGroup(PROVIDER_CONFIGURATION)]
        [ShowInInspector]
        [SerializeField]
        [NotNull]
        private string text = string.Empty;

        /// <summary>
        /// Updates the text that will be rendered.
        /// </summary>
        /// <param name="newText">New text that will be rendered.</param>
        public void SetText([NotNull] string newText)
        {
            text = newText;
            NotifyContextHasChanged();
        }

        [NotNull] public override string Provide()
        {
            return text;
        }
        
        // This handles text being edited within the editor
        // if UpdateText method is used to change text
        // all notifications will be handled automatically.
#if UNITY_EDITOR
        [NotNull] private string _renderedText = string.Empty;
        
        public void FixedUpdate()
        {
            // Check if text has changed and update it
            if (_renderedText == text) return;
            _renderedText = text;

            // Notify that the object has been updated
            NotifyContextHasChanged();
        }
#endif
    }
}