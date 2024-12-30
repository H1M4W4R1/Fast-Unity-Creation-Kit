using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.UI.Context;
using FastUnityCreationKit.UI.Context.Providers;
using FastUnityCreationKit.UI.Interfaces;
using FastUnityCreationKit.UI.Utility;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Callbacks;
using FastUnityCreationKit.Utility.Logging;
using UnityEngine;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// The base class for all UI objects in Fast Unity Creation Kit.
    /// </summary>
    public abstract class UIObject : FastMonoBehaviour, IUpdateCallback, ICreateCallback, IDestroyCallback
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
        /// Gets the data context of the specified type.
        /// </summary>
        /// <typeparam name="TDataContext">The type of the data context.</typeparam>
        /// <returns>The data context of the specified type.</returns>
        public virtual DataContextInfo<TDataContext> GetDataContext<TDataContext>()
        {
            // Try to get provider on this object or parent if not found
            IDataContextProvider<TDataContext> provider = GetComponent<IDataContextProvider<TDataContext>>() ??
                                                          GetComponentInParent<IDataContextProvider<TDataContext>>(
                                                              true);

            // Check if provider is not null
            if (provider != null) return new DataContextInfo<TDataContext>(provider, provider.Provide());
            
            // if not found, log error
            Guard<UserInterfaceLogConfig>.Error($"Data context provider not found on {name} or its parent.");
            return default;
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