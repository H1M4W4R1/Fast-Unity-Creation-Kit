namespace FastUnityCreationKit.UI.Context
{
    public interface IDataContextProvider<out TDataContext>
    {
        bool IsDirty { get; set; }
        
        /// <summary>
        /// Provides the data context.
        /// </summary>
        public TDataContext Provide();
    }
}