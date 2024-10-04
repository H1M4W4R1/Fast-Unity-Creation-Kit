using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Economy.Context.Internal
{
    /// <summary>
    /// Used to take resource amount.
    /// </summary>
    /// <typeparam name="TNumberType">Number type.</typeparam>
    public struct GenericTakeResourceContext<TNumberType> : ITakeResourceContext<TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        public IWithLocalEconomy Economy { get; set; }
        public TNumberType Amount { get; set; }

        public GenericTakeResourceContext(IWithLocalEconomy economy, TNumberType amount)
        {
            Economy = economy;
            Amount = amount;
        }
    }
}