using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Economy.Context.Internal
{
    /// <summary>
    /// Used to modify resource amount.
    /// </summary>
    /// <typeparam name="TNumberType">Number type.</typeparam>
    public struct GenericModifyResourceContext<TNumberType> : IModifyResourceContext<TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        public IWithLocalEconomy Economy { get; set; }
        public TNumberType Amount { get; set; }

        public GenericModifyResourceContext(IWithLocalEconomy economy, TNumberType amount)
        {
            Economy = economy;
            Amount = amount;
        }
    }
}