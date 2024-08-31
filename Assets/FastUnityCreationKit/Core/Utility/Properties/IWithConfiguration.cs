using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a configuration.
    /// </summary>
    public interface IWithConfiguration<out TConfigurationType, [UsedImplicitly] TUsageContext>
        where TConfigurationType : ScriptableObject
        where TUsageContext : IUsageContext
    {
        /// <summary>
        /// Gets the configuration of the object.
        /// Should be a method to acquire the scriptable object from global configuration manager
        /// or from local serialized field.
        /// </summary>
        public TConfigurationType Configuration { get; }
    }
}