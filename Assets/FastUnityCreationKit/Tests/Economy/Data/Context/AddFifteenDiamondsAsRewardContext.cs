using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy.Abstract;
using FastUnityCreationKit.Economy.Context;

namespace FastUnityCreationKit.Tests.Economy.Data.Context
{
    /// <summary>
    /// Adds fifteen diamonds as reward context.
    /// </summary>
    public sealed class AddFifteenDiamondsAsRewardContext : IAddResourceContext
    {
        public IWithLocalEconomy Economy { get; set; } = null;
        public int32 Amount { get; set; } = 15;
    }
}