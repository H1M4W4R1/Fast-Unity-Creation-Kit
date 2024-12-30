using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Logging;
using Unity.Mathematics;
using UnityEngine;

namespace FastUnityCreationKit.UI.Extensions
{
    public static class ElementBoundsExtensions
    {
        /// <summary>
        /// Checks if specified UI object is within the bounds of its parent.
        /// </summary>
        public static bool IsWithinParentBounds(this UIObject uiObject)
        {
            // Get rect transform
            RectTransform rectTransform = uiObject.GetComponent<RectTransform>();
            return rectTransform.IsWithinParentBounds();
        }

        /// <summary>
        /// Checks if specified UI object is within the bounds of its parent.
        /// </summary>
        public static bool IsWithinParentBounds(this RectTransform rectTransform)
        {
            // Get parent
            Transform parent = rectTransform.parent;

            return IsWithinRectTransformBounds(rectTransform, parent.GetComponent<RectTransform>());
        }

        /// <summary>
        /// Checks if the specified UI object is within the bounds of specified rect transform.
        /// </summary>
        public static bool IsWithinRectTransformBounds(this RectTransform rectTransform,
            RectTransform otherRectTransform)
        {
            Rect targetRect = rectTransform.rect;
            Rect otherRect = otherRectTransform.rect;

            // Get left bottom corner of the element
            Vector2 targetPosition = rectTransform.position;
            targetPosition -= targetRect.size / 2;

            // Get left bottom corner of the parent
            Vector2 parentPosition = otherRectTransform.position;
            parentPosition -= otherRect.size / 2;
            parentPosition -= targetRect.size / 2;

            // Check if the element is within the bounds of the parent,
            // if it is not, then return false.
            if (targetPosition.x < parentPosition.x) return false;
            if (targetPosition.y < parentPosition.y) return false;
            if (targetPosition.x + targetRect.width > parentPosition.x + otherRect.width) return false;
            if (targetPosition.y + targetRect.height > parentPosition.y + otherRect.height) return false;

            return true;
        }

        /// <summary>
        /// Fits the element into the bounds of the parent.
        /// </summary>
        public static void FitIntoParent(this UIObject uiObject)
        {
            // Get rect transform
            RectTransform rectTransform = uiObject.GetComponent<RectTransform>();
            rectTransform.FitIntoParent();
        }

        /// <summary>
        /// Fits the element into the bounds of the parent.
        /// </summary>
        public static void FitIntoParent(this RectTransform rectTransform)
        {
            // Get parent
            Transform parent = rectTransform.parent;

            // Fit into parent
            rectTransform.FitIntoRectTransform(parent.GetComponent<RectTransform>());
        }
        
        /// <summary>
        /// This method fits the element into the bounds of the specified rect transform.
        /// </summary>
        public static void FitIntoRectTransform(this RectTransform rectTransform,
            RectTransform otherRectTransform)
        {
            // Get rect
            Rect targetRect = rectTransform.rect;

            // Get other rect
            Rect otherRect = otherRectTransform.rect;

            // Check if rect is smaller or same size as viewport rect
            // if not, then return
            if (targetRect.width > otherRect.width ||
                targetRect.height > otherRect.height)
            {
                // Log a warning
                Guard<EditorAutomationLogConfig>.Warning("Draggable element is larger than parent.");
                return;
            }

            Vector2 pivotOffset = (float2) rectTransform.pivot * (float2)targetRect.size;
            
            // Get left bottom corner of the element
            Vector2 targetPosition = rectTransform.position;
            targetPosition -= pivotOffset;

            // Get left bottom corner of the parent
            Vector2 parentPosition = rectTransform.parent.position;
            parentPosition -= otherRect.size / 2;

            // If absolute position is lower than zero (outside the bounds of the parent)
            if (targetPosition.x < parentPosition.x) targetPosition.x = parentPosition.x;
            if (targetPosition.y < parentPosition.y) targetPosition.y = parentPosition.y;

            // If absolute position is further than the parent width (outside the bounds of the parent)
            if (targetPosition.x + targetRect.width > parentPosition.x + otherRect.width)
                targetPosition.x = parentPosition.x + otherRect.width - targetRect.width;

            // If absolute position is higher than the parent height (outside the bounds of the parent)
            if (targetPosition.y + targetRect.height > parentPosition.y + otherRect.height)
                targetPosition.y = parentPosition.y + otherRect.height - targetRect.height;

            // Set absolute position
            rectTransform.position = targetPosition + pivotOffset;
        }

        /// <summary>
        /// Fits the element into the bounds of the viewport.
        /// </summary>
        public static void FitIntoViewport(this UIObject uiObjectBase)
        {
            // Get rect transform
            RectTransform rectTransform = uiObjectBase.GetComponent<RectTransform>();
            rectTransform.FitIntoViewport();
        }

        /// <summary>
        /// Fits the element into the bounds of the viewport.
        /// </summary>
        public static void FitIntoViewport(this RectTransform rectTransform)
        {
            // Get rect
            Rect targetRect = rectTransform.rect;

            // Check if rect is smaller or same size as viewport rect
            // if not, then return
            if (targetRect.width > Screen.width ||
                targetRect.height > Screen.height)
            {
                // Log a warning
                Guard<EditorAutomationLogConfig>.Warning("Draggable element is larger than viewport.");
                return;
            }

            Vector2 pivotOffset = (float2) rectTransform.pivot * (float2)targetRect.size;
            
            // Get left bottom corner of the element
            Vector2 targetPosition = rectTransform.position;
            targetPosition -= pivotOffset;

            // Check if the element is within the bounds of the viewport,
            // if it is not, then move it to the nearest point within the bounds.

            // If absolute position is lower than zero (outside the bounds of the viewport)
            if (targetPosition.x < 0) targetPosition.x = 0;
            if (targetPosition.y < 0) targetPosition.y = 0;

            // If absolute position is higher than the viewport width (outside the bounds of the viewport)
            if (targetPosition.x + targetRect.width > Screen.width)
                targetPosition.x = Screen.width - targetRect.width;

            // If absolute position is higher than the viewport height (outside the bounds of the viewport)
            if (targetPosition.y + targetRect.height > Screen.height)
                targetPosition.y = Screen.height - targetRect.height;

            // Set absolute position
            rectTransform.position = targetPosition + pivotOffset;
        }

        /// <summary>
        /// Checks if the specified UI object is within the bounds of the viewport.
        /// </summary>
        public static bool IsWithinViewportBounds(this UIObject uiObjectBase)
        {
            // Get the rect transform
            RectTransform rectTransform = uiObjectBase.RectTransform;
            return rectTransform.IsWithinViewportBounds();
        }

        public static bool IsWithinViewportBounds(this RectTransform rectTransform)
        {
            // Get the target rect and position
            Rect targetRect = rectTransform.rect;
            Vector2 targetPosition = rectTransform.position - new Vector3(targetRect.width / 2, targetRect.height / 2);

            // Check if the draggable element is within the bounds of the viewport
            return targetPosition is {x: >= 0, y: >= 0} &&
                   targetPosition.x + targetRect.width <= Screen.width &&
                   targetPosition.y + targetRect.height <= Screen.height;
        }
    }
}