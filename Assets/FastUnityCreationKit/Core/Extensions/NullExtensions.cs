using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Extensions
{
    public static class NullExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNull([CanBeNull] this object obj)
        {
            return obj switch
            {
                UnityEngine.Object unityObject when unityObject == null => true,
                null => true,
                _ => obj.Equals(null)
            };
        }
        
    }
}