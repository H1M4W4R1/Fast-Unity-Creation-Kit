using UnityEngine;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity.Interfaces.Interaction
{
    /// <summary>
    /// Represents an object that can be double-clicked.
    /// </summary>
    public interface IDoubleClickable : IClickable
    {
        /// <summary>
        /// Represents the time of the last click.
        /// </summary>
        internal float LastClickTime { get; set; }

        /// <summary>
        /// Represents the time threshold for a double click.
        /// If the time between two clicks is less than this value, the click is considered a double click.
        /// </summary>
        internal float DoubleClickTimeThreshold { get; }

        /// <summary>
        /// Default implementation for the OnClick method.
        /// </summary>
        /// <param name="pointerData">The pointer event data.</param>
        void IClickable.OnClick(PointerEventData pointerData)
        {
            // Do nothing.
        }
        
        /// <summary>
        /// Called when the object is double-clicked.
        /// </summary>
        public void OnDoubleClick();
        
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            // Handle click in case this object is also single-clickable
            // to remove requirement of overwriting this method to remove
            // multi-interface collision issue.
            OnClick(eventData);
            
            // Check if the time between the last click and the current click is less than the double click threshold.
            if (Time.time - LastClickTime < DoubleClickTimeThreshold)
            {
                OnDoubleClick();
            }
            else
            {
                LastClickTime = Time.time;
            }
        }
    }
}