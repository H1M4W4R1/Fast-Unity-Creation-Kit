using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.UI.Features
{
    /// <summary>
    /// Allows object to be dragged. Combines well with snapping feature.
    /// </summary>
    public sealed class DragFeature : UIFeature, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [ShowInInspector] [ReadOnly] [TabGroup("Debug")]
        private bool _isDragging = false;

        /// <summary>
        /// If snapping is supported this should be set to false.
        /// Also should be false if object should stay in new position.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Debug")]
        public bool ReturnToOldPosition { get; } = false;
        
        /// <summary>
        /// If true object will be snapped to mouse position while being dragged.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Debug")]
        public bool SnapToMousePosition { get; } = true;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging)
                return;
            
            // Update position
            RectTransform rt = objectBaseReference.RectTransform;
            if(SnapToMousePosition)
                rt.position = eventData.position;
            else
                rt.position += (Vector3) eventData.delta;
            
            // Reposition child to be last
            rt.SetAsLastSibling();
        }
    }
}