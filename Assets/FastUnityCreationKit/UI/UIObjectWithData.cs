using FastUnityCreationKit.UI.Abstract;

namespace FastUnityCreationKit.UI
{
    /// <summary>
    /// Represents a UI object with data context.
    /// </summary>
    public abstract class UIObjectWithData<TData> :  UIObjectWithData
        where TData : class, IDataContext, new()
    {
        /// <summary>
        /// Reference to the data context.
        /// </summary>
        public TData DataContext { get; private set; }
        
        /// <inheritdoc/>
        public override void TryAutomaticContextBinding()
        {
            // If data context is already bound, return
            // This is used in hierarchy-based data context binding
            // where the data context from parent is passed to the children
            if(DataContext != null) return;
            
            // Try to bind local data context
            LocalDataContext localDataContext = GetComponent<LocalDataContext>();
            if(localDataContext != null) BindDataContext(localDataContext as TData);

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
        
    }

    /// <summary>
    /// Represents a UI object with data context.
    /// </summary>
    public abstract class UIObjectWithData : UIObject
    {
        /// <summary>
        /// Tries to automatically bind the data context to the UI object.
        /// </summary>
        public abstract void TryAutomaticContextBinding();
    }
}