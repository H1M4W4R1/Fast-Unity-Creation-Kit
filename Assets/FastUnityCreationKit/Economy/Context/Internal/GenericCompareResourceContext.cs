using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Economy.Context.Internal
{
    /// <summary>
    /// Used to compare resource amount.
    /// </summary>
    /// <typeparam name="TNumberType"></typeparam>
    public struct GenericCompareResourceContext<TNumberType> : ICompareResourceContext<TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        public IWithLocalEconomy Economy { get; set; }
        public TNumberType Amount { get; set; }
        
        public GenericCompareResourceContext(IWithLocalEconomy economy, TNumberType amount)
        {
            Economy = economy;
            Amount = amount;
        }
    }
}