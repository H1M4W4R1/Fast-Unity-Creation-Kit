using JetBrains.Annotations;
using UnityEngine.Localization;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a localized description.
    /// </summary>
    public interface IWithLocalizedDescription<[UsedImplicitly] TUsageContext> : IWithLocalizedDescription,
        IWithProperty<IWithLocalizedDescription<TUsageContext>, IWithLocalizedDescription<AnyUsageContext>,
            LocalizedString, TUsageContext>
        where TUsageContext : IUsageContext
    {
        /// <summary>
        /// Localized description of the object.
        /// It is recommended to assign this by hand or from a scriptable object configuration object.
        /// </summary>
        public LocalizedString LocalizedDescription { get; }

        LocalizedString
            IWithProperty<IWithLocalizedDescription<TUsageContext>, IWithLocalizedDescription<AnyUsageContext>,
                LocalizedString, TUsageContext>.Property => LocalizedDescription;
    }

    public interface IWithLocalizedDescription
    {
        /// <summary>
        /// Tries to get the localized description of the specified usage context.
        /// </summary>
        [CanBeNull]
        public LocalizedString GetLocalizedDescription<TUsageContext>()
            where TUsageContext : IUsageContext
            => IWithProperty<IWithLocalizedDescription<TUsageContext>, IWithLocalizedDescription<AnyUsageContext>,
                    LocalizedString, TUsageContext>
                .GetProperty(this);
    }
}