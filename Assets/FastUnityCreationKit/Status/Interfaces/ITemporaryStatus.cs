using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Status.Abstract;
using FastUnityCreationKit.Core.Objects;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Status.Interfaces
{
    /// <summary>
    /// Used to indicate that status is temporary and should be removed when conditions are met.
    /// By default, the status will be completely removed (cleared) when conditions are met.
    /// You can override this behavior by implementing the <see cref="ITemporaryStatus.RemoveStatusFromComponent"/> interface
    /// method explicitly.
    /// </summary>
    public interface ITemporaryStatus<TSelf> : ITemporaryStatus
        where TSelf : StatusBase, ITemporaryStatus<TSelf>
    {
        /// <summary>
        /// Override this implementation to define the behavior of the status when
        /// ITemporaryObjects removal conditions are met for current status.
        /// </summary>
        async UniTask ITemporaryStatus.RemoveStatusFromComponent(StatusContainer container)   
            => await container.Clear<TSelf>();
    }

    /// <summary>
    /// Internal interface used for markups.
    /// </summary>
    public interface ITemporaryStatus : ITemporaryObject
    {
        public UniTask RemoveStatusFromComponent([NotNull] StatusContainer container);


    }
}