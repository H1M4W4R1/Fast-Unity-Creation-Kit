using System;
using System.Runtime.InteropServices;
using FastUnityCreationKit.Crafting.Data;
using FastUnityCreationKit.Crafting.Workstations;
using JetBrains.Annotations;
using Sirenix.Utilities;

namespace FastUnityCreationKit.Crafting.Recipes
{
    /// <summary>
    /// Represents a crafting recipe used to create items.
    /// This recipe requires a specific workstations to craft.1
    /// </summary>
    /// <remarks>
    /// Beware that this uses reflection to check for required workstations and thus
    /// may be slower than using <see cref="WorkstationCraftingRecipe{TWorkstation}"/>.
    /// Also, it renders it incompatible with some platforms.
    /// </remarks>
    public abstract class MultiWorkstationCraftingRecipe : CraftingRecipe
    {
        /// <inheritdoc/>
        internal override unsafe bool _AvailableToCraft(ICraftingContext craftingContext)
        {
            // Get current type
            Type type = GetType();
            
            // Get all IRequireWorkstation interfaces.
            Type[] requireableInterfaces = type.GetInterfaces();
            
            // Fast stack memory for results.
            bool* requireableInterfacesResults = stackalloc bool[requireableInterfaces.Length];
            
            // Loop through all workstations nearby.
            for (int index = 0; index < craftingContext.WorkstationsNearby.Count; index++)
            {
                // Check if the workstation is the required workstation.
                ICraftingWorkstation workstation = craftingContext.WorkstationsNearby[index];
                
                // Get workstation type
                Type workstationType = workstation.GetType();
                
                // Loop through all IRequireWorkstation interfaces.
                for (int i = 0; i < requireableInterfaces.Length; i++)
                {
                    // Check if the workstation is the required workstation.
                    Type requireableInterface = requireableInterfaces[i];
                    
                    // Check if the interface is IRequireWorkstation
                    if (!requireableInterface.IsGenericType ||
                        requireableInterface.GetGenericTypeDefinition() != typeof(IRequireWorkstation<>)) continue;
                    
                    // Get the workstation type
                    Type requiredWorkstationType = requireableInterface.GetGenericArguments()[0];
                        
                    // Check if the workstation is the required workstation.
                    if (!workstationType.ImplementsOrInherits(requiredWorkstationType)) continue;
                        
                    // Set the result as true, we don't break as we can have multi-requirement
                    // for the same workstation or workstation may met multiple requirements.
                    requireableInterfacesResults[i] = true;
                }
            }
            
            bool result = true;
            
            // Check if all requirements are met.
            for (int i = 0; i < requireableInterfaces.Length; i++)
            {
                // Skip if the requirement is met.
                if (requireableInterfacesResults[i]) continue;
                
                // Set the result as false otherwise.
                result = false;
                break;
            }
            
            // Free memory
            Marshal.FreeHGlobal((IntPtr) requireableInterfacesResults);
            
            // Return check result
            return result;
        }
        
    }
}