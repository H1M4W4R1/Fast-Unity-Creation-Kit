using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a name.
    /// </summary>
    public interface INamed<[UsedImplicitly] TUsageContext> 
        where TUsageContext : IUsageContext
    {
        /// <summary>
        /// Name of the object.
        /// </summary>
        public string Name { get; }
    }
}