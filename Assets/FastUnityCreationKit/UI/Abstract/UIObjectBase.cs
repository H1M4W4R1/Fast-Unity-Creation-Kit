using FastUnityCreationKit.UI.Context.Providers.Base;
using FastUnityCreationKit.UI.Context.Providers.Utility;
using FastUnityCreationKit.UI.Interfaces;
using FastUnityCreationKit.UI.Utility;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Callbacks;
using FastUnityCreationKit.Utility.Logging;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// The base class for all UI objects in Fast Unity Creation Kit.
    /// </summary>
    public abstract class UIObjectBase : FastMonoBehaviour, IUpdateCallback, ICreateCallback, IDestroyCallback
    {
        private RectTransform _rectTransform;
        public RectTransform RectTransform => _rectTransform;
        
        public void OnObjectUpdated(float deltaTime)
        {
            // Check if this object is IRenderable, if so, try to render
            if (this is IRenderable renderable)
                renderable.TryRender();
        }

        public void OnObjectCreated()
        {
            // Get rect transform
            _rectTransform = GetComponent<RectTransform>();
         
            // Register this object
            UIManager.Instance.RegisterUserInterfaceObject(this);
            
            // Setup object
            Setup();
            Guard<UserInterfaceLogConfig>.Verbose($"UI object {name} has been set-up correctly.");

            // Check if this object is IRenderable, if so, try to render
            if (this is IRenderable renderable)
            {
                renderable.TryRender(true);
                Guard<UserInterfaceLogConfig>.Verbose($"UI object {name} has been rendered correctly for the first time.");
            }

            // Call after first render
            AfterFirstRenderOrCreated();
        }

        public virtual void Setup()
        {
        }
        
        public virtual void AfterFirstRenderOrCreated()
        {
        }

        public virtual void Teardown()
        {
        }

        /// <summary>
        /// Gets the data context provider of the specified type.
        /// </summary>
        /// <typeparam name="TProviderType">The type of the data context provider.</typeparam>
        /// <returns>The data context provider of the specified type or null if not found.</returns>
        public TProviderType GetProviderByType<TProviderType>() 
            where TProviderType : IDataContextProvider
        {
            // Try to get provider on this object or parent if not found
            TProviderType provider = GetComponent<TProviderType>() ??
                                     GetComponentInParent<TProviderType>(true);
            
            // Check if provider is not null
            if (provider != null) return provider;
            
            // if not found, log error
            Guard<UserInterfaceLogConfig>.Error($"Data context provider of type {typeof(TProviderType).Name} not found on {name} or its parent.");
            return default;
        }
        
        /// <summary>
        /// Attempts to get the data context provider of the specified type.
        /// This method exists to access provider without the need to actually provide the data context.
        /// </summary>
        /// <typeparam name="TDataContext">The type of the data context.</typeparam>
        /// <returns>The data context provider of the specified type or null if not found.</returns>
        [CanBeNull] public IDataContextProvider<TDataContext> GetProviderFor<TDataContext>()
            => GetProviderByType<IDataContextProvider<TDataContext>>();
        
        /// <summary>
        /// Gets the data context of the specified type.
        /// This method can be overriden for very stupid use cases that may not require
        /// provider to be used.
        /// </summary>
        /// <typeparam name="TDataContext">The type of the data context.</typeparam>
        /// <returns>The data context of the specified type.</returns>
        public virtual DataContextInfo<TDataContext> GetDataContext<TDataContext>()
        {
            IDataContextProvider<TDataContext> provider = GetProviderFor<TDataContext>();

            // Check if provider is not null
            return provider != null ? new DataContextInfo<TDataContext>(provider, provider.Provide()) : default;
        }

        public void OnObjectDestroyed()
        {
            // Teardown object
            Teardown();
            
            // Unregister this object
            UIManager.Instance.UnregisterUserInterfaceObject(this);
        }
    }
}