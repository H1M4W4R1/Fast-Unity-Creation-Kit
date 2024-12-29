using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Context
{
    /// <summary>
    /// Represents a data context provider that references another data context provider.
    /// </summary>
    public abstract class ReferencedContextProviderBase<TContextBase> :
        DataContextProviderBase<TContextBase>
    {
        [SerializeField]
        [TabGroup("Configuration")]
        [Required]
        private DataContextProviderBase<TContextBase> providerReference;

        protected override void Awake()
        {
            base.Awake();
            
            // Subscribe to context change event of the referenced provider.
            providerReference.OnContextChanged += OnContextChangedCallback;
        }

        private void OnDestroy()
        {
            // If the referenced provider is destroyed, we should unsubscribe from its events.
            // This is to prevent memory leaks. We also need to check if the provider is null to
            // prevent null reference exceptions.
            if (providerReference)
                providerReference.OnContextChanged -= OnContextChangedCallback;
        }

        /// <summary>
        /// This method is invoked whenever context of the referenced provider changes.
        /// This will automatically update IsDirty flag and notify all subscribers.
        /// </summary>
        private void OnContextChangedCallback(TContextBase context) =>
            NotifyContextHasChanged();

        /// <summary>
        /// Provides the data context from the referenced provider.
        /// </summary>
        public override TContextBase Provide() => providerReference ? providerReference.Provide() : default;
    }
}