using FastUnityCreationKit.UI.Context.Providers.Utility;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Basic;
using FastUnityCreationKit.Utility.Logging;
using JetBrains.Annotations;
using Sirenix.Utilities;

namespace FastUnityCreationKit.UI.Context.Providers.Base
{
    /// <summary>
    /// Represents a data context provider.
    /// </summary>
    public abstract class DataContextProviderBase<TContextType> : FastMonoBehaviour, IDataContextProvider<TContextType>,
        ICreateCallback, IDestroyCallback
    {
        public delegate void OnContextChangedHandler(TContextType context);

        /// <summary>
        /// Notified when the context has changed.
        /// </summary>
        public event OnContextChangedHandler OnContextChanged;

        /// <summary>
        /// Represents the dirty state of the data context.
        /// </summary>
        public virtual bool IsDirty { get; private set; }

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
        /// Resets the dirty state of the data context.
        /// </summary>
        public virtual void Consume() => IsDirty = false;

        protected virtual void NotifyContextHasChanged()
        {
            IsDirty = true;
            OnContextChanged?.Invoke(Provide());
        }

        public void OnObjectCreated() => Setup();
        public void OnObjectDestroyed() => TearDown();
        
    }
}