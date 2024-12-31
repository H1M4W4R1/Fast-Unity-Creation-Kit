using FastUnityCreationKit.UI.Context.Providers.Utility;
using FastUnityCreationKit.UI.Elements.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Elements.Callbacks
{
    /// <summary>
    /// Event callback for toggle value changed.
    /// Supported only by data context providers.
    /// </summary>
    public interface IToggleValueChangedProviderCallback : IDataContextProvider
    {
        void OnToggleValueChanged([NotNull] UIToggle toggle, bool value);
    }
}