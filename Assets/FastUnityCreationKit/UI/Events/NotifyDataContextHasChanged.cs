using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.UI.Abstract;

namespace FastUnityCreationKit.UI.Events
{
    /// <summary>
    /// Event channel for notifying that the data context has changed.
    /// </summary>
    public sealed class NotifyDataContextHasChanged<TData> : GlobalEventChannel<NotifyDataContextHasChanged<TData>> 
        where TData : class, IDataContext
    {
    }
}