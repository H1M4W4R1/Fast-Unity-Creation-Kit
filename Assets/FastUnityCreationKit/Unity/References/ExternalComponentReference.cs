using System;
using FastUnityCreationKit.Unity.Events;
using FastUnityCreationKit.Unity.Events.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity.References
{
    /// <summary>
    /// Reference to an external component that is not part of the current object.
    /// Requires to be disposed when not needed anymore, otherwise it will keep listening to the events
    /// and may cause memory leaks leading to null reference exceptions and thus unstable game.
    /// <br/><br/>
    /// It has been designed to serve as an automatic reference cleaner, however it only supports
    /// <see cref="FastMonoBehaviour"/> components due to its implementation compatible with the
    /// <see cref="OnObjectDestroyedEvent{T}"/>.
    /// </summary>
    public struct ExternalComponentReference<TComponentType> : IDisposable
        where TComponentType : FastMonoBehaviour, new()
    {
        [NotNull] private GameObject _gameObject;
        [CanBeNull] private TComponentType _component;
        private bool _isNull;
        
        /// <summary>
        /// Check if the reference is null.
        /// </summary>
        public bool IsNull => _isNull;
        
        public ExternalComponentReference([NotNull] GameObject gameObject, bool automaticallyAddComponent = false)
        {
            // Set fields (required for proper C# struct initialization).
            _gameObject = gameObject;
            _component = null;
            _isNull = true;
            
            // Initialize the reference.
            Initialize(automaticallyAddComponent);
        }
        
        /// <summary>
        /// Used to update the reference.
        /// </summary>
        public void SetReference([NotNull] GameObject gameObject, bool automaticallyAddComponent = false)
        {
            // Check if the reference is the same.
            if (ReferenceEquals(_gameObject, gameObject)) return;
            
            bool wasNull = _isNull;
            
            // Set fields.
            _gameObject = gameObject;
            
            // Initialize the reference.
            Initialize(automaticallyAddComponent);
            
            // If the reference was not null and now is, unregister the event.
            if (!wasNull && _isNull) OnObjectDestroyedEvent<TComponentType>.UnregisterEventListener(OnObjectDestroyed);
            
            // If the reference was null and now is not, register the event.
            else if (wasNull && !_isNull) OnObjectDestroyedEvent<TComponentType>.RegisterEventListener(OnObjectDestroyed);
        }
        
        /// <summary>
        /// Call this from constructor to initialize the reference.
        /// </summary>
        private void Initialize(bool automaticallyAddComponent)
        {
            // Find the component on the game object.
            _component = _gameObject.GetComponent<TComponentType>();
            
            // If component is null check if we should add it.
            if (ReferenceEquals(_component, null) && automaticallyAddComponent)
                _component = _gameObject.AddComponent<TComponentType>();
            
            // If component is still null, set the flag.
            _isNull = ReferenceEquals(_component, null);
            
            // Register event if the reference is not null.
            if(!_isNull) OnObjectDestroyedEvent<TComponentType>.RegisterEventListener(OnObjectDestroyed);
        }

        /// <summary>
        /// Called when the object (component, gameObject or its parent) is destroyed.
        /// Should automatically unregister the event.
        /// <br/><br/>
        /// This might get called even if the owner of this reference is destroyed,
        /// but it's heavily recommended to dispose the reference when the owner is destroyed.
        /// </summary>
        private void OnObjectDestroyed(FastMonoBehaviourDestroyedData<TComponentType> data)
        {
            // Check if the reference is the same.
            if (!ReferenceEquals(data.reference, _component)) return;
            
            // Clear the reference.
            _component = null;
            
            // Unregister event if the reference is not null.
            if(!_isNull) OnObjectDestroyedEvent<TComponentType>.UnregisterEventListener(OnObjectDestroyed);
            
            _isNull = true;
        }

        public void Dispose()
        {
            // Unregister event if the reference is not null.
            if(!_isNull) OnObjectDestroyedEvent<TComponentType>.UnregisterEventListener(OnObjectDestroyed);
        }
    }
}