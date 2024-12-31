using FastUnityCreationKit.UI.Context.Providers.Utility;
using FastUnityCreationKit.UI.Elements.Abstract;
using FastUnityCreationKit.UI.Elements.Base;
using FastUnityCreationKit.UI.Elements.Base.Input;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Elements.Callbacks
{
    /// <summary>
    /// Event callback for toggle value changed.
    /// Supported only by data context providers.
    /// </summary>
    public interface IToggleValueChangedProviderCallback : IDataContextProvider
    {
        void OnToggleValueChanged([NotNull] UIToggleBase toggle, bool value);
    }
}