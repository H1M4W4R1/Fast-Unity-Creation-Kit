using FastUnityCreationKit.Status;

namespace FastUnityCreationKit.Tests.Status.Data
{
    public sealed class NotSupportedStatusMockup : IStatus
    {
        public void OnStatusAdded(IObjectWithStatus objectWithStatus)
        {
            
        }

        public void OnStatusRemoved(IObjectWithStatus objectWithStatus)
        {
     
        }
    }
}