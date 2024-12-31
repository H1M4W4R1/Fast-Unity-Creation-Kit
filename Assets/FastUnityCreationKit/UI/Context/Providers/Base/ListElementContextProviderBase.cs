using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Logging;

namespace FastUnityCreationKit.UI.Context.Providers.Base
{
    /// <summary>
    /// This class is used to provide an element from a list.
    /// It requires <see cref="ListContextProviderBase{TContextType}"/> to be present in the parent.
    /// </summary>
    /// <typeparam name="TContextType">Type of context.</typeparam>
    public abstract class ListElementContextProviderBase<TContextType> : DataContextProviderBase<TContextType>
    {
        /// <summary>
        /// Index to provide element from.
        /// </summary>
        public int Index { get; set; }
        
        public override TContextType Provide()
        {
            // Try to acquire any ListProvider in the parent
            ListContextProviderBase<TContextType> listProvider = GetComponentInParent<ListContextProviderBase<TContextType>>();
            
            // Ensure that the list provider is not null
            if (listProvider == null)
            {
                Guard<ValidationLogConfig>.Error($"ListElementContextProviderBase requires a ListContextProviderBase in the parent on object {name}.");
                return default;
            }
            
            // Ensure that the index is within the bounds
            if (Index < 0 || Index >= listProvider.Count)
            {
                Guard<ValidationLogConfig>.Error($"Index {Index} is out of bounds for ListElementContextProviderBase on object {name}.");
                return default;
            }

            // Return the element at the specified index
            return listProvider.ProvideAt(Index);
        }
    }
}