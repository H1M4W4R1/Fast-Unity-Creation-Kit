using FastUnityCreationKit.UI.Context.Providers.Utility;
using FastUnityCreationKit.UI.Elements.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Elements.Callbacks
{
    /// <summary>
    /// Represents a callback for slider value changed event.
    /// Supported only by data context providers.
    /// </summary>
    public interface ISliderValueChangedProviderCallback : IDataContextProvider
    {
        void OnSliderValueChanged([NotNull] UISlider slider, float value);
    }
}