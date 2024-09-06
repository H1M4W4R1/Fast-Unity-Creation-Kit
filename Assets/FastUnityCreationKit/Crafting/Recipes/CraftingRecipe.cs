using System.Collections;
using System.Collections.Generic;
using FastUnityCreationKit.Core.Utility.Initialization;
using FastUnityCreationKit.Core.Utility.Properties;
using FastUnityCreationKit.Crafting.Data;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Crafting.Recipes
{
    /// <summary>
    /// Represents a crafting recipe used to create items. Recipes can be locked using
    /// lockable object framework, for reference see <see cref="ILockable"/>. This allows to
    /// lock recipes based on certain conditions.
    /// </summary>
    public abstract class CraftingRecipe : ILockable, IInitializable
    {
        /// <summary>
        /// If true, the recipe is locked by default.
        /// </summary>
        public abstract bool IsLockedByDefault { get; }

        /// <summary>
        /// Checks if the recipe is craftable.
        /// </summary>
        public bool IsCraftable([NotNull] ICraftingContext craftingContext)
        {
            // Ensure the recipe is initialized.
            IInitializable initializable = this;
            initializable.EnsureInitialized();

            ILockable lockable = this;

            // Check if the recipe is locked.
            if (lockable.IsLocked) return false;

            // Check if the recipe is available to craft.
            if (!AvailableToCraft(craftingContext)) return false;

            // Check if the recipe is available to craft, additional FastUnityCreationKit logic.
            if (!_AvailableToCraft(craftingContext)) return false;

            // Check if the recipe is available to craft.
            return HasIngredients(craftingContext);
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
        protected virtual bool AvailableToCraft([NotNull] ICraftingContext craftingContext) => true;

        /// <summary>
        /// Internal logic used to determine if the recipe can be crafted.
        /// This method is used to check additional conditions before crafting the item while
        /// implementing custom recipe logic for abstract classes that inherit from this class and
        /// are implemented within FastUnityCreationKit.
        /// </summary>
        internal virtual bool _AvailableToCraft([NotNull] ICraftingContext craftingContext) => true;

        /// <summary>
        /// Checks if the recipe has the required ingredients.
        /// Implement your ingredient logic here.
        /// </summary>
        protected abstract bool HasIngredients([NotNull] ICraftingContext craftingContext);

        /// <summary>
        /// Executes the crafting logic. This method should take care of removing the ingredients and adding the crafted item.
        /// This is a coroutine method that can be used to craft items over multiple frames allowing to
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
        protected abstract IEnumerator OnCraft([NotNull] ICraftingContext craftingContext);

        /// <summary>
        /// Crafts the item, returns true if the item was crafted successfully, false otherwise.
        /// </summary>
        /// TODO: Check if crafting context is being passed correctly and result is being set correctly.
        public IEnumerator TryCraftingItem([NotNull] ICraftingContext craftingContext)
        {
            // Check if the recipe is craftable.
            if (!IsCraftable(craftingContext))
            {
                craftingContext.WasSuccessful = false;
                craftingContext.WasCompleted = true;
                yield break;
            }

            // Craft the item - wait for the coroutine to finish.
            yield return OnCraft(craftingContext);
            
            // Set the crafting context as successful and complete.
            craftingContext.WasSuccessful = true;
            craftingContext.WasCompleted = true;
        }
    }
}