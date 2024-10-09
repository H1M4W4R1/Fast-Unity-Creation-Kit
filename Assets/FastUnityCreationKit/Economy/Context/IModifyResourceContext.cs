using FastUnityCreationKit.Context.Abstract;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Context
{
    /// <summary>
    /// This context represents a resource modification.
    /// </summary>
    public interface IModifyResourceContext : IContext
    {
        /// <summary>
        /// Economy that contains the resource. Can be null - in this case, the global economy is used.
        /// </summary>
        [CanBeNull] IWithLocalEconomy Economy { get; set; }
        
        /// <summary>
        /// The amount of resource to modify.
        /// </summary>
        int32 Amount { get; set; }
    }
}