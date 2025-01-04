using System;
using FastUnityCreationKit.Core.Serialization.Interfaces;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Serialization
{
    public static class ConvertibleExtensions
    {
        /// <summary>
        ///     Converts the current instance to the target type.
        /// </summary>
        public static TTargetType Convert<TTargetType>([NotNull] this IConvertibleTo<TTargetType> convertible)
        {
            return convertible.Convert();
        }

        /// <summary>
        ///     Tries to convert the object to the target type using the
        ///     best available method (IConvertibleTo, safe cast, operators).
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <param name="result">The result of the conversion.</param>
        /// <typeparam name="TTargetType">The target type to convert to.</typeparam>
        public static bool TryConvert<TTargetType>(this object obj, out TTargetType result)
        {
            switch (obj)
            {
                // Check if supports direct conversion interface
                case IConvertibleTo<TTargetType> convertible:
                    result = convertible.Convert();
                    return true;

                // Support basic C# conversions too
                case TTargetType targetType:
                    result = targetType;
                    return true;

                // Also attempt to convert using System.Convert
                // to handle convert operators
                default:
                    try
                    {
                        result = (TTargetType) System.Convert.ChangeType(obj, typeof(TTargetType));
                        return true;
                    }
                    catch (Exception) // When not supported
                    {
                        result = default;
                        return false;
                    }
            }
        }
    }
}