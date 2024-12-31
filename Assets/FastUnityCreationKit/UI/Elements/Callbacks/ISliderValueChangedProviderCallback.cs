using FastUnityCreationKit.UI.Context.Providers.Utility;
using FastUnityCreationKit.UI.Elements.Abstract;
using FastUnityCreationKit.UI.Elements.Base;
using FastUnityCreationKit.UI.Elements.Base.Input;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Elements.Callbacks
{
    /// <summary>
    /// Represents a callback for slider value changed event.
    /// Supported only by data context providers.
    /// </summary>
    public interface ISliderValueChangedProviderCallback : IDataContextProvider
    {
        void OnSliderValueChanged([NotNull] UISliderBase sliderBase, float value);
    }
}