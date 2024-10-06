using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Economy.Context.Internal
{
    /// <summary>
    /// Used to take resource amount.
    /// </summary>
    public struct GenericTakeResourceContext : ITakeResourceContext
    {
        public IWithLocalEconomy Economy { get; set; }
        public int32 Amount { get; set; }

        public GenericTakeResourceContext(IWithLocalEconomy economy, int32 amount)
        {
            Economy = economy;
            Amount = amount;
        }
    }
}