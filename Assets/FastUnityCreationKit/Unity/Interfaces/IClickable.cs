using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity.Interfaces
{
    /// <summary>
    /// Represents an object that can be clicked.
    /// </summary>
    public interface IClickable
    {
        /// <summary>
        /// Called when the object is clicked.
        /// </summary>
        public void OnClick(PointerEventData pointerData);
    }
}