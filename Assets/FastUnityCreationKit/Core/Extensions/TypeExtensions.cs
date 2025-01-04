using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Sirenix.Utilities;

namespace FastUnityCreationKit.Core.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets nice name for the member
        /// e.g. GetNiceName() => "Get Nice Name"
        /// IDataContext => "Data Context"
        /// </summary>
        [NotNull] public static string GetLabel([NotNull] this Type type)
        {
            // For interfaces skip 'I' prefix
            if (type.IsInterface)
            {
                string interfaceName = type.Name;
                if (interfaceName.StartsWith("I", StringComparison.Ordinal))
                    return interfaceName[1..].SplitPascalCase();
            }
            
            return type.Name.SplitPascalCase();
        }
        
        /// <summary>
        /// Get all interfaces that are of the same type as the root interface
        /// Works only for interfaces in the same assembly to avoid performance issues.
        /// </summary>
        [NotNull] public static List<Type> GetSameAssemblyInterfacesByRootInterface(
            [NotNull] this Type interfaceType,
            bool skipGeneric = true,
            [NotNull] params Type[] skipIfImplementsOrInherits)
        {
            // Get all types in the assembly
            Type[] allTypes = interfaceType.Assembly.GetTypes();
            
            // Create a list to store the found interfaces
            List<Type> foundInterfaces = new List<Type>();
            
            // Loop through all types
            foreach (Type type in allTypes)
            {
                // Skip if the type is generic
                if(skipGeneric && type.IsGenericType) continue;
                
                // Skip if interface is the same as the root interface
                if (type == interfaceType) continue;
                
                // Skip if the type implements or inherits from the specified types
                if (skipIfImplementsOrInherits.Length > 0)
                {
                    bool skip = false;
                    for (int index = 0; index < skipIfImplementsOrInherits.Length; index++)
                    {
                        // Check if the type implements or inherits from the specified type
                        Type skipType = skipIfImplementsOrInherits[index];
                        if (!skipType.IsAssignableFrom(type)) continue;
                        
                        // Skip the type
                        skip = true;
                        break;
                    }

                    // Skip the type if needed
                    if (skip) continue;
                }
                
                // Check if the type is an interface
                if (!type.IsInterface) continue;
                
                // Check if the interface is of the desired type
                if (!interfaceType.IsAssignableFrom(type)) continue;
                
                // Add the interface to the list
                foundInterfaces.Add(type);
            }
            
            return foundInterfaces;
        }
        
        public static void CallGenericCascadeInterfaces<TMethodData>(
            [NotNull] this object obj,
            [NotNull] Type genericInterface,
            [NotNull] string methodName,
            [NotNull] TMethodData data)
        {
            // Get all generic interfaces implemented by the class
            Type[] foundInterfaces = obj.GetType().GetInterfaces();

            // Loop through all interfaces
            foreach (Type ifx in foundInterfaces)
            {
                // Check if the interface is a generic interface
                // that is of desired type
                if (!ifx.IsGenericType || ifx.GetGenericTypeDefinition() != genericInterface) continue;

                // Get the method info of the generic method
                MethodInfo methodInfo = ifx.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

                // Invoke the generic method
                methodInfo?.Invoke(obj, new object[] {data});
            }
        }

        /// <summary>
        ///     Check if the type has the attribute
        /// </summary>
        public static bool HasAttribute<TAttribute>([NotNull] this Type type, bool inherit = true)
        {
            // Get the attribute from the current type
            foreach (object customAttribute in type.GetCustomAttributes(inherit))
                // Check if custom attribute is of the type we are looking for
                if (customAttribute is TAttribute)
                    return true;

            return false;
        }

        /// <summary>
        ///     Get the attribute from the type
        /// </summary>
        [CanBeNull] public static TAttribute GetAttribute<TAttribute>(
            [NotNull] this Type type,
            bool inherit = true)
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