using FastUnityCreationKit.UI.Elements.Abstract;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace FastUnityCreationKit.UI.Elements.Base.Progress
{
    [RequireComponent(typeof(Slider))]
    public abstract class UISliderProgressBase : UIProgress
    {
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [NotNull] private Slider _slider = null!;

        public override void Setup()
        {
            base.Setup();

            // Get the slider component and disable interaction - we can't modify
            // progress bar value directly.
            _slider = GetComponent<Slider>();
            _slider.interactable = false;

            // Ensure slider setup is done correctly
            _slider.minValue = 0;
            _slider.maxValue = PROGRESS_MULTIPLIER;
        }

        public override void Render(float dataContext)
        {
            _slider.value = dataContext * PROGRESS_MULTIPLIER;
        }
    }
}