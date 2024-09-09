using System.Collections.Generic;
using FastUnityCreationKit.Crafting.Workstations;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Crafting.Data
{
    /// <summary>
    /// Represents the context of a crafting operation.
    /// </summary>
    public struct BasicCraftingContext : ICraftingContext<BasicCraftingContext>
    {
        /// <summary>
        /// Workstation used to craft the item.
        /// </summary>
        public readonly List<ICraftingWorkstation> workstationsNearby;

        /// <inheritdoc/>
        public List<ICraftingWorkstation> WorkstationsNearby => workstationsNearby;

        public BasicCraftingContext([CanBeNull] List<ICraftingWorkstation> workstationsNearby)
        {
            this.workstationsNearby = workstationsNearby;
        }
    }
}