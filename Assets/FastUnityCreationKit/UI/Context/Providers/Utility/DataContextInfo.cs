namespace FastUnityCreationKit.UI.Context.Providers.Utility
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

        public DataContextInfo(IDataContextProvider<TDataContext> provider, TDataContext context)
        {
            IsValid = provider != null;
            Provider = provider;
            Context = context;
        }
		
        
    }
}