using System.Collections.Generic;
using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Extensions;
using FastUnityCreationKit.UI.Interfaces;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.UI.Features.Snapping
{
    /// <summary>
    /// Represents a feature that allows snapping to a specific position.
    /// </summary>
    /// TODO: Consider if events should be propagated to children
    public abstract class SnapToFeature<TSnapObject> : UIFeature, IPointerUpHandler, IPointerDownHandler
        where TSnapObject : UIObject, ISnapTarget<TSnapObject>
    {
        /// <summary>
        /// Object that is currently snapped to.
        /// </summary>
        [CanBeNull] [ShowInInspector] [ReadOnly] [TabGroup("Debug")]
        private TSnapObject _currentlySnappedTo;
        
        /// <summary>
        /// Original position of the object.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Debug")]
        private Vector2 _originalPosition;
        
        /// <summary>
        /// If true next pointer up event will execute snap.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Debug")]
        private bool _searchForSnapActive;

        /// <summary>
        /// If true mouse position will be used to calculate snap position instead
        /// of transform position. It is recommended to use mouse position for snapping
        /// for better performance and user experience.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Debug")]
        protected virtual bool UseMousePosition => true;

        public override void Setup()
        {
            base.Setup();
            _originalPosition = transform.position;
        }

        private void Start()
        {
            // Snap to closest object after everything is initialized
            ExecuteSnap(transform.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _searchForSnapActive = true;
            _originalPosition = transform.position;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            // If search for snap is active, execute snap
            if(_searchForSnapActive)
                ExecuteSnap(UseMousePosition ? eventData.position : transform.position);
        }

        internal void ExecuteSnap(Vector2 position)
        {
            OnSnapBreak(_currentlySnappedTo);
            
            _searchForSnapActive = false;
            
            // Get all objects of desired type
            IEnumerable<TSnapObject> objects = UIManager.Instance.GetAllObjectsOfType<TSnapObject>();
            
            float minDistance = float.MaxValue;
            TSnapObject closestObject = null;
            
            // Loop through all objects to get closest object
            foreach (TSnapObject uiObject in objects)
            {
                // Check if object can be snapped to
                if(!uiObject.IsPossibleToSnap(this)) continue;
                
                // Get distance to object
                float distance = uiObject.GetDistanceTo(position);

                // If distance is greater than minimum distance, continue
                if (distance >= minDistance) continue;
                
                // Set new minimum distance and closest object
                minDistance = distance;
                closestObject = uiObject;
            }
            
            // If no object was found, snap back to original position
            // and return
            if (!closestObject)
            {
                transform.position = _originalPosition;
                
                // Execute snap event
                OnSnap(_currentlySnappedTo);
                return;
            }
            
            // Snap to closest object
            transform.position = closestObject.RectTransform.position;
            _currentlySnappedTo = closestObject;
            
            // Execute snap event
            OnSnap(_currentlySnappedTo);
        }

        /// <summary>
        /// Executes snap event.
        /// </summary>
        /// <param name="toObject">Object to snap to.</param>
        private void OnSnap([CanBeNull] TSnapObject toObject)
        {
            if (!toObject) return;
            
            // We can't cache this as components may be added or removed at runtime
            ISnapCallback<TSnapObject>[] componentLookupTable = GetComponents<ISnapCallback<TSnapObject>>();
            
            // Loop through all components and call OnSnap
            foreach (ISnapCallback<TSnapObject> snapCallback in componentLookupTable)
                snapCallback.OnSnap(toObject);
            
            // Look-up all objects that implement IOnObjectSnappedCallback
            IOnObjectSnappedCallback<TSnapObject>[] onObjectSnappedCallbacks = toObject.GetComponents<IOnObjectSnappedCallback<TSnapObject>>();
            
            // Loop through all components and call OnSnap
            foreach (IOnObjectSnappedCallback<TSnapObject> onObjectSnappedCallback in onObjectSnappedCallbacks)
                onObjectSnappedCallback.OnSnap(this);
        }
        
        /// <summary>
        /// Executes snap break event.
        /// </summary>
        /// <param name="fromObject">Object to break snap from.</param>
        private void OnSnapBreak([CanBeNull] TSnapObject fromObject)
        {
            if(!fromObject) return;
            
            // We can't cache this as components may be added or removed at runtime
            ISnapCallback<TSnapObject>[] componentLookupTable = GetComponents<ISnapCallback<TSnapObject>>();
            
            // Loop through all components and call OnSnapBreak
            foreach (ISnapCallback<TSnapObject> snapCallback in componentLookupTable)
                snapCallback.OnBreakSnap(fromObject);
            
            // Look-up all objects that implement IOnObjectSnappedCallback
            IOnObjectSnappedCallback<TSnapObject>[] onObjectSnappedCallbacks = fromObject.GetComponents<IOnObjectSnappedCallback<TSnapObject>>();
            
            // Loop through all components and call OnSnapBreak
            foreach (IOnObjectSnappedCallback<TSnapObject> onObjectSnappedCallback in onObjectSnappedCallbacks)
                onObjectSnappedCallback.OnSnapBreak(this);
        }
    }
}