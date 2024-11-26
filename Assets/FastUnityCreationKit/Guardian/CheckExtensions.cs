using System.Runtime.CompilerServices;
using FastUnityCreationKit.Guardian.Logging;
using UnityEngine;

namespace FastUnityCreationKit.Guardian
{
    public static class CheckExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFailed(this AssertStatus assertStatus) => assertStatus == AssertStatus.Fail;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasPassed(this AssertStatus assertStatus) => assertStatus == AssertStatus.Pass;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AssertStatus LogIfFalse(this bool obj, LogType type, string message, ILogger logger = null)
            => LogIfTrue(!obj, type, message, logger);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AssertStatus LogIfTrue(this bool obj, LogType type, string message, ILogger logger = null)
        {
            // Check if condition is met
            if (!obj) return AssertStatus.Pass;
            
            new CheckLog(type, message, logger).Assert();
            return AssertStatus.Fail;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AssertStatus EditorLogIfFalse(this bool obj, LogType type, string message, ILogger logger = null) =>
            EditorLogIfTrue(!obj, type, message, logger);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AssertStatus BuildLogIfFalse(this bool obj, LogType type, string message, ILogger logger = null) =>
            BuildLogIfTrue(!obj, type, message, logger);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AssertStatus EditorLogIfTrue(this bool obj, LogType type, string message, ILogger logger = null) {
#if UNITY_EDITOR
            return LogIfTrue(obj, type, message, logger);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AssertStatus BuildLogIfTrue(this bool obj, LogType type, string message, ILogger logger = null)
        {
#if !UNITY_EDITOR
            return LogIfTrue(obj, type, message, logger);
#endif
            return AssertStatus.Pass;
        }
    }
}