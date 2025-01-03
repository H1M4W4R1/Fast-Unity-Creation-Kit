using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity.Interfaces.Interaction
{
    /// <summary>
    /// Represents an object that can be hovered over.
    /// </summary>
    public interface IHoverable : IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// Event that is called when the object is hovered over.
        /// </summary>
        public void OnHoverEnter(PointerEventData pointerData);
        
        /// <summary>
        /// Event that is called when the object is no longer hovered over.
        /// </summary>
        public void OnHoverExit(PointerEventData pointerData);
        
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            OnHoverEnter(eventData);
        }
        
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            OnHoverExit(eventData);
        }
    }
}