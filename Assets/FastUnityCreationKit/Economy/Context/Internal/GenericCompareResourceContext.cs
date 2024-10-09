using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Economy.Context.Internal
{
    /// <summary>
    /// Used to compare resource amount.
    /// </summary>
    /// <typeparam name="TNumberType"></typeparam>
    public struct GenericCompareResourceContext : ICompareResourceContext
    {
        public IWithLocalEconomy Economy { get; set; }
        public int32 Amount { get; set; }
        
        public GenericCompareResourceContext(IWithLocalEconomy economy, int32 amount)
        {
            Economy = economy;
            Amount = amount;
        }
    }
}