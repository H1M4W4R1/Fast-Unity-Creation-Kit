using System;
using System.Reflection;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Extensions
{
    public static class TypeExtensions
    {
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