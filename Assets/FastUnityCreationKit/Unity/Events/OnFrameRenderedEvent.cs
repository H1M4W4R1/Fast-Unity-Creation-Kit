using FastUnityCreationKit.Events;

namespace FastUnityCreationKit.Unity.Events
{
    /// <summary>
    /// Called when a frame is rendered and all <see cref="FastMonoBehaviour"/> scripts have been updated.
    /// </summary>
    public sealed class OnFrameRenderedEvent : GlobalEventChannel<OnFrameRenderedEvent>
    {
        
    }
}