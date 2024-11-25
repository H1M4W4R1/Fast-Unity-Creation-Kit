using System;
using FastUnityCreationKit.Core.Utility.Initialization;
using FastUnityCreationKit.Unity.Events;
using FastUnityCreationKit.Unity.Events.Data;
using FastUnityCreationKit.Unity.Events.Input;
using FastUnityCreationKit.Unity.Events.Interfaces;
using FastUnityCreationKit.Unity.Events.Unity;
using FastUnityCreationKit.Unity.Interfaces;
using FastUnityCreationKit.Unity.Structure.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity
{
    /// <summary>
    /// Base class for all MonoBehaviours compatible with the FastUnityCreationKit.
    /// Used to automatically handle interface processing.
    /// </summary>
    public abstract class FastMonoBehaviour<TSelf> : FastMonoBehaviour, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler
        where TSelf : FastMonoBehaviour<TSelf>, new()
    {
        /// <summary>
        /// Avoid overriding the Awake method. Implement the <see cref="IInitializable"/> interface instead
        /// and use the <see cref="IInitializable.OnInitialize"/> method.
        /// </summary>
        protected virtual void Awake()
        {
            // Register this object to the object registry.
            FastMonoBehaviourManager.Instance.RegisterFastMonoBehaviour(this);
            
            // Initialize the object if it implements the IInitializable interface.
            if (this is IInitializable initializable)
            {
                initializable.Initialize();
                
                // Trigger the OnObjectInitialized event.
                OnObjectInitializedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourEventData<TSelf>((this as TSelf)!));
            }

            // Call creation event to notify listeners that the object has been created.
            OnObjectCreatedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourEventData<TSelf>((this as TSelf)!));
            
            // Check if this object supports "create" callback.
            if(this is ICreateCallback createCallback)
                createCallback.OnObjectCreated();
            
            // If this is both clickable and selectable print warning.
            if (this is IClickable and ISelectable)
                Debug.LogWarning("Object is both clickable and selectable. This may cause unexpected behavior.");
        }

        protected void OnEnable()
        {
            // Trigger the OnObjectActivated event.
            OnObjectActivatedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourEventData<TSelf>((this as TSelf)!));
        }
        
        protected void OnDisable()
        {
            // Trigger the OnObjectDeactivated event.
            OnObjectDeactivatedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourEventData<TSelf>((this as TSelf)!));
        }

        protected void OnDestroy()
        {
            // Trigger the OnObjectDestroyed event.
            OnObjectDestroyedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourEventData<TSelf>((this as TSelf)!));
            
            // Callback must be called after event to prevent weird behavior.
            if(this is IDestroyCallback destroyCallback)
                destroyCallback.OnObjectDestroyed();
            
            // Unregister this object from the object registry.
            FastMonoBehaviourManager.Instance.UnregisterFastMonoBehaviour(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (this is IHoverable hoverable)
            {
                hoverable.OnHoverEnter(eventData);
                
                // Call the OnHoverStart event.
                OnObjectHoverStartEvent<TSelf>.TriggerEvent(new FastMonoBehaviourPointerEventData<TSelf>(eventData, (this as TSelf)!));
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (this is IHoverable hoverable)
            {
                hoverable.OnHoverExit(eventData);

                // Call the OnHoverEnd event.
                OnObjectHoverEndEvent<TSelf>.TriggerEvent(new FastMonoBehaviourPointerEventData<TSelf>(eventData, (this as TSelf)!));
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (this is ISelectable selectable)
            {
                selectable._ReverseSelectionState();
                
                OnObjectSelectionChangedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourSelectionPointerEventData<TSelf>(eventData, (this as TSelf)!, selectable.IsSelected));
                
                // Trigger selection events
                if (selectable.IsSelected) OnObjectSelectedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourSelectionPointerEventData<TSelf>(eventData, (this as TSelf)!, selectable.IsSelected));
                else OnObjectDeselectedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourSelectionPointerEventData<TSelf>(eventData, (this as TSelf)!, selectable.IsSelected));
            }

            if (this is IClickable clickable)
            {
                clickable.OnClick(eventData);
                
                // Call the OnClick event.
                OnObjectClickedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourPointerEventData<TSelf>(eventData, (this as TSelf)!));
            }

            if(this is IDoubleClickable doubleClickable)
            {
                // Check if the time between two clicks is less than the threshold.
                if (Time.time - doubleClickable.LastClickTime < doubleClickable.DoubleClickTimeThreshold)
                {
                    doubleClickable.OnDoubleClick();
                    
                    // Call the OnDoubleClick event.
                    OnObjectDoubleClickedEvent<TSelf>.TriggerEvent(
                        new FastMonoBehaviourPointerEventData<TSelf>(eventData, (this as TSelf)!));
                }
            }
        }
    }

    public abstract class FastMonoBehaviour : MonoBehaviour
    {
        
    }
}