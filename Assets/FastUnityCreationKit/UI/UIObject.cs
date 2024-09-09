using FastUnityCreationKit.Core.Utility.Initialization;
using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Events;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.UI
{
    /// <summary>
    /// Represents a UI object.
    /// This is usually a "side-wrapper" for the actual Unity UI object which
    /// implements User Interface logic.
    /// </summary>
    /// <remarks>
    /// It is forbidden to use Unity's Awake and OnDestroy methods in the derived classes
    /// as they are used by the initialization system. <br/><br/>
    /// Usage of these methods will result in an unexpected behavior as the initialization
    /// won't be performed correctly and thus uninitialized object will be used.
    /// </remarks>
    public abstract class UIObject : MonoBehaviour, IInitializable
    {
        /// <inheritdoc/>
        bool IInitializable.InternalInitializationStatusStorage { get; set; }
        
        /// <summary>
        /// Get the data context of the specified type.
        /// If the object does not have a data context of the specified type, returns null.
        /// </summary>
        [CanBeNull] protected TDataContext GetDataContext<TDataContext>() 
            where TDataContext : class, IDataContext, new()
        {
            if (this is IUIObjectWithDataContext<TDataContext> uiObjectWithDataContext)
                return uiObjectWithDataContext.DataContext;
            
            Debug.LogError($"The object {name} does not have a data context of type {typeof(TDataContext).Name}.");
            return null;
        }
        
        /// <summary>
        /// Tries to get the data context of the specified type.
        /// Returns true if the data context is found, false otherwise.
        /// </summary>
        protected bool TryGetDataContext<TDataContext>([CanBeNull] out TDataContext dataContext) 
            where TDataContext : class, IDataContext, new()
        {
            dataContext = GetDataContext<TDataContext>();
            return dataContext != null;
        }

        /// <summary>
        /// Ensures that the object is initialized.
        /// </summary>
        private void Awake() => this.Initialize();

        private void OnDestroy()
        {
            // Tear down the object
            if (this is IUIObjectWithSetup uiObjectWithSetup)
                uiObjectWithSetup.TearDown();

            // Unregister from the refresh event channel
            if (this is IRefreshable refreshable)
                RefreshUIEventChannel.UnregisterEventListener(refreshable.RefreshAsync);
            
            // Unregister from the data context events
            if (this is IUIObjectWithDataContext uiObjectWithData)
                uiObjectWithData.DetachDataContextEvents();
        }

        void IInitializable.OnInitialize()
        {
            // Set up the object
            if (this is IUIObjectWithSetup uiObjectWithSetup)
                uiObjectWithSetup.Setup();
            
            // Try to bind the data context
            if (this is IUIObjectWithDataContext uiObjectWithData)
            {
                uiObjectWithData.TryAutomaticContextBinding();
                uiObjectWithData.AttachDataContextEvents();
            }

            // If the object is refreshable, install event bindings
            if (this is IRefreshable refreshable)
                RefreshUIEventChannel.RegisterEventListener(refreshable.RefreshAsync);
            
            // If the object is renderable, render it
            if (this is IRenderable renderable) renderable.RenderAsync();
        }
    }
}