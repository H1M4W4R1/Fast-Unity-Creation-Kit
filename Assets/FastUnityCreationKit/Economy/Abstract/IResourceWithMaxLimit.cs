using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Economy.Abstract
{
    /// <summary>
    /// Represents that resource has a maximum limit.
    /// </summary>
    public interface IResourceWithMaxLimit<out TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Maximum amount of the resource that can be stored.
        /// </summary>
        public TNumberType MaxAmount { get; }
    }
}