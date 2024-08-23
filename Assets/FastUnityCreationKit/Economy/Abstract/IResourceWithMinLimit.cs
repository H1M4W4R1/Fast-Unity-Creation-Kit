using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Economy.Abstract
{
    /// <summary>
    /// Represents that resource has a minimum limit.
    /// </summary>
    public interface IResourceWithMinLimit<out TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Maximum amount of the resource that can be stored.
        /// </summary>
        public TNumberType MinAmount { get; }
    }
}