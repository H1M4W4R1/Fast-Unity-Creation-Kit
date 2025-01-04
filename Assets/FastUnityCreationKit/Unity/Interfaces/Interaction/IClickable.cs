using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity.Interfaces.Interaction
{
    /// <summary>
    ///     Represents an object that can be clicked.
    /// </summary>
    public interface IClickable : IPointerClickHandler
    {
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnClick(eventData);
        }

        /// <summary>
        ///     Called when the object is clicked.
        /// </summary>
        public void OnClick(PointerEventData pointerData);
    }
}