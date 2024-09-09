using System.Collections.Generic;
using FastUnityCreationKit.Crafting.Workstations;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Crafting.Data
{
    /// <summary>
    /// Represents the context of a crafting operation.
    /// </summary>
    public readonly struct BasicCraftingContext : ICraftingContext<BasicCraftingContext>
    {
        /// <summary>
        /// Workstation used to craft the item.
        /// </summary>
        private readonly List<ICraftingWorkstation> _workstationsNearby;

        /// <inheritdoc/>
        public List<ICraftingWorkstation> WorkstationsNearby => _workstationsNearby;

        public BasicCraftingContext([CanBeNull] List<ICraftingWorkstation> workstationsNearby)
        {
            _workstationsNearby = workstationsNearby;
        }
    }
}