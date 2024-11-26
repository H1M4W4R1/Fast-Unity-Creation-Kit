using System.Runtime.CompilerServices;

namespace FastUnityCreationKit.Guardian.Data
{
    /// <summary>
    /// Represents instance checking
    /// </summary>
    public readonly ref struct CheckedInstance<TInstanceType>
    {
        private readonly TInstanceType _instance;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CheckedInstance(TInstanceType instance)
        {
            _instance = instance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool IsNull() => _instance == null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool IsNotNull() => _instance != null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public CheckedType HasTypeThat() => new(_instance.GetType());
    }
}