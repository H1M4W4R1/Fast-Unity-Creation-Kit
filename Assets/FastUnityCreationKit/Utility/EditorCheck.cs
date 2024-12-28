using System.Runtime.CompilerServices;
using UnityEngine;

namespace FastUnityCreationKit.Utility
{
    public readonly ref struct EditorCheck
    {
        private readonly bool _source;

        private EditorCheck(bool source)
        {
            _source = source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EditorCheck Perform(bool source)
        {
#if UNITY_EDITOR
            return new EditorCheck(source);
#else
            return false;
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool WithError(string message)
        {
#if UNITY_EDITOR
            if (_source) Debug.LogError(message);
            return _source;
#endif
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool WithWarning(string message)
        {
#if UNITY_EDITOR
            if (_source) Debug.LogWarning(message);
            return _source;
#endif
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool WithMessage(string message)
        {
#if UNITY_EDITOR
            if (_source) Debug.Log(message);
            return _source;
#endif
            return false;
        }
    }
}