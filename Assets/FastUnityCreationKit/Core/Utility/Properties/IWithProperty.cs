using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Utility.Properties
{
    /// <summary>
    /// Represents that an object has a property.
    /// </summary>
    /// <typeparam name="TSelfInterface">Inheritor of this interface</typeparam>
    /// <typeparam name="TAnyInterface">Inheritor of this interface with TUsageContext as AnyUsageContext</typeparam>
    /// <typeparam name="TResultType">Property type</typeparam>
    /// <typeparam name="TUsageContext">Proxypass of usage context, should be a generic parameter at inheritor</typeparam>
    public interface IWithProperty<TSelfInterface, TAnyInterface, out TResultType, TUsageContext>
        where TSelfInterface : IWithProperty<TSelfInterface, TAnyInterface, TResultType, TUsageContext>
        where TAnyInterface : IWithProperty<TAnyInterface, TAnyInterface, TResultType, AnyUsageContext>
        where TUsageContext : IUsageContext
    {   
        /// <summary>
        /// Contains the property of the object.
        /// </summary>
        public TResultType Property { get; } 
        
        /// <summary>
        /// Tries to get the property of the specified type and usage context.
        /// </summary>
        [CanBeNull] public static TResultType GetProperty([NotNull] object obj)
        {
            // Check if this is the correct usage context
            if (obj is TSelfInterface withProperty)
                return withProperty.Property;
            
            // If not try to find with AnyUsageContext
            if (obj is TAnyInterface withAnyUsageContext)
                return withAnyUsageContext.Property;
            
            return default;
        }
        
    }
}