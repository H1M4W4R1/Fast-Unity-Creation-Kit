using FastUnityCreationKit.UI.Context.Providers.Utility;
using FastUnityCreationKit.UI.Elements.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Elements.Callbacks
{
    /// <summary>
    /// Represents a callback for button clicked event.
    /// Supported only by data context providers.
    /// </summary>
    public interface IButtonClickedProviderCallback : IDataContextProvider
    {
        void OnButtonClicked([NotNull] UIButton button);
    }
}