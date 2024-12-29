using System;
using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.Unity;
using UnityEngine;

namespace FastUnityCreationKit.UI.Context
{
    /// <summary>
    /// Represents a data context provider.
    /// </summary>
    public abstract class DataContextProviderBase<TContextType> : MonoBehaviour, IDataContextProvider<TContextType>
    {
        /// <summary>
        /// Represents the dirty state of the data context.
        /// </summary>
        public virtual bool IsDirty { get; set; }
        
        /// <summary>
        /// Provides the data context.
        /// </summary>
        public abstract TContextType Provide();

        protected virtual void Awake()
        {
            if(this is IInitializable initializable)
                initializable.Initialize();
        }
    }
}