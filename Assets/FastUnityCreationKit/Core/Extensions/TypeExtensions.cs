using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastUnityCreationKit.Core.Extensions.Enums;
using JetBrains.Annotations;
using Sirenix.Serialization;
using Sirenix.Utilities;

namespace FastUnityCreationKit.Core.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        ///     Perform cascade search on the type - it scans all properties and fields
        ///     of the type and all its base types for the specified attribute.
        /// </summary>
        /// <param name="onType">Type to perform search on</param>
        /// <param name="memberSearchPredicate">Predicate to filter types, null means always true</param>
        /// <param name="onFound">Action to perform when type is found, can be omitted</param>
        /// <param name="foundTypes">List of found types</param>
        /// <param name="skipCascadeOnTypeIfConditionsNotMet">Skip cascade search on type if conditions are not met</param>
        /// <returns>Number of found types</returns>
        public static int PerformCascadeSearch(
            [NotNull] this Type onType,
            [CanBeNull] Predicate<MemberInfo> memberSearchPredicate,
            [CanBeNull] Action<MemberInfo> onFound,
            [CanBeNull] List<Type> foundTypes = null,
            bool skipCascadeOnTypeIfConditionsNotMet = true)
        {
            // Initialize variables
            foundTypes ??= new List<Type>();
            memberSearchPredicate ??= _ => true;
            onFound ??= _ => { };

            // Check if type is already found
            if (foundTypes.Contains(onType)) return 0;

            // Add current type to found types
            foundTypes.Add(onType);

            // Check if type is class or struct, otherwise we ignore it
            if (onType is {IsClass: false, IsValueType: false}) return 0;

            // Add all base classes to verified types            
            Type bType = onType.BaseType;
            while (bType != null)
            {
                // Add base type to verified types
                if(!foundTypes.Contains(bType)) foundTypes.Add(bType);
                bType = bType.BaseType;
            }

            int counter = 0;

            // Check all fields
            foreach (FieldInfo field in onType.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                         BindingFlags.NonPublic))
            {
                // Check if conditions are met
                if (memberSearchPredicate(field))
                    onFound(field);
                else if(skipCascadeOnTypeIfConditionsNotMet) continue;

                // Increase counter by the result of the cascade search on field type
                counter += PerformCascadeSearch(field.FieldType, memberSearchPredicate, onFound, foundTypes);
            }

            // Check all properties
            foreach (PropertyInfo property in onType.GetProperties(BindingFlags.Instance | BindingFlags.Public |
                                                                   BindingFlags.NonPublic))
            {
                // Check if conditions are met
                if (memberSearchPredicate(property))
                    onFound(property);
                else if(skipCascadeOnTypeIfConditionsNotMet) continue;
                    
                // Increase counter by the result of the cascade search on property return type
                counter += PerformCascadeSearch(property.PropertyType, memberSearchPredicate, onFound, foundTypes);
            }

            // Return counter
            return counter;
        }

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
        ///     Get all interfaces of the specified type. It will return all interfaces
        ///     that are inherit from the specified interface type.
        /// </summary>
        /// <param name="objectType">Type to get interfaces from</param>
        /// <param name="interfaceType">Type of the interface</param>
        /// <param name="searchMode">Defines search mode of interface</param>
        /// <returns>List of interfaces</returns>
        [NotNull] public static List<Type> GetInterfacesByType(
            [NotNull] this Type objectType,
            [NotNull] Type interfaceType,
            TypeSearchMode searchMode = TypeSearchMode.All)
        {
            List<Type> foundInterfaces = new();

            // Get all interfaces implemented by the class
            Type[] interfaces = objectType.GetInterfaces();

            // Loop through all interfaces
            foreach (Type ifx in interfaces)
            {
                // Check if the interface is of desired type
                switch (ifx.IsGenericType)
                {
                    // Skip generic interfaces if they are not allowed
                    case true when (searchMode & TypeSearchMode.Generic) == 0:
                    case false when (searchMode & TypeSearchMode.NonGeneric) == 0: continue;
                }

                // Check if interface must be valid and non-generic
                if (ifx.ContainsGenericParameters && (searchMode & TypeSearchMode.Valid) != 0) continue;

                // Skip self type if it is not allowed
                if (ifx == interfaceType && (searchMode & TypeSearchMode.IncludeSelf) == 0) continue;

                // Check if the interface is of desired type
                if (ifx.ImplementsOrInherits(interfaceType)) foundInterfaces.Add(ifx);
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