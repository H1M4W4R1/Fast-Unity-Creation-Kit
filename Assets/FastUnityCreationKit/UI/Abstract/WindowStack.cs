using System.Collections.Generic;
using FastUnityCreationKit.UI.Elements.Core;
using FastUnityCreationKit.Core.Logging;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// Represents stack of windows - used to manage windows order and automatic opening/closing of windows.
    /// </summary>
    public sealed class WindowStack
    {
        /// <summary>
        /// List of windows in stack.
        /// </summary>
        internal List<UIWindow> Windows { get; } = new List<UIWindow>();

        /// <summary>
        /// Check if stack is empty.
        /// </summary>
        public bool IsEmpty => Windows.Count == 0;
        
        /// <summary>
        /// Order of first window in stack.
        /// </summary>
        private int StartOrder { get; set; }

        internal void DeleteWindow([NotNull] UIWindow window)
        {
            Windows.Remove(window);
            window.SetWindowStack(null!);
            Guard<UserInterfaceLogConfig>.Verbose($"Window {window.name} has been removed from stack.");
        }

        internal void AddWindow([NotNull] UIWindow window)
        {
            Windows.Add(window);
            window.SetWindowStack(this);
            Guard<UserInterfaceLogConfig>.Verbose($"Window {window.name} has been added to stack.");
        }
        
        /// <summary>
        /// Sort windows in stack.
        /// </summary>
        internal void Sort()
        {
            // Reorder windows based on their order in stack.
            for(int i = 0; i < Windows.Count; i++)
                Windows[i].SetOrder(StartOrder + i);
        }
        
        /// <summary>
        /// Sort windows in stack.
        /// </summary>
        internal void Sort(int startOrder)
        {
            StartOrder = startOrder;
            Sort();
        }
        
        /// <summary>
        /// Check if stack has any window of given type.
        /// </summary>
        public bool HasWindow<TWindowType>() where TWindowType : UIWindow
        {
            for(int i = 0; i < Windows.Count; i++)
            {
                if(Windows[i] is TWindowType) return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// Get window of given type from stack.
        /// </summary>
        /// <typeparam name="TWindowType">Type of window to get.</typeparam>
        /// <returns>Window of given type or null if not found.</returns>
        [CanBeNull] public TWindowType GetWindow<TWindowType>() where TWindowType : UIWindow
        {
            for(int i = 0; i < Windows.Count; i++)
            {
                if(Windows[i] is TWindowType window) return window;
            }
            
            return null;
        }
        
    }
}