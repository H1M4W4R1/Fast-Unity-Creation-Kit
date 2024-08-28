namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Represents a status that can be stacked.
    /// Allows to stack multiple instances of the same status on an object.
    /// It can go either positive or negative way to support both buffs and debuffs
    /// using a single class.
    /// </summary>
    public interface IStackableStatus : IStatus
    {
        /// <summary>
        /// Number of times this status is stacked on an object.
        /// </summary>
        public int StackCount { get; protected set; }
        
        /// <summary>
        /// Increases the stack count by the given amount.
        /// </summary>
        public void IncreaseStackCount(int amount = 1) => ChangeStackCount(amount);
        
        /// <summary>
        /// Decreases the stack count by the given amount.
        /// </summary>
        public void DecreaseStackCount(int amount = 1) => ChangeStackCount(-amount);
        
        /// <summary>
        /// Changes the stack count by the given amount.
        /// You need to call increase/decrease stack count methods instead of this method
        /// events should be manually added after calling method wrappers.
        /// </summary>
        private void ChangeStackCount(int amount)
        {
            StackCount += amount;
        }
    }
}