﻿using FastUnityCreationKit.Annotations.Addressables;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.Unity.Interfaces.Interaction;
using JetBrains.Annotations;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.UI.Elements.Core
{
    /// <summary>
    ///     Represents generic window. Can be for example your main menu or settings window.
    /// </summary>
    [AddressableGroup(UI_ADDRESSABLE_GROUP_TAG, UI_WINDOWS_ADDRESSABLE_TAG)]
    [RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
    public abstract class UIWindow : UIPanel, IClickable
    {
        /// <summary>
        ///     Internal reference to Unity's Canvas component.
        /// </summary>
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [NotNull] protected Canvas canvas = null!;

        /// <summary>
        ///     Internal reference to Unity's CanvasGroup component.
        /// </summary>
        // ReSharper disable once NullableWarningSuppressionIsUsed
        [NotNull] protected CanvasGroup canvasGroup = null!;

        /// <summary>
        ///     Local reference to window stack.
        /// </summary>
        [CanBeNull] protected WindowStack windowStack;

        /// <summary>
        ///     Database with all windows
        /// </summary>
        public UIWindowsDatabase Database => UIWindowsDatabase.Instance;

        public void OnClick(PointerEventData pointerData)
        {
            // Check if window stack is not null.
            if (windowStack == null) return;

            // Check if window is not already on top of stack.
            if (windowStack.Windows[^1] == this) return;

            // Move window to top of stack.
            windowStack.Windows.Remove(this);
            windowStack.Windows.Add(this);
            Guard<UserInterfaceLogConfig>.Info(
                $"Window {GetType().GetCompilableNiceFullName()} moved to top of stack.");

            // Sort windows in manager as window was clicked.
            UIManager.Instance.SortWindows();
        }

        public override void Setup()
        {
            base.Setup();
            canvas = GetComponent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        ///     Set window stack for window.
        /// </summary>
        /// <param name="stack">Window stack to set.</param>
        public void SetWindowStack([CanBeNull] WindowStack stack)
        {
            windowStack = stack;
        }

        /// <summary>
        ///     Set order of window in canvas.
        /// </summary>
        /// <param name="startOrder">Order to set.</param>
        public void SetOrder(int startOrder)
        {
            canvas.sortingOrder = startOrder;
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
            if (notifyManager) UIManager.Instance.SortWindows();

            Guard<UserInterfaceLogConfig>.Info($"Window {GetType().GetCompilableNiceFullName()} closed.");
        }

        /// <summary>
        ///     Open child window of type TWindowType in current window stack.
        /// </summary>
        /// <typeparam name="TWindowType">Type of window to open.</typeparam>
        /// <returns>Opened window or null if not found.</returns>
        [CanBeNull] public TWindowType OpenChildWindow<TWindowType>()
            where TWindowType : UIWindow
        {
            return UIManager.Instance.OpenWindow<TWindowType>(windowStack);
        }

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