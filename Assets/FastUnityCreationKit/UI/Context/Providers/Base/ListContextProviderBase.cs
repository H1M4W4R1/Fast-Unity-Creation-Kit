using System;
using System.Collections.Generic;
using FastUnityCreationKit.Core.Logging;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

namespace FastUnityCreationKit.UI.Context.Providers.Base
{
    public abstract class ListContextProviderBase<TContextType> : DataContextProviderBase<TContextType>
    {
        /// <summary>
        ///     List of all elements in the carousel.
        /// </summary>
        [ItemNotNull] [NotNull] [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)]
        private readonly List<TContextType> _elements = new();

        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] public int Count
            => _elements.Count;

        protected override void Setup()
        {
            base.Setup();

            SetupElements();
        }

        /// <summary>
        ///     Adds an element to the carousel.
        /// </summary>
        /// <param name="element">Element to add.</param>
        protected void AddElement([NotNull] TContextType element)
        {
            // Add element if not yet present
            // to prevent duplicates
            if (!_elements.Contains(element)) _elements.Add(element);

            NotifyContextHasChanged();
        }

        /// <summary>
        ///     Removes an element from the carousel.
        /// </summary>
        /// <param name="element">Element to remove.</param>
        protected void RemoveElement([NotNull] TContextType element)
        {
            if (_elements.Contains(element)) _elements.Remove(element);

            NotifyContextHasChanged();
        }

        /// <summary>
        ///     Inserts an element at the specified index.
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
        ///     Removes an element at the specified index.
        /// </summary>
        /// <param name="index">Index of the element to remove.</param>
        protected void RemoveElementAt(int index)
        {
            // If no element with the specified index exists, return
            // as we cannot remove an element that does not exist
            if (index < 0 || index >= _elements.Count) return;

            _elements.RemoveAt(index);
            NotifyContextHasChanged();
        }

        /// <summary>
        ///     Gets an element at the specified index.
        /// </summary>
        /// <param name="index">Index of the element to get.</param>
        /// <returns>Element at the specified index.</returns>
        [CanBeNull] protected TContextType GetElementAt(int index)
        {
            if (index < 0 || index >= _elements.Count)
            {
                Guard<ValidationLogConfig>.Error($"Requested index {index} is out of bounds on {name}.");
                return default;
            }

            return _elements[index];
        }

        /// <summary>
        ///     Replaces an element at the specified index.
        /// </summary>
        /// <param name="index">Index of the element to replace.</param>
        /// <param name="element">Element to replace with.</param>
        protected void SetElementAt(int index, [NotNull] TContextType element)
        {
            if (index < 0 || index >= _elements.Count)
            {
                Guard<ValidationLogConfig>.Error($"Requested index {index} is out of bounds on {name}.");
                return;
            }

            _elements[index] = element;
            NotifyContextHasChanged();
        }

        /// <summary>
        ///     Removes all elements from the carousel.
        /// </summary>
        protected void Clear()
        {
            _elements.Clear();
            NotifyContextHasChanged();
        }

        /// <summary>
        ///     Used to set up elements within list provider
        ///     (aka. add elements to the carousel).
        /// </summary>
        protected abstract void SetupElements();

        public override TContextType Provide()
        {
            throw new NotSupportedException("This method is not supported for ListContextProvider.");
        }

        /// <summary>
        ///     Provides a random element from the list.
        /// </summary>
        /// <returns>Random element from the list or default if not found.</returns>
        [CanBeNull] public TContextType ProvideRandom()
        {
            return _elements.Count == 0 ? default : _elements[Random.Range(0, _elements.Count)];
        }

        public override TContextType ProvideAt(int index)
        {
            if (index < 0 || index >= _elements.Count) return default;

            return _elements[index];
        }
    }
}