using System;

namespace FastUnityCreationKit.Unity.Events.Unity
{
    [Flags] public enum CKGlobalEvents : uint
    {
        None = 0,
        ObjectCreatedEvent = 1 << 0,
        ObjectDestroyedEvent = 1 << 1,
        ObjectEnabledEvent = 1 << 2,
        ObjectDisabledEvent = 1 << 3,
        ObjectPreUpdateEvent = 1 << 4,
        ObjectUpdateEvent = 1 << 5,
        ObjectPostUpdateEvent = 1 << 6,
        ObjectFixedUpdateEvent = 1 << 7,
        ObjectExpiredEvent = 1 << 8,


        All = 0xFFFFFFFF
    }
}