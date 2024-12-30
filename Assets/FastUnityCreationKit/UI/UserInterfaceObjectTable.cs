using System;
using System.Collections.Generic;
using FastUnityCreationKit.UI.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI
{
    /// <summary>
    /// Represents a table of UI objects.
    /// </summary>
    /// <typeparam name="TUserInterfaceObject">Type of UI object.</typeparam>
    internal sealed class UserInterfaceObjectTable<TUserInterfaceObject> : UserInterfaceObjectTable
        where TUserInterfaceObject : UIObject
    {
        /// <summary>
        /// Creates a new table of UI objects.
        /// </summary>
        public UserInterfaceObjectTable() : base(typeof(TUserInterfaceObject))
        {
        }

        public TUserInterfaceObject this[int index]
        {
            get
            {
                if (index < 0 || index >= objects.Count)
                    return null;
                
                return (TUserInterfaceObject) objects[index];
            }

            set
            {
                if (index < 0 || index >= objects.Count)
                    return;
                
                objects[index] = value;
            }
        }
    }
    
    /// <summary>
    /// Represents a table of UI objects.
    /// </summary>
    internal abstract class UserInterfaceObjectTable 
    {
        protected readonly Type mainTableType;
        protected readonly List<UIObject> objects = new List<UIObject>();
        
        /// <summary>
        /// Type of table.
        /// </summary>
        public Type TableType => mainTableType;

        protected UserInterfaceObjectTable(Type tableType)
        {
            mainTableType = tableType;
        }

        public int Count => objects.Count;
        
        public void Add(UIObject uiObject)
        {
            #if UNITY_EDITOR
            if(uiObject.GetType() != mainTableType)
                throw new ArgumentException("UI object type does not match table type.");
            #endif
            
            // Add object if not yet added
            if(!objects.Contains(uiObject))
                objects.Add(uiObject);
        }
        
        public void Remove(UIObject uiObject)
        {
            objects.Remove(uiObject);
        }
    }
}