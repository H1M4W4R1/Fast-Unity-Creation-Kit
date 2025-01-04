using FastUnityCreationKit.Annotations.Unity;
using FastUnityCreationKit.Data;
using FastUnityCreationKit.Data.Containers;
using FastUnityCreationKit.Identification.Identifiers;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Economy
{
    [AutoCreatedObject(DatabaseConstants.DATABASE_DIRECTORY)]
    public sealed class ResourceDatabase : AddressableDatabase<ResourceDatabase, ResourceBase>
    {
        public ResourceDatabase()
        {
            addressableTags.Add(LocalConstants.RESOURCE_ADDRESSABLE_TAG);
        }

        /// <summary>
        ///     Get resource by identifier.
        /// </summary>
        /// <param name="identifier">Identifier of resource to get.</param>
        /// <returns>Resource with specified identifier or null if not found.</returns>
        [CanBeNull] public ResourceBase GetResource(Snowflake128 identifier)
        {
            EnsurePreloaded();

            for (int i = 0; i < PreloadedCount; i++)
            {
                ResourceBase resource = GetElementAt(i);
                if (!resource) continue;

                // If resource has the same identifier
                if (resource.Id == identifier) return resource;
            }

            return null;
        }

        /// <summary>
        ///     Get resource of specified type.
        /// </summary>
        /// <typeparam name="TResource">Type of resource to get.</typeparam>
        /// <returns>Resource of specified type or null if not found.</returns>
        [CanBeNull] public TResource GetResource<TResource>()
            where TResource : ResourceBase, new()
        {
            EnsurePreloaded();

            for (int i = 0; i < PreloadedCount; i++)
            {
                ResourceBase resource = GetElementAt(i);
                if (!resource) continue;

                if (resource is TResource castedResource) return castedResource;
            }

            return null;
        }
    }
}