using FastUnityCreationKit.Core.Events;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Events.Data
{
    /// <summary>
    /// Represents data sent in event indicating that a FastMonoBehaviour has been initialized.
    /// </summary>
    public struct FastMonoBehaviourInitializationEventData<TFastMonoBehaviour> : IEventChannelData
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        [NotNull] public TFastMonoBehaviour fastMonoBehaviour;
        
        public FastMonoBehaviourInitializationEventData([NotNull] TFastMonoBehaviour fastMonoBehaviour)
        {
            this.fastMonoBehaviour = fastMonoBehaviour;
        }
    }
}