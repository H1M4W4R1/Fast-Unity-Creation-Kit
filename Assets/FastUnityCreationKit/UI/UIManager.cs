using System.Collections.Generic;
using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Elements.Core;
using FastUnityCreationKit.Unity.Interfaces;
using FastUnityCreationKit.Unity.Structure.Managers;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI
{
    /// <summary>
    /// UI manager - used to manage all UI elements.
    /// </summary>
    public sealed class UIManager : FastManager<UIManager>, IPersistent
    {
        [SerializeField] [Tooltip("Starting order for window canvas.")]
        [TabGroup("Configuration")]
        public int startOrder = 1000;
        
        /// <summary>
        /// List of window stacks.
        /// </summary>
        private List<WindowStack> _windowStacks = new List<WindowStack>();
        
        /// <summary>
        /// Search for window prefab in database.
        /// </summary>
        /// <typeparam name="TWindowType">Type of window to search for.</typeparam>
        /// <returns>Window prefab or null if not found.</returns>
        [CanBeNull] public TWindowType FindWindowPrefab<TWindowType>()
            where TWindowType : UIWindow
        {
            UIWindowsDatabase database = UIWindowsDatabase.Instance;
            for(int i = 0; i < database.All.Count; i++)
            {
                UIWindow window = database.All[i];
                if(window is TWindowType result)
                    return result;
            }
            
            return null;
        }

        /// <summary>
        /// Open window of type TWindowType.
        /// </summary>
        /// <param name="inStack">Window stack to open window in.</param>
        /// <param name="autoTransferToTop">Should the stack be transferred to top.</param>
        /// <typeparam name="TWindowType">Type of window to open.</typeparam>
        /// <returns>Opened window or null if not found.</returns>
        public TWindowType OpenWindow<TWindowType>(WindowStack inStack = null, bool autoTransferToTop = true)
            where TWindowType : UIWindow
        {
            TWindowType window = FindWindowPrefab<TWindowType>();
            if (window == null)
            {
                Debug.LogError($"Window of type {typeof(TWindowType)} not found in database.", this);
                return null;
            }
            
            if(inStack == null)
            {
                // Create new stack
                inStack = new WindowStack();
                _windowStacks.Add(inStack);
            }
            
            // Create window instance and add to desired stack.
            // Place all windows in the same parent - UIManager.
            TWindowType instance = Instantiate(window, transform);
            inStack.AddWindow(instance);

            // Set order of stack to be last if auto transfer is enabled
            if (autoTransferToTop)
            {
                _windowStacks.Remove(inStack);
                _windowStacks.Add(inStack);
            }
            
            // Set order of windows to be nicely displayed
            SortWindows();
            
            // Return instance
            return instance;
        }
        
        /// <summary>
        /// Search for first open window of type TWindowType.
        /// </summary>
        /// <typeparam name="TWindowType"></typeparam>
        /// <returns></returns>
        public TWindowType FindFirstOpenWindow<TWindowType>()
            where TWindowType : UIWindow
        {
            for (int i = 0; i < _windowStacks.Count; i++)
            {
                WindowStack stack = _windowStacks[i];
                for (int j = 0; j < stack.Windows.Count; j++)
                {
                    UIWindow window = stack.Windows[j];
                    if (window is TWindowType result)
                        return result;
                }
            }

            return null;
        }
        
        /// <summary>
        /// Check if window of type TWindowType is open.
        /// </summary>
        /// <typeparam name="TWindowType">Type of window to search for.</typeparam>
        /// <returns>True if window is open, false otherwise.</returns>
        public bool IsAnyWindowOpen<TWindowType>()
            where TWindowType : UIWindow => !ReferenceEquals(FindFirstOpenWindow<TWindowType>(), null);
        
        /// <summary>
        /// Find all open windows of type TWindowType.
        /// </summary>
        /// <typeparam name="TWindowType">Window type to search for.</typeparam>
        /// <returns>List of windows of type TWindowType.</returns>
        public List<TWindowType> FindAllOpenWindows<TWindowType>()
            where TWindowType : UIWindow
        {
            List<TWindowType> result = new List<TWindowType>();
            for (int i = 0; i < _windowStacks.Count; i++)
            {
                WindowStack stack = _windowStacks[i];
                for (int j = 0; j < stack.Windows.Count; j++)
                {
                    UIWindow window = stack.Windows[j];
                    if (window is TWindowType windowType)
                        result.Add(windowType);
                }
            }
            
            return result;
        }
        
        /// <summary>
        /// Close all windows of type TWindowType.
        /// </summary>
        /// <typeparam name="TWindowType">Type of window to close.</typeparam>
        public void CloseAllWindowsOf<TWindowType>()
            where TWindowType : UIWindow
        {
            for (int i = 0; i < _windowStacks.Count; i++)
            {
                WindowStack stack = _windowStacks[i];
                for (int j = 0; j < stack.Windows.Count; j++)
                {
                    UIWindow window = stack.Windows[j];
                    window.Close(false);
                }
            }
            
            // Sort windows after closing
            SortWindows();
        }
        
        /// <summary>
        /// Close all windows.
        /// </summary>
        public void CloseAllWindows() => CloseAllWindowsOf<UIWindow>();

        /// <summary>
        /// Sort windows in the manager.
        /// </summary>
        public void SortWindows()
        {
            int currentOrder = startOrder;
            
            // Sort all window stacks
            for (int i = 0; i < _windowStacks.Count; i++)
            {
                WindowStack stack = _windowStacks[i];
                stack.Sort(currentOrder);
                
                // We need to increment the order by the number of windows in the stack
                currentOrder += stack.Windows.Count;
            }
        }

        /// <summary>
        /// Remove window stack from manager.
        /// </summary>
        internal void TryRemoveEmptyWindowStack([NotNull] WindowStack windowStack)
        {
            if (windowStack.IsEmpty)
                _windowStacks.Remove(windowStack);
        }
    }
}