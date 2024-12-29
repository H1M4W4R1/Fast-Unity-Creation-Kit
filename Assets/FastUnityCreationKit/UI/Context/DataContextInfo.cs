namespace FastUnityCreationKit.UI.Context
{
    /// <summary>
    /// Represents a data context info used to store data context and its provider.
    /// </summary>
    public readonly ref struct DataContextInfo<TDataContext>
    {
        public bool IsValid { get; }
        public IDataContextProvider<TDataContext> Provider { get; }
        public TDataContext Context { get; }

        public bool IsDirty => Provider.IsDirty;

        public void Consume() => Provider.Consume();
        
        public DataContextInfo(IDataContextProvider<TDataContext> provider, TDataContext context)
        {
            IsValid = provider != null;
            Provider = provider;
            Context = context;
        }
		
        
    }
}