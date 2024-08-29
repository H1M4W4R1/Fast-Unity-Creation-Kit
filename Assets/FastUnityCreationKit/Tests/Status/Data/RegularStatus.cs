using FastUnityCreationKit.Status;

namespace FastUnityCreationKit.Tests.Status.Data
{
    /// <summary>
    /// Represents a regular status that does not have any additional properties.
    /// </summary>
    public sealed class RegularStatus : IStatus
    {
        public bool wasStatusAdded;
        public bool wasStatusRemoved;
        
        public void OnStatusAdded(IObjectWithStatus objectWithStatus)
        {
            wasStatusAdded = true;
        }

        public void OnStatusRemoved(IObjectWithStatus objectWithStatus)
        {
            wasStatusRemoved = true;
        }
    }
}