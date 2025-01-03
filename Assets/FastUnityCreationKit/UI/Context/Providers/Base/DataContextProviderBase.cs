using FastUnityCreationKit.UI.Context.Providers.Utility;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Basic;
using FastUnityCreationKit.Utility.Logging;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

namespace FastUnityCreationKit.UI.Context.Providers.Base
{
    /// <summary>
    /// Represents a data context provider.
    /// </summary>
    public abstract class DataContextProviderBase<TContextType> : FastMonoBehaviour, IDataContextProvider<TContextType>,
        ICreateCallback, IDestroyCallback, IUpdateCallback
    {
        protected const string PROVIDER_CONFIGURATION = "Provider Configuration";
        
        public delegate void OnContextChangedHandler(TContextType context);
        public delegate void OnProviderDestroyedHandler();

        /// <summary>
        /// Invoked when the context has changed.
        /// </summary>
        public event OnContextChangedHandler OnContextChanged;
        
        /// <summary>
        /// Invoked when the provider has been destroyed.
        /// Can be used to detach from the provider and clean up resources.
        /// </summary>
        public event OnProviderDestroyedHandler OnProviderDestroyed;

        /// <summary>
        /// Represents the dirty state of the data context.
        /// Setting to true will notify all subscribers that the context has changed
        /// within the next frame.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(PROVIDER_CONFIGURATION)]
        public bool IsDirty { get; set; }

        /// <summary>
        /// Provides the data context.
        /// </summary>
        [CanBeNull] public abstract TContextType Provide();

        /// <summary>
        /// Provides the data context at the specified index.
        /// </summary>
        /// <param name="index">Index of the data context.</param>
        /// <returns>Data context at the specified index.</returns>
        [CanBeNull] public virtual TContextType ProvideAt(int index)
        {
            Guard<ValidationLogConfig>.Warning(
                $"ProvideAt method on {GetType().GetCompilableNiceFullName()} is not overridden. Are you accessing wrong provider? " +
                "Executing fallback to default Provide method.");
            Provide();
            return default;
        }
        
        protected virtual void Setup() {}
        
        protected virtual void TearDown() {}

        /// <summary>
        /// Notifies this provider that the context has changed.
        /// All subscribers will be instantly notified about the change.
        /// </summary>
        /// <remarks>
        /// If you want more performance it's recommended to avoid this method
        /// in favor of <see cref="IsDirty"/>
        /// </remarks>
        protected virtual void NotifyContextHasChanged()
        {
            // Ensure that context is mark as dirty to guarantee that it will be updated
            // on each subscriber
            IsDirty = true;
            
            OnContextChanged?.Invoke(Provide());
            IsDirty = false;
        }

        public void OnObjectCreated() => Setup();

        public void OnObjectDestroyed()
        {
            TearDown();
            OnProviderDestroyed?.Invoke();
        }

        public void OnObjectUpdated(float deltaTime)
        {
            // Check if data context is dirty
            // if so, notify that the context has changed
            if (IsDirty) NotifyContextHasChanged();
        }
    }
}