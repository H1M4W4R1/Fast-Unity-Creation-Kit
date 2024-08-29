using FastUnityCreationKit.Core.Numerics.Abstract;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    /// API functions and extensions for economy.
    /// </summary>
    public static class EconomyAPI
    {
        /// <summary>
        /// Gets global resource of type <typeparamref name="TResource"/>.
        /// </summary>
        public static GlobalResource<TResource, TNumberType> GetGlobalResource<TResource, TNumberType>()
            where TResource : GlobalResource<TResource, TNumberType>, new()
            where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
        {
            return GlobalResource<TResource, TNumberType>.Instance;
        }

    }
}