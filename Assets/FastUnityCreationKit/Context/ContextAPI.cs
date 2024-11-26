using System.Runtime.CompilerServices;

namespace FastUnityCreationKit.Context
{
    /// <summary>
    /// Context API is used to provide quick access for common context analysis operations.
    /// </summary>
    public static class ContextAPI
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAnyContext<TContextType>() => typeof(TContextType) == typeof(AnyUsageContext);
    }
}