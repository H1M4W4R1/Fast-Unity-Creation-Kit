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
        protected void _AttachDataContextEvents() =>
            NotifyDataContextHasChanged<TData>.RegisterEventListener(OnDataContextChanged);
        
        /// <summary>
        /// Unregisters the event listeners for the data context.
        /// </summary>
        protected void _DetachDataContextEvents() =>
            NotifyDataContextHasChanged<TData>.UnregisterEventListener(OnDataContextChanged);
        
        /// <summary>
        /// Internal method to bind the data context to the UI object.
        /// </summary>
        protected void _TryAutomaticContextBinding()
        {
            // If data context is already bound, return
            // This is used in hierarchy-based data context binding
            // where the data context from parent is passed to the children
            if (DataContext != null) return;

            // Check if this is MonoBehaviour
            if (this is not MonoBehaviour monoBehaviour)
            {
                // Log error if this is not MonoBehaviour
                Debug.LogError("Automatic context binding can only be used with MonoBehaviour objects.");
                return;
            }
            
            // Try to bind local data context
            LocalDataContext localDataContext = monoBehaviour.GetComponent<LocalDataContext>();
            if (localDataContext != null) BindDataContext(localDataContext as TData);

            // Try to bind global data context
            // Use instance pass to do type checking
            TData instance = new TData();

            // If the global data context is the same type as the data context, bind it
            if (instance is GlobalDataContext globalDataContext)
                BindDataContext(globalDataContext.GetInstance() as TData);

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
        /// </summary>
        public void OnDataContextChanged();
        
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