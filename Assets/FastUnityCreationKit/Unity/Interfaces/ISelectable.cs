using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Interfaces
{
    /// <summary>
    /// Represents an object that can be selected.
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Checks if the multi-selection key is pressed.
        /// TODO: Support new input system.
        /// </summary>
        public static bool MultiSelectionKeyIsPressed =>
            Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

        /// <summary>
        /// List of all selected objects.
        /// </summary>
        [UsedImplicitly] protected static List<ISelectable> SelectedObjects { get; } = new();
        
        public bool IsSelected { get; protected set; }

        /// <summary>
        /// Reverses the selection state of the object.
        /// Called when object is clicked.
        /// </summary>
        internal void _ReverseSelectionState() => _OnSelectionChanged(!IsSelected);
        
        /// <summary>
        /// Internal method that is called when the selection state of the object changes.
        /// </summary>
        internal void _OnSelectionChanged(bool newSelectionState)
        {
            // Clear list of selected objects if object does not support multiple selection or
            // if required keys are not pressed.
            if (!MultiSelectionKeyIsPressed) SelectedObjects.Clear();
            else if (this is not ISupportsMultipleSelection) SelectedObjects.Clear();
            
            bool previousSelectionState = IsSelected;
            IsSelected = newSelectionState;
            OnSelectionChanged(previousSelectionState, newSelectionState);
            
            // Update the list of selected objects.
            if (IsSelected) SelectedObjects.Add(this);
            else SelectedObjects.Remove(this);
        }
        
        /// <summary>
        /// Called when the object is selected.
        /// </summary>
        public void OnSelectionChanged(bool previousSelectionState, bool newSelectionState);
    }
}