using System;

namespace FastUnityCreationKit.Utility.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Check if the type has the attribute
        /// </summary>
        public static bool HasAttribute<TAttribute>(this Type type, bool inherit = true)
        {
            // Get the attribute from the current type
            foreach (object customAttribute in type.GetCustomAttributes(inherit))
            {
                // Check if custom attribute is of the type we are looking for
                if (customAttribute is TAttribute)
                    return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// Get the attribute from the type
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Type type, bool inherit = true)
        {
            // Get the attribute from the current type
            foreach (object customAttribute in type.GetCustomAttributes(inherit))
            {
                // Check if custom attribute is of the type we are looking for
                if (customAttribute is not TAttribute foundAttribute) continue;
                
                return foundAttribute;
            }

            return default;
        }
        
    }
}