using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has an icon.
    /// </summary>
    public interface IWithIcon<[UsedImplicitly] TIconUsage>
        where TIconUsage : IUsageContext
    {
        /// <summary>
        /// Icon of the object.
        /// </summary>
        public Sprite Icon { get; }
    }
}