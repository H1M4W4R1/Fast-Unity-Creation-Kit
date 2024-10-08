﻿using FastUnityCreationKit.Core.Events;
using FastUnityCreationKit.Unity.Events.Data;

namespace FastUnityCreationKit.Unity.Events
{
    /// <summary>
    /// Represents an event that is triggered when an object is clicked.
    /// </summary>
    public sealed class OnObjectClickedEvent<TFastMonoBehaviour> : GlobalEventChannel<OnObjectClickedEvent<TFastMonoBehaviour>, FastMonoBehaviourPointerEventData<TFastMonoBehaviour>>
        where TFastMonoBehaviour : FastMonoBehaviour<TFastMonoBehaviour>, new()
    {
        
    }
}