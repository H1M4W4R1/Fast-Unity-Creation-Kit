using System;
using System.Collections.Generic;
using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Elements.Core;
using FastUnityCreationKit.Unity.Interfaces;
using FastUnityCreationKit.Unity.Structure.Managers;
using FastUnityCreationKit.Utility.Logging;
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
        [SerializeField]
        [Tooltip("Starting order for window canvas.")]
        [TabGroup("Configuration")]
        public int startOrder = 1000;

        /// <summary>
        /// List of window stacks.
        /// </summary>
        private List<WindowStack> _windowStacks = new List<WindowStack>();

        /// <summary>
        /// List of object tables.
        /// </summary>
        private List<UserInterfaceObjectTable> _objectTables = new List<UserInterfaceObjectTable>();

        /// <summary>
        /// Search for window prefab in database.
        /// </summary>
        /// <typeparam name="TWindowType">Type of window to search for.</typeparam>
        /// <returns>Window prefab or null if not found.</returns>
        [CanBeNull] public TWindowType FindWindowPrefab<TWindowType>()
            where TWindowType : UIWindow
        {
            UIWindowsDatabase database = UIWindowsDatabase.Instance;
            for (int i = 0; i < database.All.Count; i++)
            {
                UIWindow window = database.All[i];
                if (window is TWindowType result)
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
        [CanBeNull] public TWindowType OpenWindow<TWindowType>(WindowStack inStack = null, bool autoTransferToTop = true)
            where TWindowType : UIWindow
        {
            TWindowType window = FindWindowPrefab<TWindowType>();
            if (window == null)
            {
                Guard<UserInterfaceLogConfig>.Fatal(
                    $"Window of type {typeof(TWindowType).Name} not found in database.");
                return null;
            }

            if (inStack == null)
            {
                // Create new stack
                inStack = new WindowStack();
                _windowStacks.Add(inStack);
                Guard<UserInterfaceLogConfig>.Info($"Created new window stack for window {window.name}.");
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
                Guard<UserInterfaceLogConfig>.Info($"Moved window {instance.name} and it's stack to top.");
            }

            Guard<UserInterfaceLogConfig>.Info($"Opened window of type {typeof(TWindowType).Name}.");

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
        [CanBeNull] public TWindowType FindFirstOpenWindow<TWindowType>()
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
        [NotNull] public List<TWindowType> FindAllOpenWindows<TWindowType>()
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

            Guard<UserInterfaceLogConfig>.Info($"Closed all windows of type {typeof(TWindowType).Name}.");

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

            Guard<UserInterfaceLogConfig>.Info("Sorted windows.");
        }

        /// <summary>
        /// List of all available UI objects.
        /// </summary>
        /// <typeparam name="TUserInterfaceObjectType">Type of object to get.</typeparam>
        /// <returns>Enumerator of all objects of type TUserInterfaceObjectType.</returns>
        public IEnumerable<TUserInterfaceObjectType> GetAllObjectsOfType<TUserInterfaceObjectType>()
            where TUserInterfaceObjectType : UIObject
        {
            UserInterfaceObjectTable<TUserInterfaceObjectType> table = GetTableFor<TUserInterfaceObjectType>();
            for (int i = 0; i < table.Count; i++)
                yield return table[i];
        }
        
        /// <summary>
        /// Register user interface object.
        /// </summary>
        /// <param name="obj">Object to register.</param>
        internal void RegisterUserInterfaceObject([NotNull] UIObject obj) =>
            GetTableFor(obj).Add(obj);
        
        /// <summary>
        /// Unregister user interface object.
        /// </summary>
        /// <param name="obj">Object to unregister.</param>
        internal void UnregisterUserInterfaceObject([NotNull] UIObject obj) =>
            GetTableFor(obj).Remove(obj);

        /// <summary>
        /// Get table for object.
        /// </summary>
        /// <typeparam name="TUserInterfaceObject">Type of object.</typeparam>
        /// <returns>Table for object.</returns>
        [NotNull] internal UserInterfaceObjectTable<TUserInterfaceObject> GetTableFor<TUserInterfaceObject>()
            where TUserInterfaceObject : UIObject =>
            (UserInterfaceObjectTable<TUserInterfaceObject>) GetTableFor(typeof(TUserInterfaceObject));
        
        /// <summary>
        /// Get table for object.
        /// </summary>
        /// <param name="obj">Object to get table for.</param>
        /// <returns>Table for object.</returns>
        [NotNull] internal UserInterfaceObjectTable GetTableFor([NotNull] UIObject obj) =>
            GetTableFor(obj.GetType());

        /// <summary>
        /// Get table for object.
        /// </summary>
        /// <param name="type">Type of object.</param>
        /// <returns>Table for object.</returns>
        [NotNull] internal UserInterfaceObjectTable GetTableFor([NotNull] Type type)
        {
            // Find table for object
            UserInterfaceObjectTable table = null;
            for(int i = 0; i < _objectTables.Count; i++)
            {
                UserInterfaceObjectTable currentTable = _objectTables[i];
                if (currentTable.TableType != type) continue;
                table = currentTable;
                break;
            }
            
            // Create new table if not found
            if (table == null)
            {
                table = (UserInterfaceObjectTable) Activator.CreateInstance(
                    typeof(UserInterfaceObjectTable<>).MakeGenericType(type));
                _objectTables.Add(table);
            }
            
            // Return table
            return table;
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