using FastUnityCreationKit.Context.Abstract;
using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy.Context
{
    /// <summary>
    /// Represents read-only resource related context.
    /// </summary>
    /// <typeparam name="TNumberType">The number type.</typeparam>
    public interface IReadResourceContext<TNumberType> : IContext
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Economy that contains the resource. Can be null - in this case, the global economy is used.
        /// </summary>
        [CanBeNull] IWithLocalEconomy Economy { get; set; }
    }
}