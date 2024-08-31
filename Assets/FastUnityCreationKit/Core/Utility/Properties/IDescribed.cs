using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a description.
    /// </summary>
    public interface IDescribed<[UsedImplicitly] TUsageContext> 
        where TUsageContext : IUsageContext
    {
        /// <summary>
        /// Description of the object.
        /// </summary>
        public string Description { get; }
    }
}