using FastUnityCreationKit.Core.Events;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Events.Data
{
    /// <summary>
    /// Data that is sent when a FastMonoBehaviour is destroyed.
    /// </summary>
    public readonly struct FastMonoBehaviourDestroyedData<TFastCreationKitObject> : IEventChannelData 
        where TFastCreationKitObject : FastMonoBehaviour, new()
    {
        [NotNull] public readonly TFastCreationKitObject reference;

        public FastMonoBehaviourDestroyedData([NotNull] TFastCreationKitObject reference)
        {
            this.reference = reference;
        }

        public static implicit operator TFastCreationKitObject(
            FastMonoBehaviourDestroyedData<TFastCreationKitObject> data) => data.reference;

        public static implicit operator
            FastMonoBehaviourDestroyedData<TFastCreationKitObject>(TFastCreationKitObject reference) =>
            new FastMonoBehaviourDestroyedData<TFastCreationKitObject>(reference);
    }
}