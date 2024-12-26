using FastUnityCreationKit.Data.Containers;
using FastUnityCreationKit.Identification.Identifiers;
using FastUnityCreationKit.Status.Abstract;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Attributes;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Database for status.  
    /// </summary>
    [AutoCreatedObject(Directories.DATABASES_PATH)]
    [AddressableGroup(Directories.DATABASES_PATH, Directories.DATABASES_PATH)]
    public sealed class StatusDatabase : AddressableDatabase<StatusDatabase, StatusBase>
    {
        public StatusDatabase()
        {
            addressableTag = Directories.STATUS_PATH;
        }

        /// <summary>
        /// Get status by type.
        /// </summary>
        /// <typeparam name="TStatusType">Type of the status.</typeparam>
        /// <returns>Status with the type or null if not found.</returns>
        public TStatusType GetStatus<TStatusType>() where TStatusType : StatusBase
        {
            for(int i = 0; i < internalContainer.All.Count; i++)
            {
                if(internalContainer.All[i] is TStatusType status)
                    return status;
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
            for(int i = 0; i < internalContainer.All.Count; i++)
            {
                StatusBase status = internalContainer.All[i];
                
                if(status.Id.Equals(identifier))
                    return status;
            }
            
            return null;
        }
    }
}