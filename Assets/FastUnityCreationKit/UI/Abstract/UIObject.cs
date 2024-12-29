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
            if (this is IRenderable renderable)
                renderable.TryRender();
        }

        public void OnObjectCreated()
        {
            // Setup object
            Setup();

            // Check if this object is IRenderable, if so, try to render
            if (this is IRenderable renderable)
                renderable.TryRender(true);
        }

        public virtual void Setup()
        {
        }

        public virtual void Teardown()
        {
        }

        void IInitializable.OnInitialize()
        {
            // TODO: Initialize if supports setup for current object
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

            // If provider is not found, log an error and return null
            if (provider == null)
            {
                Debug.LogError($"Data context provider of type {typeof(TDataContext)} is not found on the object.",
                    this);
                return default;
            }

            // Provide the data context
            return new DataContextInfo<TDataContext>(provider, provider.Provide());
        }

        public void OnObjectDestroyed()
        {
            // Teardown object
            Teardown();
        }
    }
}