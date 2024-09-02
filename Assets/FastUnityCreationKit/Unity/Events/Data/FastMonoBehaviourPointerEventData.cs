using FastUnityCreationKit.Core.Events;
using JetBrains.Annotations;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity.Events.Data
{
    public readonly struct FastMonoBehaviourPointerEventData<TFastMonoBehaviour> : IEventChannelData
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        [CanBeNull] public readonly PointerEventData pointerEventData;
        [NotNull] public readonly TFastMonoBehaviour fastMonoBehaviour;
        
        public FastMonoBehaviourPointerEventData([CanBeNull] PointerEventData pointerEventData, [NotNull] TFastMonoBehaviour fastMonoBehaviour)
        {
            this.pointerEventData = pointerEventData;
            this.fastMonoBehaviour = fastMonoBehaviour;
        }
    }
}