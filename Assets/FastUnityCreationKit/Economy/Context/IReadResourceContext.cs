using FastUnityCreationKit.Context.Abstract;
using FastUnityCreationKit.Economy.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Context
{
    /// <summary>
    /// Represents read-only resource related context.
    /// </summary>
    public interface IReadResourceContext : IContext
    {
        /// <summary>
        /// Economy that contains the resource. Can be null - in this case, the global economy is used.
        /// </summary>
        [CanBeNull] IWithLocalEconomy Economy { get; set; }
    }
}