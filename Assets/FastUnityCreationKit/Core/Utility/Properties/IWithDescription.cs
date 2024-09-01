using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a description.
    /// </summary>
    public interface IWithDescription<[UsedImplicitly] TUsageContext> : IWithDescription,
        IWithProperty<IWithDescription<TUsageContext>, IWithDescription<AnyUsageContext>, string, TUsageContext>
        where TUsageContext : IUsageContext
    {
        /// <summary>
        /// Description of the object.
        /// </summary>
        public string Description { get; }

        string IWithProperty<IWithDescription<TUsageContext>, IWithDescription<AnyUsageContext>, string, TUsageContext>.
            Property => Description;
    }

    public interface IWithDescription
    {
        /// <summary>
        /// Tries to get the description of the specified usage context.
        /// </summary>
        [CanBeNull]
        public string GetDescription<TUsageContext>()
            where TUsageContext : IUsageContext
            => IWithProperty<IWithDescription<TUsageContext>, IWithDescription<AnyUsageContext>, string, TUsageContext>
                .GetProperty(this);
    }
}