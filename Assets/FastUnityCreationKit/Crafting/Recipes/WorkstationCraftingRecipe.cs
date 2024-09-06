using FastUnityCreationKit.Crafting.Data;
using FastUnityCreationKit.Crafting.Workstations;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Crafting.Recipes
{
    /// <summary>
    /// Represents a crafting recipe used to create items.
    /// This recipe requires a specific workstation to craft.
    /// It only supports one workstation type, for multiple workstation types see <see cref="MultiWorkstationCraftingRecipe"/>.
    /// </summary>
    public abstract class WorkstationCraftingRecipe<[UsedImplicitly] TWorkstation> : CraftingRecipe,
        IRequireWorkstation<TWorkstation>
        where TWorkstation : ICraftingWorkstation
    {
        /// <inheritdoc/>
        internal override bool _AvailableToCraft(ICraftingContext craftingContext)
        {
            // Loop through all workstations nearby.
            for (int index = 0; index < craftingContext.WorkstationsNearby.Count; index++)
            {
                // Check if the workstation is the required workstation.
                ICraftingWorkstation workstation = craftingContext.WorkstationsNearby[index];
                if (workstation is TWorkstation) return true;
            }
            
            // The required workstation is not nearby.
            return false;
        }
        
    }
}