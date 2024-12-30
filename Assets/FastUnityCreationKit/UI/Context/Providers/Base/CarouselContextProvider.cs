using System.Collections.Generic;
using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.UI.Elements.Utility.Internal.Carousel;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Context.Providers.Base
{
    /// <summary>
    /// Carousel context provider - used to provide context in carousel style (with right/left buttons).
    /// </summary>
    /// <typeparam name="TContextType">Type of context.</typeparam>
    public abstract class CarouselContextProvider<TContextType> : DataContextProviderBase<TContextType>
    {
        [Required] [SerializeField] [TabGroup("Configuration")] private CarouselButton previousButton;
        [Required] [SerializeField] [TabGroup("Configuration")] private CarouselButton nextButton;
        
        /// <summary>
        /// List of all elements in the carousel.
        /// </summary>
        [ItemNotNull] [NotNull] protected List<TContextType> _elements = new List<TContextType>();

        protected int CurrentIndex { get; private set; } = 0;
        
        public virtual bool WrapForward { get; set; } = true;
        public virtual bool WrapBackward { get; set; } = true;
        
        protected override void Awake()
        {
            base.Awake();
            
            previousButton.onButtonPressed += OnPreviousButtonPressed;
            nextButton.onButtonPressed += OnNextButtonPressed;

            // Setup elements
            SetupElements();
        }

        /// <summary>
        /// Used to set up elements within carousel provider
        /// (aka. add elements to the carousel).
        /// </summary>
        protected abstract void SetupElements();

        /// <summary>
        /// Adds an element to the carousel.
        /// </summary>
        /// <param name="element">Element to add.</param>
        protected void AddElement([NotNull] TContextType element)
        {
            // Add element if not yet present
            // to prevent duplicates
            if(!_elements.Contains(element))
                _elements.Add(element);
            
            NotifyContextHasChanged();
        }
        
        /// <summary>
        /// Removes an element from the carousel.
        /// </summary>
        /// <param name="element">Element to remove.</param>
        protected void RemoveElement([NotNull] TContextType element)
        {
            if(_elements.Contains(element))
                _elements.Remove(element);

            NotifyContextHasChanged();
        }

        /// <summary>
        /// Inserts an element at the specified index.
        /// </summary>
        /// <param name="index">Index to insert the element at.</param>
        /// <param name="element">Element to insert.</param>
        protected void InsertElementAt(int index, [NotNull] TContextType element)
        {
            // Ensure that the index is within the bounds
            // if not, add the element to the end of the list
            // as it is the acceptable safe behavior
            if (index < 0 || index >= _elements.Count)
            {
                AddElement(element);
                return;
            }
            
            _elements.Insert(index, element);
            NotifyContextHasChanged();
        }
        
        /// <summary>
        /// Removes an element at the specified index.
        /// </summary>
        /// <param name="index">Index of the element to remove.</param>
        protected void RemoveElementAt(int index)
        {
            // If no element with the specified index exists, return
            // as we cannot remove an element that does not exist
            if(index < 0 || index >= _elements.Count)
                return;
            
            _elements.RemoveAt(index);
            NotifyContextHasChanged();
        }
        
        /// <summary>
        /// Removes all elements from the carousel.
        /// </summary>
        protected void Clear()
        {
            _elements.Clear();
            NotifyContextHasChanged();
        }
        
        public override TContextType Provide()
        {
            // Ensure that the list is not empty
            if(_elements.Count == 0)
                return default;
            
            // Ensure that the index is within the bounds
            // we need to ensure that just in case some overrides will change the index
            // to be out of bounds
            if(CurrentIndex < 0 || CurrentIndex >= _elements.Count)
                CurrentIndex = 0;
            
            // Return the current element
            return _elements[CurrentIndex];
        }
        
        private void OnNextButtonPressed()
        {
            int index = CurrentIndex;
            CurrentIndex++;
            
            // Wrap around if needed, also prevents index out of bounds
            if(CurrentIndex >= _elements.Count)
                CurrentIndex = WrapForward ? 0 : _elements.Count - 1;
            
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
                CurrentIndex = WrapBackward ? _elements.Count - 1 : 0;
            
            // Notify that the context has changed (only if index was changed)
            // to prevent unnecessary updates
            if(index != CurrentIndex)
                NotifyContextHasChanged();
        }
    }
}