using System;
using System.Collections.Generic;
using FastUnityCreationKit.UI.Abstract;

namespace FastUnityCreationKit.UI.Utility
{
    /// <summary>
    /// Represents a table of UI objects.
    /// </summary>
    /// <typeparam name="TUserInterfaceObject">Type of UI object.</typeparam>
    internal sealed class UserInterfaceObjectTable<TUserInterfaceObject> : UserInterfaceObjectTable
        where TUserInterfaceObject : UIObjectBase
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
        protected readonly List<UIObjectBase> objects = new List<UIObjectBase>();
        
        /// <summary>
        /// Type of table.
        /// </summary>
        public Type TableType => mainTableType;

        protected UserInterfaceObjectTable(Type tableType)
        {
            mainTableType = tableType;
        }

        public int Count => objects.Count;
        
        public void Add(UIObjectBase uiObjectBase)
        {
            #if UNITY_EDITOR
            if(uiObjectBase.GetType() != mainTableType)
                throw new ArgumentException("UI object type does not match table type.");
            #endif
            
            // Add object if not yet added
            if(!objects.Contains(uiObjectBase))
                objects.Add(uiObjectBase);
        }
        
        public void Remove(UIObjectBase uiObjectBase)
        {
            objects.Remove(uiObjectBase);
        }
    }
}