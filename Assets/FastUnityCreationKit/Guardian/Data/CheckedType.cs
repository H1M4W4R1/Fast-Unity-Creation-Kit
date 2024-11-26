using System;
using System.Runtime.CompilerServices;
using FastUnityCreationKit.Guardian.Logging;
using UnityEngine;

namespace FastUnityCreationKit.Guardian.Data
{
    /// <summary>
    /// Represents type checking
    /// </summary>
    public readonly ref struct CheckedType
    {
        private readonly Type _type;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CheckedType(Type type)
        {
            _type = type;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsExactly<TType>() => _type == typeof(TType);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Is<TType>() => _type is TType;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNot<TType>() => _type is not TType;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsAssignableTo<TType>() => typeof(TType).IsAssignableFrom(_type);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsExactly(Type type) => _type == type;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsAssignableFrom<TType>() => _type.IsAssignableFrom(typeof(TType));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsAssignableTo(Type type) => type.IsAssignableFrom(_type);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsAssignableFrom(Type type) => _type.IsAssignableFrom(type);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Assert(LogType type, string message, ILogger logger = null)
            => new CheckLog(type, message, logger).Assert();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AssertInEditor(LogType type, string message, ILogger logger = null)
        {
#if UNITY_EDITOR
            Assert(type, message, logger);
#endif
        }
    }
}