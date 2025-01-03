using FastUnityCreationKit.Annotations.Unity;
using FastUnityCreationKit.Data;
using FastUnityCreationKit.Data.Containers;
using FastUnityCreationKit.Identification.Identifiers;
using FastUnityCreationKit.Status.Abstract;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Database for status.  
    /// </summary>
    [AutoCreatedObject(DatabaseConstants.DATABASE_DIRECTORY)]
    public sealed class StatusDatabase : AddressableDatabase<StatusDatabase, StatusBase>
    {
        public StatusDatabase()
        {
            addressableTags.Add(LocalConstants.STATUS_ADDRESSABLE_TAG);
        }

        /// <summary>
        /// Get status by type.
        /// </summary>
        /// <typeparam name="TStatusType">Type of the status.</typeparam>
        /// <returns>Status with the type or null if not found.</returns> 
        public TStatusType GetStatus<TStatusType>() where TStatusType : StatusBase
        {
            EnsurePreloaded();
            
            for(int i = 0; i < PreloadedCount; i++)
            {
                StatusBase status = GetElementAt(i);
                
                if (status is TStatusType castedStatus)
                    return castedStatus;
            }
            
            return null;
        }
        
        /// <summary>
        /// Get status by identifier.
        /// </summary>
        /// <param name="identifier">Identifier of the status.</param>
        /// <returns>Status with the identifier or null if not found.</returns>
        public StatusBase GetStatusByIdentifier(Snowflake128 identifier)
        {
            EnsurePreloaded();
            
            for(int i = 0; i < PreloadedCount; i++)
            {
                StatusBase status = GetElementAt(i);
                if (!status) continue;
                
                if(status.Id.Equals(identifier))
                    return status;
            }
            
            return null;
        }
    }
}