using JetBrains.Annotations;
using UnityEngine.Localization;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a localized name.
    /// </summary>
    public interface IWithLocalizedName<[UsedImplicitly] TUsageContext> 
        where TUsageContext : IUsageContext
    {
        /// <summary>
        /// Localized name of the object.
        /// It is recommended to assign this by hand or from a scriptable object configuration object.
        /// </summary>
        public LocalizedString LocalizedName { get; }
    }
}