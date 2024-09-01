using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has an icon.
    /// </summary>
    public interface IWithIcon<[UsedImplicitly] TIconUsage> : IWithIcon,
        IWithProperty<IWithIcon<TIconUsage>, IWithIcon<AnyUsageContext>, Sprite, TIconUsage>
        where TIconUsage : IUsageContext
    {
        /// <summary>
        /// Icon of the object.
        /// </summary>
        public Sprite Icon { get; }
        
        Sprite IWithProperty<IWithIcon<TIconUsage>, IWithIcon<AnyUsageContext>, Sprite, TIconUsage>.Property => Icon;
    }
    
    public interface IWithIcon
    {
        /// <summary>
        /// Tries to get the icon of the specified usage context.
        /// </summary>
        [CanBeNull] public Sprite GetIcon<TIconUsage>()
            where TIconUsage : IUsageContext
            => IWithProperty<IWithIcon<TIconUsage>, IWithIcon<AnyUsageContext>, Sprite, TIconUsage>
                .GetProperty(this);
    }
}