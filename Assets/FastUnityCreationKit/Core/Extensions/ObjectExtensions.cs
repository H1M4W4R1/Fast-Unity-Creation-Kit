using System;
using System.Reflection;
using FastUnityCreationKit.Core.Logging;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Extensions
{
    public static class ObjectExtensions
    {
        [CanBeNull] public static T CastTo<T>([NotNull] this object o)
        {
            if (o is T o1) return o1;

            Guard<ValidationLogConfig>.Error(
                $"Failed to cast object of type {o.GetType().FullName} to {typeof(T).FullName}");
            return default;
        }

        [CanBeNull] public static object CastToReflected([NotNull] this object o, [NotNull] Type type)
        {
            MethodInfo methodInfo =
                typeof(ObjectExtensions).GetMethod(nameof(CastTo), BindingFlags.Static | BindingFlags.Public);
            Type[] genericArguments = {type};
            MethodInfo genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return genericMethodInfo?.Invoke(null, new[] {o});
        }
    }
}