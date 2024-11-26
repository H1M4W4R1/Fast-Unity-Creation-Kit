using FastUnityCreationKit.Guardian.Data;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Guardian
{
    /// <summary>
    /// Guardian API entrance point
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Checks the instance
        /// </summary>
        public static CheckedInstance<TInstanceType> ThatObject<TInstanceType>([CanBeNull] TInstanceType instance) =>
            new(instance);
        
        /// <summary>
        /// Checks the type
        /// </summary>
        public static CheckedType ThatType<TType>() => new(typeof(TType));
    }
}