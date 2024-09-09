using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Utility.Singleton;
using FastUnityCreationKit.UI.Events;
using UnityEngine;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// Represents a UI object that can be bound to a data context.
    /// </summary>
    public interface IUIObjectWithDataContext<TData> : IUIObjectWithDataContext
        where TData : class, IDataContext, new()
    {
        /// <summary>
        /// Reference to the data context.
        /// </summary>
        public TData DataContext { get; protected set; }

        /// <summary>
        /// Registers the event listeners for the data context.
        /// </summary>
        internal void _AttachDataContextEvents() =>
            NotifyDataContextHasChanged<TData>.RegisterEventListener(OnDataContextChangedAsync);

        /// <summary>
        /// Unregisters the event listeners for the data context.
        /// </summary>
        internal void _DetachDataContextEvents() =>
            NotifyDataContextHasChanged<TData>.UnregisterEventListener(OnDataContextChangedAsync);

        /// <summary>
        /// Internal method to bind the data context to the UI object.
        /// </summary>
        internal void _TryAutomaticContextBinding()
        {
            // If data context is already bound, return
            // This is used in hierarchy-based data context binding
            // where the data context from parent is passed to the children
            if (DataContext != null) return;

            // Check if this is MonoBehaviour
            if (this is MonoBehaviour monoBehaviour)
            {
                // Try to bind local data context
                // If successful, return
                monoBehaviour.TryGetComponent(out TData localDataContext);
                if (localDataContext != null)
                {
                    BindDataContext(localDataContext);
                    return;
                }
            }

            // Try to bind global data context, use singleton pattern
            // to acquire the instance of the global data context
            TData singleton = IUnsafeSingleton<TData>.GetInstance();
            if(singleton is GlobalDataContext)
                BindDataContext(singleton);
            
            // If the object supports passing data context to children, pass it
            if (this is IPassDataContextToChildren<TData> passDataContextToChildren)
                passDataContextToChildren.PassDataContextToChildren();
        }

        /// <summary>
        /// Binds the data context to the UI object.
        /// It is strongly recommended to use automatic context binding instead.
        /// </summary>
        public void BindDataContext(TData dataContext)
        {
            DataContext = dataContext;
            DataContext.OnBind(this);
        }

        /// <summary>
        /// Executed when the data context is changed.
        /// Called asynchronously.
        /// </summary>
        public UniTask OnDataContextChangedAsync();

        void IUIObjectWithDataContext.TryAutomaticContextBinding() => _TryAutomaticContextBinding();

        void IUIObjectWithDataContext.AttachDataContextEvents() => _AttachDataContextEvents();

        void IUIObjectWithDataContext.DetachDataContextEvents() => _DetachDataContextEvents();
    }

    /// <summary>
    /// Internal interface used to bind data context to the UI object.
    /// </summary>
    public interface IUIObjectWithDataContext
    {
        /// <summary>
        /// Tries to automatically bind the data context to the UI object.
        /// </summary>
        public void TryAutomaticContextBinding();

        /// <summary>
        /// Attaches the event listeners for the data context.
        /// </summary>
        public void AttachDataContextEvents();

        /// <summary>
        /// Detaches the event listeners for the data context.
        /// </summary>
        public void DetachDataContextEvents();
    }
}