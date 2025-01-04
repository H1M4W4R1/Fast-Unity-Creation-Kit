using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Context.Providers.Utility
{
    public interface IDataContextProvider<out TDataContext> : IDataContextProvider
    {
        object IDataContextProvider.ProvideRaw()
        {
            return Provide();
        }

        object IDataContextProvider.ProvideAtRaw(int index)
        {
            return ProvideAt(index);
        }

        /// <summary>
        ///     Provides the data context.
        /// </summary>
        [CanBeNull] public TDataContext Provide();

        /// <summary>
        ///     Provides the data context at the specified index.
        /// </summary>
        /// <param name="index">Index of the data context.</param>
        /// <returns>Data context at the specified index.</returns>
        [CanBeNull] public TDataContext ProvideAt(int index);
    }

    /// <summary>
    ///     Internal marker class for data context providers.
    /// </summary>
    public interface IDataContextProvider
    {
        bool IsDirty { get; }

        /// <summary>
        ///     Provides the data context.
        /// </summary>
        [CanBeNull] public object ProvideRaw();

        /// <summary>
        ///     Provides the data context at the specified index.
        /// </summary>
        /// <param name="index">Index of the data context.</param>
        /// <returns>Data context at the specified index.</returns>
        [CanBeNull] public object ProvideAtRaw(int index);
    }
}