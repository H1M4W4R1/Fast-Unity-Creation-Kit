using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Context.Providers.Utility
{
    public interface IDataContextProvider<out TDataContext> : IDataContextProvider
    {
        /// <summary>
        /// Provides the data context.
        /// </summary>
        [CanBeNull] public TDataContext Provide();
        
        /// <summary>
        /// Provides the data context at the specified index.
        /// </summary>
        /// <param name="index">Index of the data context.</param>
        /// <returns>Data context at the specified index.</returns>
        [CanBeNull] public TDataContext ProvideAt(int index);

        object IDataContextProvider.ProvideRaw() => Provide();
        object IDataContextProvider.ProvideAtRaw(int index) => ProvideAt(index);
    }
    
    /// <summary>
    /// Internal marker class for data context providers.
    /// </summary>
    public interface IDataContextProvider
    {
        bool IsDirty { get; }
        
        /// <summary>
        /// Provides the data context.
        /// </summary>
        [CanBeNull] public object ProvideRaw();
        
        /// <summary>
        /// Provides the data context at the specified index.
        /// </summary>
        /// <param name="index">Index of the data context.</param>
        /// <returns>Data context at the specified index.</returns>
        [CanBeNull] public object ProvideAtRaw(int index);
    }
}