using FastUnityCreationKit.Context.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a name.
    /// </summary>
    public interface IWithName<[UsedImplicitly] TUsageContext> : 
        IWithProperty<IWithName<TUsageContext>, IWithName<AnyUsageContext>, string, TUsageContext>,
        IWithName
        where TUsageContext : IUsageContext
    {
        /// <summary>
        /// Name of the object.
        /// </summary>
        public string Name { get; }
        
        string IWithProperty<IWithName<TUsageContext>,  IWithName<AnyUsageContext>, string, TUsageContext>.Property => Name;
        
    }

    public interface IWithName
    {
        /// <summary>
        /// Tries to get the name of the specified usage context.
        /// </summary>
        [CanBeNull]
        public string GetName<TUsageContext>()
            where TUsageContext : IUsageContext =>
            IWithProperty<IWithName<TUsageContext>, IWithName<AnyUsageContext>, string, TUsageContext>
                .GetProperty(this);
    }
}