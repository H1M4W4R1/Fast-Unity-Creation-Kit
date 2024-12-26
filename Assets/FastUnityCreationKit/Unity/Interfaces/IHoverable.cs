using FastUnityCreationKit.Unity.Events.Data;
using FastUnityCreationKit.Unity.Events.Input;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity.Interfaces
{
    /// <summary>
    /// Represents an object that can be hovered over.
    /// </summary>
    public interface IHoverable<TSelf> : IPointerEnterHandler, IPointerExitHandler
        where TSelf : FastMonoBehaviour<TSelf>, new()
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
            
            // Call the OnHoverStart event.
            OnObjectHoverStartEvent<TSelf>.TriggerEvent(
                new FastMonoBehaviourPointerEventData<TSelf>(eventData, (this as TSelf)!));
        }
        
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            OnHoverExit(eventData);
            
            // Call the OnHoverEnd event.
            OnObjectHoverEndEvent<TSelf>.TriggerEvent(
                new FastMonoBehaviourPointerEventData<TSelf>(eventData, (this as TSelf)!));
        }
    }
}