using FastUnityCreationKit.Core.Events;
using JetBrains.Annotations;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity.Events.Data
{
    public struct FastMonoBehaviourPointerEventData<TFastMonoBehaviour> : IEventChannelData
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        [CanBeNull] public PointerEventData pointerEventData;
        [NotNull] public TFastMonoBehaviour fastMonoBehaviour;
        
        public FastMonoBehaviourPointerEventData([CanBeNull] PointerEventData pointerEventData, [NotNull] TFastMonoBehaviour fastMonoBehaviour)
        {
            this.pointerEventData = pointerEventData;
            this.fastMonoBehaviour = fastMonoBehaviour;
        }
    }
}