using FastUnityCreationKit.Context.Abstract;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Context
{
    /// <summary>
    /// Used to compare resource amount.
    /// </summary>
    /// <typeparam name="TNumberType">The number type.</typeparam>
    public interface ICompareResourceContext<TNumberType> : IReadResourceContext<TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// The amount of resource to modify.
        /// </summary>
        TNumberType Amount { get; set; }
    }
}