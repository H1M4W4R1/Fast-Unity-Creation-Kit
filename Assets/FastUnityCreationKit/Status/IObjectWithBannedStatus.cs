using JetBrains.Annotations;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Represents an object that has a banned status.
    /// </summary>
    public interface IObjectWithBannedStatus<[UsedImplicitly] TStatus> : IObjectWithStatus
        where TStatus : IStatus
    {
        
    }
}