using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Economy.Abstract
{
    /// <summary>
    /// Represents that resource has a default amount.
    /// </summary>
    public interface IResourceWithDefaultAmount<out TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        /// <summary>
        /// Default amount of the resource.
        /// </summary>
        public TNumberType DefaultAmount { get; }
    }
}