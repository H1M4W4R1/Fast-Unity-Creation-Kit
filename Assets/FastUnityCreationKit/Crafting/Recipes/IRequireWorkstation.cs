using FastUnityCreationKit.Crafting.Workstations;

namespace FastUnityCreationKit.Crafting.Recipes
{
    /// <summary>
    /// Indicates that the recipe requires a specific workstation to craft.
    /// If multiple workstations are required only one of required workstations needs to be present.
    /// </summary>
    public interface IRequireWorkstation<TWorkstation> : IRequireWorkstation
        where TWorkstation : ICraftingWorkstation
    {
        
    }
    
    public interface IRequireWorkstation
    {
        
    }
}