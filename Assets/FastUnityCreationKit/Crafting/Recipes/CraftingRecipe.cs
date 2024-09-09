using System;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Core.Utility.Initialization;
using FastUnityCreationKit.Core.Utility.Properties;
using FastUnityCreationKit.Crafting.Data;
using FastUnityCreationKit.Crafting.Workstations;
using JetBrains.Annotations;
using Sirenix.Utilities;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace FastUnityCreationKit.Crafting.Recipes
{
    /// <summary>
    /// Represents a crafting recipe used to create items. Recipes can be locked using
    /// lockable object framework, for reference see <see cref="ILockable"/>. This allows to
    /// lock recipes based on certain conditions.
    /// <br/>
    /// <br/>
    /// Crafting system uses UniTask to handle async operations - this allows for easy
    /// implementation of crafting operations that take multiple frames to complete and
    /// improves performance of the system.
    /// </summary>
    public abstract class CraftingRecipe : ILockable, IInitializable
    {
        /// <summary>
        /// If true, the recipe is locked by default.
        /// </summary>
        public abstract bool IsLockedByDefault { get; }

        /// <summary>
        /// Internal stash for required workstations.
        /// Used to check if the recipe has required workstations.
        /// Automatically populated by the recipe system.
        /// </summary>
        private Type[] _requiredWorkstations;

        /// <summary>
        /// Checks if the recipe is craftable.
        /// </summary>
        public async UniTask<bool> IsCraftable([NotNull] ICraftingContext craftingContext)
        {
            // Ensure the recipe is initialized.
            IInitializable initializable = this;
            initializable.EnsureInitialized();

            // Get lockable object.
            ILockable lockable = this;

            // Check if the recipe is locked.
            if (lockable.IsLocked) return false;

            // Check if the recipe is available to craft.
            if (!await AvailableToCraft(craftingContext)) return false;

            // Check if the recipe has required workstations.
            if (!CheckWorkstationsAvailableInContext(craftingContext)) return false;

            // Check if the recipe is available to craft.
            return await HasIngredients(craftingContext);
        }

        bool ILockable.IsLocked { get; set; }
        bool IInitializable.InternalInitializationStatusStorage { get; set; }

        void IInitializable.OnInitialize()
        {
            // Check if the recipe is locked by default.
            if (IsLockedByDefault)
            {
                ILockable lockable = this;
                lockable.Lock();
            }
        }

        /// <summary>
        /// Checks if the recipe can be crafted.
        /// Used internally, do not call this method directly <see cref="IsCraftable"/>.
        /// Defaults to true, this can be used to implement custom logic to determine if the recipe can be crafted.
        /// </summary>
        protected virtual async UniTask<bool> AvailableToCraft([NotNull] ICraftingContext craftingContext) => true;

        /// <summary>
        /// Checks if the recipe has the required ingredients.
        /// Implement your ingredient logic here.
        /// </summary>
        protected virtual async UniTask<bool> HasIngredients([NotNull] ICraftingContext craftingContext) => true;

        /// <summary>
        /// Executes the crafting logic. This method should take care of removing the ingredients and adding the crafted item.
        /// This is a async method that can be used to craft items over multiple frames allowing to
        /// implement automatic crafting machines that craft items over time (see Minecraft furnace as an example).
        /// </summary>
        /// <remarks>
        /// To implement an example of Minecraft Furnace you need to take the following steps:
        /// <ol>
        /// <li>Take the input fuel and consume it, add the fuel time to a timer [only if fuel time is greater than 0].</li> 
        /// <li>Start the crafting process, consume the fuel time and add it to production timer.</li> 
        /// <li>If production timer is full, take the input item and produce the output item and add it to the output slot.</li> 
        /// <li>Reset the production timer and start the process again.</li>
        /// </ol>
        /// <i>Note: Of course you need to check if the input time is not zero to prevent fuel running out mid-crafting.</i>
        /// </remarks>
        /// <returns>
        /// True if crafting is successful, false if it failed.
        /// </returns>
        protected virtual async UniTask<bool> OnCraft([NotNull] ICraftingContext craftingContext) => true;

        /// <summary>
        /// Crafts the item, returns true if the item was crafted successfully, false otherwise.
        /// </summary>
        public async UniTask<bool> TryCraftingItem([NotNull] ICraftingContext craftingContext)
        {
            // Check if the recipe is craftable.
            if (!await IsCraftable(craftingContext))
                return false;

            // Craft the item - wait for the coroutine to finish.
            return await OnCraft(craftingContext);
        }

        /// <summary>
        /// Checks if all required workstations are available in the context.
        /// </summary>
        private unsafe bool CheckWorkstationsAvailableInContext([NotNull] ICraftingContext craftingContext)
        {
            // Check if this requires a workstation.
            if (this is not IRequireWorkstation) return true;

            // Get current type
            Type type = GetType();

            // Get all IRequireWorkstation interfaces.
            // We cache required workstations to avoid reflection overhead on each check.
            if (_requiredWorkstations == null) _requiredWorkstations = type.GetInterfaces();

            // Fast stack memory for results.
            // This is a fast allocation that should reduce GC overhead.
            bool* requiredWorkstationsAreNearby = stackalloc bool[_requiredWorkstations.Length];

            // Loop through all workstations nearby.
            for (int index = 0; index < craftingContext.WorkstationsNearby.Count; index++)
            {
                // Check if the workstation is the required workstation.
                ICraftingWorkstation workstation = craftingContext.WorkstationsNearby[index];

                // Get workstation type
                Type workstationType = workstation.GetType();

                // Loop through all IRequireWorkstation interfaces.
                for (int i = 0; i < _requiredWorkstations.Length; i++)
                {
                    // Check if the workstation is the required workstation.
                    Type requireableInterface = _requiredWorkstations[i];

                    // Check if the interface is IRequireWorkstation
                    if (!requireableInterface.IsGenericType ||
                        requireableInterface.GetGenericTypeDefinition() != typeof(IRequireWorkstation<>)) continue;

                    // Get the workstation type
                    Type requiredWorkstationType = requireableInterface.GetGenericArguments()[0];

                    // Check if the workstation is the required workstation.
                    if (!workstationType.ImplementsOrInherits(requiredWorkstationType)) continue;

                    // Set the result as true, we don't break as we can have multi-requirement
                    // for the same workstation or workstation may met multiple requirements.
                    requiredWorkstationsAreNearby[i] = true;
                }
            }

            bool result = true;

            // Check if all requirements are met.
            for (int i = 0; i < _requiredWorkstations.Length; i++)
            {
                // Skip if the requirement is met.
                if (requiredWorkstationsAreNearby[i]) continue;

                // Set the result as false otherwise.
                result = false;
                break;
            }

            // Free memory
            Marshal.FreeHGlobal((IntPtr) requiredWorkstationsAreNearby);

            // Return check result
            return result;
        }
    }
}