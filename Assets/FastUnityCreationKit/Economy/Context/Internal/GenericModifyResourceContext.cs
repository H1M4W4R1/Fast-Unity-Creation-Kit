using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Economy.Context.Internal
{
    /// <summary>
    /// Used to modify resource amount.
    /// </summary>
    public struct GenericModifyResourceContext : IModifyResourceContext
    {
        public IWithLocalEconomy Economy { get; set; }
        public int32 Amount { get; set; }

        public GenericModifyResourceContext(IWithLocalEconomy economy, int32 amount)
        {
            Economy = economy;
            Amount = amount;
        }
    }
}