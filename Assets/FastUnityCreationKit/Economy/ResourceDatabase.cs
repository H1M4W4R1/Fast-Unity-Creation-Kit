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
    }
}