using Cysharp.Threading.Tasks;
using FastUnityCreationKit.UI.Abstract;
using Sirenix.OdinInspector;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FastUnityCreationKit.UI.Elements
{
    /// <summary>
    /// Localized version of the <see cref="UIText{TSelf}"/>.
    /// </summary>
    /// <remarks>
    /// To change the text you need to implement <see cref="IUIObjectWithDataContext{TData}"/>
    /// and change <see cref="localizedString"/> variables within
    /// <see cref="IUIObjectWithDataContext{TData}.OnDataContextChangedAsync"/>
    /// <br/><br/>
    /// It is possible to use this class directly as UI object, however it is not recommended
    /// and better approach is to inherit from this class and use name that describes a purpose of the text.
    /// This way designers can easily understand what the text is used for and attach script to correct object.
    /// </remarks>
    public class LocalizedUIText : UIText<LocalizedUIText>, IRefreshable
    {
        /// <summary>
        /// String that should be displayed in the text element.
        /// If string is set automatically use a predefined empty key to fix
        /// [Required] attribute error.
        /// </summary>
        [Required]
        public LocalizedString localizedString;

        /// <inheritdoc/>
        protected sealed override void TextSetup()
        {
            base.TextSetup();

            // Register the refresh event
            localizedString.ValueChanged += OnLocalizedStringChanged;
        }

        /// <inheritdoc/>
        protected sealed override void TextTearDown()
        {
            base.TextTearDown();

            // Unregister the refresh event
            localizedString.ValueChanged -= OnLocalizedStringChanged;
        }

        /// <summary>
        /// Event handler for the localized string change.
        /// </summary>
        private void OnLocalizedStringChanged(IVariable obj)
        {
            // Convert this to refreshable object and call the refresh method
            IRefreshable refreshable = this;
            refreshable.RefreshAsync();
        }

        public override async UniTask RenderAsync()
        {
            // Request the localized string
            AsyncOperationHandle<string> operationHandle = localizedString.GetLocalizedStringAsync();
            
            // Wait for the operation to complete and set the text
            string text = await operationHandle.Task;
            unityText.text = text;
        }
    }
}