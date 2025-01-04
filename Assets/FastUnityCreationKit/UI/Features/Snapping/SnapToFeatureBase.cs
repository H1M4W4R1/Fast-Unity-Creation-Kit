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
    public abstract class SnapToFeatureBase<TSnapObject> : UIObjectFeatureBase, 
        IPointerUpHandler, IPointerDownHandler
        where TSnapObject : UIObjectBase, ISnapTarget<TSnapObject>
    {
        /// <summary>
        /// Object that is currently snapped to.
        /// </summary>
        [CanBeNull] [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)]
        private TSnapObject _currentlySnappedTo;
        
        /// <summary>
        /// Original position of the object.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)]
        private Vector2 _originalPosition;
        
        /// <summary>
        /// If true next pointer up event will execute snap.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)]
        private bool _searchForSnapActive;

        /// <summary>
        /// If true mouse position will be used to calculate snap position instead
        /// of transform position. It is recommended to use mouse position for snapping
        /// for better performance and user experience.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)]
        protected virtual bool UseMousePosition => true;
        
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)]
        protected virtual bool StartSnapped => false;

        public override void Setup()
        {
            base.Setup();
            _originalPosition = transform.position;
        }

        private void Start()
        {
            // Snap to the closest object after everything is initialized
            if(StartSnapped)
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

        /// <summary>
        /// High-level access to snapping to a specific position.
        /// Warning: this may be unstable, so use with caution.
        /// </summary>
        /// <param name="position">Position from which you would like to search for TSnapObject (targets)
        /// this object can snap to.</param>
         public void SnapToObjectClosestTo(Vector2 position)=>
            ExecuteSnap(position);
        
        internal void ExecuteSnap(Vector2 position)
        {
            NotifyObjectSnapBroken(_currentlySnappedTo);
            
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
                NotifyObjectSnapped(_currentlySnappedTo);
                return;
            }
            
            // Snap to closest object
            transform.position = closestObject.RectTransform.position;
            _currentlySnappedTo = closestObject;
            
            // Execute snap event
            NotifyObjectSnapped(_currentlySnappedTo);
        }

        /// <summary>
        /// Executes snap event.
        /// </summary>
        /// <param name="toObject">Object to snap to.</param>
        private void NotifyObjectSnapped([CanBeNull] TSnapObject toObject)
        {
            if (!toObject) return;
            
            // Send messages to all components that implement ISnapCallback
            BroadcastMessage(nameof(ISnapCallback<TSnapObject>.OnSnap), toObject, SendMessageOptions.DontRequireReceiver);
            
            // And notify target slot that it was snapped
            toObject.SendMessage(nameof(IOnObjectSnappedCallback<TSnapObject>.OnSnap), this, SendMessageOptions.DontRequireReceiver);
        }
        
        /// <summary>
        /// Executes snap break event.
        /// </summary>
        /// <param name="fromObject">Object to break snap from.</param>
        private void NotifyObjectSnapBroken([CanBeNull] TSnapObject fromObject)
        {
            if(!fromObject) return;
            
            // Send messages to all components that implement ISnapCallback
            BroadcastMessage(nameof(ISnapCallback<TSnapObject>.OnSnapBreak), fromObject, SendMessageOptions.DontRequireReceiver);
            
            // And notify target slot that it was snapped
            fromObject.SendMessage(nameof(IOnObjectSnappedCallback<TSnapObject>.OnSnapBreak), this, SendMessageOptions.DontRequireReceiver);
        }
    }
}