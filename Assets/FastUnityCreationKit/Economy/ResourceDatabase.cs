using FastUnityCreationKit.Data;
using FastUnityCreationKit.Data.Containers;
using FastUnityCreationKit.Identification.Identifiers;
using FastUnityCreationKit.Utility.Attributes;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy
{
    [AutoCreatedObject(DatabaseConstants.DATABASE_DIRECTORY)]
    [AddressableGroup(DatabaseConstants.DATABASE_ADDRESSABLE_TAG)]   
    public sealed class ResourceDatabase : AddressableDatabase<ResourceDatabase, ResourceBase>
    {
        public ResourceDatabase()
        {
            addressableTag = LocalConstants.RESOURCE_ADDRESSABLE_TAG;
        }

        /// <summary>
        /// Get resource by identifier.
        /// </summary>
        /// <param name="identifier">Identifier of resource to get.</param>
        /// <returns>Resource with specified identifier or null if not found.</returns>
        [CanBeNull] public ResourceBase GetResource(Snowflake128 identifier)
        {
            for(int i = 0; i < Count; i++)
            {
                ResourceBase resource = this[i];
                if (!resource) continue;
                
                if (resource.Id == identifier)
                    return this[i];
            }
            
            return null;
        }
        
        /// <summary>
        /// Get resource of specified type.
        /// </summary>
        /// <typeparam name="TResource">Type of resource to get.</typeparam>
        /// <returns>Resource of specified type or null if not found.</returns>
        [CanBeNull] public TResource GetResource<TResource>() 
            where TResource : ResourceBase
        {
            for(int i = 0; i < Count; i++)
            {
                ResourceBase resource = this[i];
                if (!resource) continue;
                
                if (resource is TResource castedResource)
                    return castedResource;
            }
            
            return null;
        }
    }
}