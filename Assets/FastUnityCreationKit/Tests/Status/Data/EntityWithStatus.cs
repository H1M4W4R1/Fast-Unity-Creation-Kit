using System.Collections.Generic;
using FastUnityCreationKit.Status;

namespace FastUnityCreationKit.Tests.Status.Data
{
    /// <summary>
    /// Represents an entity that has a status.
    /// </summary>
    public sealed class EntityWithStatus : IObjectWithStatus<RegularStatus>
    {
        public int statusPercentageReachedZeroTimes;
        
        /// <summary>
        /// Internal list of statuses.
        /// </summary>
        List<IStatus> IObjectWithStatus.Statuses { get; set; }
    }
}