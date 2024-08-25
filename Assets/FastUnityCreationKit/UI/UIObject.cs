using FastUnityCreationKit.Core.Initialization;
using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Events;
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
        /// <summary>
        /// True if the object has been initialized.
        /// </summary>
        public bool IsInitialized => ((IInitializable) this).InternalInitializationStatusStorage;
        
        /// <inheritdoc/>
        bool IInitializable.InternalInitializationStatusStorage { get; set; }
        
        /// <summary>
        /// Ensures that the object is initialized.
        /// </summary>
        private void Awake() => ((IInitializable) this).EnsureInitialized();

        private void OnDestroy()
        {
            // Tear down the object
            if (this is IUIObjectWithSetup uiObjectWithSetup)
                uiObjectWithSetup.TearDown();

            // Unregister from the refresh event channel
            if (this is IRefreshable refreshable)
                RefreshUIEventChannel.UnregisterEventListener(refreshable.Refresh);
            
            // Unregister from the data context events
            if (this is IUIObjectWithDataContext uiObjectWithData)
                uiObjectWithData.DetachDataContextEvents();
        }

        void IInitializable._Initialize()
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
                RefreshUIEventChannel.RegisterEventListener(refreshable.Refresh);
            
            // If the object is renderable, render it
            if (this is IRenderable renderable) renderable.Render();
        }
    }
}