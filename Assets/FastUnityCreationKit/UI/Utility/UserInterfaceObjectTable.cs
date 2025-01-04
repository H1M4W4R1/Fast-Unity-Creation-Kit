using System;
using System.Collections.Generic;
using FastUnityCreationKit.UI.Abstract;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace FastUnityCreationKit.UI.Utility
{
    /// <summary>
    /// Represents a table of UI objects.
    /// </summary>
    /// <typeparam name="TUserInterfaceObject">Type of UI object.</typeparam>
    [Preserve] [UsedImplicitly]
    internal sealed class UserInterfaceObjectTable<TUserInterfaceObject> : UserInterfaceObjectTable
        where TUserInterfaceObject : UIObjectBase
    {
        /// <summary>
        /// Creates a new table of UI objects.
        /// </summary>
        public UserInterfaceObjectTable() : base(typeof(TUserInterfaceObject))
        {
        }

        [CanBeNull] public TUserInterfaceObject this[int index]
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
        [NotNull] protected readonly Type mainTableType;
        [ItemNotNull] [NotNull] protected readonly List<UIObjectBase> objects = new List<UIObjectBase>();
        
        /// <summary>
        /// Type of table.
        /// </summary>
        [NotNull] public Type TableType => mainTableType;

        protected UserInterfaceObjectTable([NotNull] Type tableType)
        {
            mainTableType = tableType;
        }

        public int Count => objects.Count;
        
        public void Add([NotNull] UIObjectBase uiObjectBase)
        {
            #if UNITY_EDITOR
            if(uiObjectBase.GetType() != mainTableType)
                throw new ArgumentException("UI object type does not match table type.");
            #endif
            
            // Add object if not yet added
            if(!objects.Contains(uiObjectBase))
                objects.Add(uiObjectBase);
        }
        
        public void Remove([NotNull] UIObjectBase uiObjectBase)
        {
            objects.Remove(uiObjectBase);
        }
    }
}