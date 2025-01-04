using FastUnityCreationKit.Annotations.Info;
using FastUnityCreationKit.UI.Context.Providers.Base;
using FastUnityCreationKit.UI.Context.Providers.Utility;
using FastUnityCreationKit.UI.Interfaces;
using FastUnityCreationKit.Core.Logging;
using JetBrains.Annotations;
using Sirenix.Utilities;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// Represents a UI object with context.
    /// </summary>
    [SupportedFeature(typeof(IRenderable))]
    public abstract class UIObjectWithContextBase<TDataContext> : UIObjectBase
        where TDataContext : notnull
    {
        /// <summary>
        /// Context provider for this object.
        /// </summary>
        [CanBeNull] protected DataContextProviderBase<TDataContext> DataContextProvider { get; set; }

        public override void OnObjectCreated()
        {
            // We need to assign data provider before calling base method
            // as base method may need this provider to render the object
            TryToAssignProvider();
            base.OnObjectCreated();
        }

        public override void OnObjectDestroyed()
        {
            base.OnObjectDestroyed();
            DetachProvider();
        }

        /// <summary>
        /// Attempts to get the data context provider of the specified type.
        /// This method exists to access provider without the need to actually provide the data context.
        /// </summary>
        /// <typeparam name="TDataContext">The type of the data context.</typeparam>
        /// <returns>The data context provider of the specified type or null if not found.</returns>
        [CanBeNull] public DataContextProviderBase<TDataContext> GetProvider()
        {
            // Check if provider is null, if so, try to assign it
            // if exists in the hierarchy of this object return current one
            if (ReferenceEquals(DataContextProvider, null)) TryToAssignProvider();
     
            // Return current provider
            return DataContextProvider;
        }

        /// <summary>
        /// Gets the data context for this object.
        /// This method can be overriden for very stupid use cases that may not require
        /// provider to be used.
        /// </summary>
        /// <typeparam name="TDataContext">The type of the data context.</typeparam>
        /// <returns>The data context of the specified type.</returns>
        public virtual DataContextInfo<TDataContext> GetDataContext()
        {
            // Get provider if exists
            IDataContextProvider<TDataContext> provider = GetProvider();

            // Check if provider is not null
            return provider != null ? new DataContextInfo<TDataContext>(provider, provider.Provide()) : default;
        }
        
        /// <summary>
        /// Gets the data context provider of the specified type.
        /// </summary>
        /// <typeparam name="TProviderType">The type of the data context provider.</typeparam>
        /// <returns>The data context provider of the specified type or null if not found.</returns>
        /// <remarks>
        /// It was found out that GetComponent is faster than Dictionary lookup, so it will remain
        /// as is for now. The only faster way would be to cache the provider, but that would be
        /// pretty hard to implement and maintain in a reliable way.
        /// </remarks>
        private TProviderType GetProviderByType<TProviderType>() 
            where TProviderType : IDataContextProvider
        {
            // Try to get provider on this object or cascade to parents until root
            // Also handle inactive objects to guarantee that provided will be found
            // if it exists in the hierarchy
            TProviderType provider = GetComponentInParent<TProviderType>(true);
            
            // Check if provider is not null
            if (provider != null) return provider;
            
            // if not found, log error
            Guard<UserInterfaceLogConfig>.Error($"Data context provider of type {typeof(TProviderType).GetCompilableNiceFullName()} not found on {name} or its parent.");
            return default;
        }

        /// <summary>
        /// Tries to assign the provider to this object.
        /// </summary>
        private void TryToAssignProvider()
        {
            // Try to detach provider, if provider is null, it will do nothing
            DetachProvider();

            // Assign provider by type, we assume that it's not yet assigned
            DataContextProvider = GetProviderByType<DataContextProviderBase<TDataContext>>();
            
            // Assign provider event, if provider is null, it will do nothing
            AttachProvider();
        }

        private void AttachProvider()
        {
            // Don't attach if provider is null
            if (ReferenceEquals(DataContextProvider, null)) return;
            
            DataContextProvider.OnProviderDestroyed += HandleProviderDestroyed;
            DataContextProvider.OnContextChanged += HandleContextHasChanged;
        }

        private void DetachProvider()
        {
            // Check if provider is null, if so, return
            if (ReferenceEquals(DataContextProvider, null)) return;
            
            // Unsubscribe from provider destroyed event
            DataContextProvider.OnProviderDestroyed -= HandleProviderDestroyed;
            DataContextProvider.OnContextChanged -= HandleContextHasChanged;
        }

        private void HandleContextHasChanged(TDataContext context)
        {
            if(this is IRenderable<TDataContext> renderable)
                renderable.Render(true);
        }

        private void HandleProviderDestroyed()
        {
            // Ensure that provider is not null, should not happen
            // but is kept for future safety
            if(ReferenceEquals(DataContextProvider, null)) return;
            
            // Unsubscribe from provider destroyed event
            DataContextProvider.OnProviderDestroyed -= HandleProviderDestroyed;
            
            // Clear provider
            DataContextProvider = null;
            
            // We don't assign provider here to prevent issues with provider
            // acquiring, GetProvider() will automatically assign it if needed
        }
    }
}