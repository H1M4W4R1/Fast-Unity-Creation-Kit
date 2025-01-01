using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Utility;
using FastUnityCreationKit.Unity.Interfaces;
using FastUnityCreationKit.Utility.Attributes;
using FastUnityCreationKit.Utility.Logging;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.UI.Elements.Core
{
    /// <summary>
    /// Represents generic window. Can be for example your main menu or settings window.
    /// </summary>
    [AddressableGroup(LocalConstants.UI_ADDRESSABLE_GROUP_TAG, LocalConstants.UI_WINDOWS_ADDRESSABLE_TAG)]
    [RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
    public abstract class UIWindow : UIPanel, IClickable
    {
        /// <summary>
        /// Database with all windows
        /// </summary>
        public UIWindowsDatabase Database => UIWindowsDatabase.Instance;
        
        /// <summary>
        /// Internal reference to Unity's Canvas component.
        /// </summary>
        protected Canvas canvas;
        
        /// <summary>
        /// Internal reference to Unity's CanvasGroup component.
        /// </summary>
        protected CanvasGroup canvasGroup;
        
        /// <summary>
        /// Local reference to window stack.
        /// </summary>
        protected WindowStack windowStack;
        
        public override void Setup()
        {
            base.Setup();
            canvas = GetComponent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// Set window stack for window.
        /// </summary>
        /// <param name="stack">Window stack to set.</param>
        public void SetWindowStack([NotNull] WindowStack stack) =>
            windowStack = stack;

        /// <summary>
        /// Set order of window in canvas.
        /// </summary>
        /// <param name="startOrder">Order to set.</param>
        public void SetOrder(int startOrder) =>
            canvas.sortingOrder = startOrder;

        public void OnClick(PointerEventData pointerData)
        {
            // Check if window stack is not null.
            if(windowStack == null) return;
            
            // Check if window is not already on top of stack.
            if(windowStack.Windows[^1] == this) return;
            
            // Move window to top of stack.
            windowStack.Windows.Remove(this);
            windowStack.Windows.Add(this);
            Guard<UserInterfaceLogConfig>.Info($"Window {GetType().Name} moved to top of stack.");
            
            // Sort windows in manager as window was clicked.
            UIManager.Instance.SortWindows();
        }

        public void Close(bool notifyManager = true)
        {
            // Perform close operation.
            OnWindowClosed();

            // Try to remove window from stack and empty stacks from manager.
            if (windowStack != null)
            {
                windowStack.DeleteWindow(this);
                UIManager.Instance.TryRemoveEmptyWindowStack(windowStack);
            }

            // Destroy window game object.
            Destroy(gameObject);
            
            // Sort windows in manager as window was closed.
            if(notifyManager)
                UIManager.Instance.SortWindows();
            
            Guard<UserInterfaceLogConfig>.Info($"Window {GetType().Name} closed.");
        }

        /// <summary>
        /// Open child window of type TWindowType in current window stack.
        /// </summary>
        /// <typeparam name="TWindowType">Type of window to open.</typeparam>
        /// <returns>Opened window or null if not found.</returns>
        public TWindowType OpenChildWindow<TWindowType>()
            where TWindowType : UIWindow => UIManager.Instance.OpenWindow<TWindowType>(windowStack);

        public override void AfterFirstRenderOrCreated()
        {
            base.AfterFirstRenderOrCreated();
            OnWindowOpened();
        }

        public virtual void OnWindowOpened()
        {
            // Do nothing by default.
        }
        
        public virtual void OnWindowClosed()
        {
            // Do nothing by default.
        }
    }
}