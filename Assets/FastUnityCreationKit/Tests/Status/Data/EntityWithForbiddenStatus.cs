using System.Collections.Generic;
using FastUnityCreationKit.Status;

namespace FastUnityCreationKit.Tests.Status.Data
{
    /// <summary>
    /// Represents an entity that has a forbidden status.
    /// </summary>
    public class EntityWithForbiddenStatus : IObjectWithBannedStatus<RegularStatus>,
        IObjectWithStatus<StackableStatus>, IObjectWithBannedStatus<StackableStatus>
    {
        List<IStatus> IObjectWithStatus.Statuses { get; set; }
    }
}