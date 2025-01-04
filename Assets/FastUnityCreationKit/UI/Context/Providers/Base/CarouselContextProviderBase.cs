using FastUnityCreationKit.UI.Elements.Utility.Internal.Carousel;
using FastUnityCreationKit.Core.Logging;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Context.Providers.Base
{
    /// <summary>
    /// Carousel context provider - used to provide context in carousel style (with right/left buttons).
    /// </summary>
    /// <typeparam name="TContextType">Type of context.</typeparam>
    public abstract class CarouselContextProviderBase<TContextType> : ListContextProviderBase<TContextType>
    {
        [Required] [SerializeField] [TitleGroup(GROUP_CONFIGURATION)] private CarouselButton previousButton;
        [Required] [SerializeField] [TitleGroup(GROUP_CONFIGURATION)] private CarouselButton nextButton;

        protected int CurrentIndex { get; private set; }
        
        public virtual bool WrapForward => true;
        public virtual bool WrapBackward => true;

        protected override void Setup()
        {
            base.Setup();
            
            // Ensure that the buttons are not null
            if(previousButton == null || nextButton == null)
            {
                Guard<ValidationLogConfig>.Error($"Buttons are not set on {name}. Cannot setup carousel.");
                return;
            }
            
            previousButton.onButtonPressed += OnPreviousButtonPressed;
            nextButton.onButtonPressed += OnNextButtonPressed;
        }

        public override TContextType Provide()
        {
            // Ensure that the list is not empty
            if (Count == 0)
            {
                Guard<ValidationLogConfig>.Error($"The list is empty on {name}. Cannot provide context.");
                return default;
            }

            // Ensure that the index is within the bounds
            // we need to ensure that just in case some overrides will change the index
            // to be out of bounds
            if(CurrentIndex < 0 || CurrentIndex >= Count)
                CurrentIndex = 0;
            
            // Return the current element
            return GetElementAt(CurrentIndex);
        }
        
        private void OnNextButtonPressed()
        {
            int index = CurrentIndex;
            CurrentIndex++;
            
            // Wrap around if needed, also prevents index out of bounds
            if(CurrentIndex >= Count)
                CurrentIndex = WrapForward ? 0 : Count - 1;
            
            // Notify that the context has changed (only if index was changed)
            // to prevent unnecessary updates
            if(index != CurrentIndex)
                NotifyContextHasChanged();
        }

        private void OnPreviousButtonPressed()
        {
            int index = CurrentIndex;
            CurrentIndex--;
            
            // Wrap around if needed, also prevents index out of bounds
            if(CurrentIndex < 0)
                CurrentIndex = WrapBackward ? Count - 1 : 0;
            
            // Notify that the context has changed (only if index was changed)
            // to prevent unnecessary updates
            if(index != CurrentIndex)
                NotifyContextHasChanged();
        }
    }
}