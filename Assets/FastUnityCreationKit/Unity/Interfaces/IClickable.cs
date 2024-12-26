using FastUnityCreationKit.Unity.Events.Data;
using FastUnityCreationKit.Unity.Events.Input;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity.Interfaces
{
    /// <summary>
    /// Represents an object that can be clicked.
    /// </summary>
    public interface IClickable<TSelf> : IPointerClickHandler
        where TSelf : FastMonoBehaviour<TSelf>, new()
    {
        /// <summary>
        /// Called when the object is clicked.
        /// </summary>
        public void OnClick(PointerEventData pointerData);

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnClick(eventData);
            
            // Call the OnClick event.
            OnObjectClickedEvent<TSelf>.TriggerEvent(
                new FastMonoBehaviourPointerEventData<TSelf>(eventData, (this as TSelf)!));
        }
    }
}