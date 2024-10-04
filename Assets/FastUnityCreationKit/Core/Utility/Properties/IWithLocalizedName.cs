using FastUnityCreationKit.Context.Abstract;
using JetBrains.Annotations;
using UnityEngine.Localization;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a localized name.
    /// </summary>
    public interface IWithLocalizedName<[UsedImplicitly] TUsageContext> : IWithLocalizedName,
        IWithProperty<IWithLocalizedName<TUsageContext>, IWithLocalizedName<AnyUsageContext>, LocalizedString,
            TUsageContext>
        where TUsageContext : IUsageContext
    {
        /// <summary>
        /// Localized name of the object.
        /// It is recommended to assign this by hand or from a scriptable object configuration object.
        /// </summary>
        public LocalizedString LocalizedName { get; }

        LocalizedString
            IWithProperty<IWithLocalizedName<TUsageContext>, IWithLocalizedName<AnyUsageContext>, LocalizedString,
                TUsageContext>.Property => LocalizedName;
    }

    public interface IWithLocalizedName
    {
        /// <summary>
        /// Tries to get the localized name of the specified usage context.
        /// </summary>
        [CanBeNull]
        public LocalizedString GetLocalizedName<TUsageContext>()
            where TUsageContext : IUsageContext
            => IWithProperty<IWithLocalizedName<TUsageContext>, IWithLocalizedName<AnyUsageContext>, LocalizedString,
                    TUsageContext>
                .GetProperty(this);
    }
}