using FastUnityCreationKit.Core.Numerics;

namespace FastUnityCreationKit.Economy.Context
{
    /// <summary>
    /// Used to compare resource amount.
    /// </summary>
    public interface ICompareResourceContext : IReadResourceContext
    {
        /// <summary>
        /// The amount of resource to modify.
        /// </summary>
        int32 Amount { get; set; }
    }
}