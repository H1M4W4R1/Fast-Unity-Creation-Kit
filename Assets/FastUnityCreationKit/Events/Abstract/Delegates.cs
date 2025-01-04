using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Events.Abstract
{
    /// <summary>
    ///     Represents callback that is invoked when the event channel is triggered.
    /// </summary>
    public delegate UniTask EventChannelCallbackAsync();

    /// <summary>
    ///     Represents callback that is invoked when the event channel is triggered.
    ///     Also contains the data that is sent through the event channel.
    /// </summary>
    public delegate UniTask EventChannelCallbackAsync<in TEventData>([NotNull] TEventData data)
        where TEventData : notnull;
}