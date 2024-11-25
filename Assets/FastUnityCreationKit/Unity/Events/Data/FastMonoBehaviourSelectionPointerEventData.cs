using FastUnityCreationKit.Core.Events;
using JetBrains.Annotations;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity.Events.Data
{
    public readonly struct FastMonoBehaviourSelectionPointerEventData<TFastMonoBehaviour> : IEventChannelData
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        [CanBeNull] public readonly PointerEventData pointerEventData;
        [NotNull] public readonly TFastMonoBehaviour fastMonoBehaviour;
        public readonly bool isSelected;
        
        public FastMonoBehaviourSelectionPointerEventData([CanBeNull] PointerEventData pointerEventData, [NotNull] TFastMonoBehaviour fastMonoBehaviour,
            bool isSelected)
        {
            this.pointerEventData = pointerEventData;
            this.fastMonoBehaviour = fastMonoBehaviour;
            this.isSelected = isSelected;
        }
    }
}