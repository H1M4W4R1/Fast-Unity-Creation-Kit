using FastUnityCreationKit.Events;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Events.Data
{
    /// <summary>
    /// Data for generic FastMonoBehaviour events.
    /// </summary>
    public readonly struct FastMonoBehaviourEventData<TFastCreationKitObject> : IEventChannelData 
        where TFastCreationKitObject : FastMonoBehaviour, new()
    {
        [NotNull] public readonly TFastCreationKitObject reference;

        public FastMonoBehaviourEventData([NotNull] TFastCreationKitObject reference)
        {
            this.reference = reference;
        }

        public static implicit operator TFastCreationKitObject(
            FastMonoBehaviourEventData<TFastCreationKitObject> data) => data.reference;

        public static implicit operator
            FastMonoBehaviourEventData<TFastCreationKitObject>(TFastCreationKitObject reference) =>
            new FastMonoBehaviourEventData<TFastCreationKitObject>(reference);
    }
}