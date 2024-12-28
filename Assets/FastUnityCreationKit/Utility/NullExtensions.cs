using System.Runtime.CompilerServices;

namespace FastUnityCreationKit.Utility
{
    public static class NullExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNull(this object obj)
        {
            return obj switch
            {
                UnityEngine.Object unityObject when unityObject == null => true,
                null => true,
                _ => false
            };
        }
        
    }
}