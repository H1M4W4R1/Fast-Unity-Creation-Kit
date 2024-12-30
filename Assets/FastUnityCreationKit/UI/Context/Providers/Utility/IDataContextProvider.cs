namespace FastUnityCreationKit.UI.Context.Providers.Utility
{
    public interface IDataContextProvider<out TDataContext>
    {
        bool IsDirty { get; }
        
        /// <summary>
        /// Provides the data context.
        /// </summary>
        public TDataContext Provide();
        
        /// <summary>
        /// Provides the data context at the specified index.
        /// </summary>
        /// <param name="index">Index of the data context.</param>
        /// <returns>Data context at the specified index.</returns>
        public TDataContext ProvideAt(int index);

        void Consume();
    }
}