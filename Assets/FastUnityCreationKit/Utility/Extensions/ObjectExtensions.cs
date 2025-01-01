using System;
using System.Reflection;
using FastUnityCreationKit.Utility.Logging;

namespace FastUnityCreationKit.Utility.Extensions
{
    public static class ObjectExtensions
    {
        public static T CastTo<T>(this object o)
        {
            if (o is T o1) return o1;

            Guard<ValidationLogConfig>.Error(
                $"Failed to cast object of type {o.GetType().FullName} to {typeof(T).FullName}");
            return default;
        }

        public static object CastToReflected(this object o, Type type)
        {
            MethodInfo methodInfo =
                typeof(ObjectExtensions).GetMethod(nameof(CastTo), BindingFlags.Static | BindingFlags.Public);
            Type[] genericArguments = new[] {type};
            MethodInfo genericMethodInfo = methodInfo?.MakeGenericMethod(genericArguments);
            return genericMethodInfo?.Invoke(null, new[] {o});
        }
    }
}