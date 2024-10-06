using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Economy.Context.Internal
{
    /// <summary>
    /// Used to add resource amount.
    /// </summary>
    public struct GenericAddResourceContext : IAddResourceContext
    {
        public IWithLocalEconomy Economy { get; set; }
        public int32 Amount { get; set; }
        
        public GenericAddResourceContext(IWithLocalEconomy economy, int32 amount)
        {
            Economy = economy;
            Amount = amount;
        }
    }
}