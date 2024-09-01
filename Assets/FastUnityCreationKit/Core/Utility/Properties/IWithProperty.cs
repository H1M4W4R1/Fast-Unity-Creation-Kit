using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Core.Utility.Properties
{
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