using FastUnityCreationKit.Context.Abstract;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a configuration.
    /// </summary>
    public interface IWithConfiguration<TConfigurationType, [UsedImplicitly] TUsageContext> : IWithConfiguration,
        IWithProperty<IWithConfiguration<TConfigurationType, TUsageContext>,
            IWithConfiguration<TConfigurationType, AnyUsageContext>, TConfigurationType, TUsageContext>
        where TConfigurationType : ScriptableObject
        where TUsageContext : IUsageContext
    {
        /// <summary>
        /// Gets the configuration of the object.
        /// Should be a method to acquire the scriptable object from global configuration manager
        /// or from local serialized field.
        /// </summary>
        public TConfigurationType Configuration { get; }
        
        TConfigurationType IWithProperty<IWithConfiguration<TConfigurationType, TUsageContext>,
            IWithConfiguration<TConfigurationType, AnyUsageContext>, TConfigurationType, TUsageContext>.Property
            => Configuration;
    }

    public interface IWithConfiguration
    {
        /// <summary>
        /// Tries to get the configuration of the specified type and usage context.
        /// </summary>
        [CanBeNull]
        public TConfigurationType GetConfiguration<TConfigurationType, TUsageContext>()
            where TConfigurationType : ScriptableObject
            where TUsageContext : IUsageContext =>
            IWithProperty<IWithConfiguration<TConfigurationType, TUsageContext>,
                IWithConfiguration<TConfigurationType, AnyUsageContext>, TConfigurationType, TUsageContext>
                .GetProperty(this);
        
    }
}