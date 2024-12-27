using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.UI.Context;
using FastUnityCreationKit.UI.Interfaces;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Events.Interfaces;
using UnityEngine;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// The base class for all UI objects in Fast Unity Creation Kit.
    /// </summary>
    public abstract class UIObject<TSelf> : FastMonoBehaviour<TSelf>, IUpdateCallback, IInitializable, ICreateCallback,
        IDestroyCallback 
        where TSelf : UIObject<TSelf>, new()
    {
        bool IInitializable.InternalInitializationStatusStorage { get; set; }

        public void OnObjectUpdated(float deltaTime)
        {
            // Check if this object is IRenderable, if so, try to render
            if(this is IRenderable renderable)
                renderable.TryRender();
        }
        
        public void OnObjectCreated()
        {
            // Setup object
            Setup();
            
            // Check if this object is IRenderable, if so, try to render
            if(this is IRenderable renderable)
                renderable.TryRender(true);
        }

        public virtual void Setup(){}

        public virtual void Teardown(){}
        
        void IInitializable.OnInitialize()
        {
           // TODO: Initialize if supports setup for current object
        }
        
        /// <summary>
        /// Gets the data context of the specified type.
        /// </summary>
        /// <typeparam name="TDataContext">The type of the data context.</typeparam>
        /// <returns>The data context of the specified type.</returns>
        public virtual TDataContext GetDataContext<TDataContext>() 
            where TDataContext : DataContext<TDataContext>, new()
        {
            // Get first found data context
            TDataContext dataContext = GetComponent<TDataContext>();

            // Check if data context is found
            if (dataContext is null)
            {
                Debug.LogError($"DataContext of type {typeof(TDataContext)} not found on {name}.", this);
                return default;
            }
            
            // Check if Data Context
            // is reference
            if (dataContext is ReferencedDataContext<TDataContext> referenceContext)
            {
                // Check if reference is set
                if (referenceContext.Reference == null)
                    Debug.LogError($"DataContext of type {typeof(TDataContext)} is a reference but no reference is set.", this);
                
                // Check if reference is of the same type
                if(referenceContext.Reference is not TDataContext reference)
                    Debug.LogError($"DataContext of type {typeof(TDataContext)} is a reference but reference is not of the same type.", this);
                else
                    return reference;
                
                // Return default (null) if reference is not set or not of the same type
                return default;
            }
            
            // Return data context
            return dataContext;
        }

        public void OnObjectDestroyed()
        {
            // Teardown object
            Teardown();
        }
    }
}