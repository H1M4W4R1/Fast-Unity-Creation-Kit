using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Context.Providers.Base
{
    /// <summary>
    /// Represents a data context provider that references another data context provider.
    /// </summary>
    public abstract class ReferencedContextProviderBase<TContextBase> :
        DataContextProviderBase<TContextBase>
    {
        [SerializeField]
        [TitleGroup(PROVIDER_CONFIGURATION)]
        [Required]
        [CanBeNull]
        private DataContextProviderBase<TContextBase> providerReference;

        protected override void Setup()
        {
            // If the referenced provider is not null, we should subscribe to its events.
            if (providerReference)
                providerReference.OnContextChanged += OnContextChangedCallback;
        }
        
        protected override void TearDown()
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
        private void OnContextChangedCallback([CanBeNull] TContextBase context) =>
            NotifyContextHasChanged();

        /// <summary>
        /// Provides the data context from the referenced provider.
        /// </summary>
        public override TContextBase Provide() => providerReference ? providerReference.Provide() : default;
    }
}