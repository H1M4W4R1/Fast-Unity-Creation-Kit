using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity.Interfaces.Interaction
{
    /// <summary>
    ///     Represents an object that can be selected.
    /// </summary>
    public interface ISelectable : IPointerClickHandler
    {
        /// <summary>
        ///     List of all selected objects.
        /// </summary>
        [UsedImplicitly] protected static List<ISelectable> SelectedObjects { get; } = new();


        /// <summary>
        ///     Checks if the multi-selection key is pressed.
        ///     TODO: Support new input system.
        /// </summary>
        public static bool MultiSelectionKeyIsPressed =>
            Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        public bool IsSelected { get; protected set; }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            _ReverseSelectionState();

            // Trigger selection events
            if (IsSelected)
                OnSelected();
            else
                OnDeselected();
        }

        /// <summary>
        ///     Called when the selection state of the object changes to a selected state.
        /// </summary>
        void OnSelected();

        /// <summary>
        ///     Called when the selection state of the object changes to a deselected state.
        /// </summary>
        void OnDeselected();

        /// <summary>
        ///     Called when the selection state of the object changes.
        /// </summary>
        /// <param name="previousSelectionState">Old selection state.</param>
        /// <param name="newSelectionState">New selection state.</param>
        void OnSelectionChanged(bool previousSelectionState, bool newSelectionState);

        /// <summary>
        ///     Reverses the selection state of the object.
        ///     Called when object is clicked.
        /// </summary>
        internal void _ReverseSelectionState()
        {
            _OnSelectionChanged(!IsSelected);
        }

        /// <summary>
        ///     Internal method that is called when the selection state of the object changes.
        /// </summary>
        internal void _OnSelectionChanged(bool newSelectionState)
        {
            // Clear list of selected objects if object does not support multiple selection or
            // if required keys are not pressed.
            if (!MultiSelectionKeyIsPressed)
                SelectedObjects.Clear();
            else if (this is not ISupportsMultipleSelection) SelectedObjects.Clear();

            bool previousSelectionState = IsSelected;
            IsSelected = newSelectionState;
            OnSelectionChanged(previousSelectionState, newSelectionState);

            // Update the list of selected objects.
            if (IsSelected)
                SelectedObjects.Add(this);
            else
                SelectedObjects.Remove(this);
        }
    }
}