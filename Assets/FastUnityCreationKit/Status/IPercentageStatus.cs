using Unity.Mathematics;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Represents a status that has a percentage value.
    /// This can be used to represent any status that has a percentage value and can trigger events based on that value.
    /// </summary>
    public interface IPercentageStatus : IStatus
    {
        /// <summary>
        /// Percentage value of the status.
        /// </summary>
        public float Percentage { get; protected set; }

        /// <summary>
        /// Triggered when the percentage value reaches 100%.
        /// </summary>
        public void OnMaxPercentageReached();
        
        /// <summary>
        /// Triggered when the percentage value reaches 0%.
        /// </summary>
        public void OnMinPercentageReached();
        
        /// <summary>
        /// Increases the percentage value by the given amount where 1.0 is 100%.
        /// </summary>
        public void IncreasePercentage(float amount) => ChangePercentage(amount);
        
        /// <summary>
        /// Decreases the percentage value by the given amount where 1.0 is 100%.
        /// </summary>
        public void DecreasePercentage(float amount) => ChangePercentage(-amount);
        
        /// <summary>
        /// Increases the percentage value by the given amount.
        /// </summary>
        private void ChangePercentage(float amount)
        {
            float previousPercentage = Percentage;
            
            // Increase the percentage value by the given amount.
            Percentage += amount;
            
            // Trigger events based on the percentage value.
            if (Percentage >= 100f && previousPercentage < 100f)
                OnMaxPercentageReached();
            else if (Percentage <= 0f && previousPercentage > 0f)
                OnMinPercentageReached();
            
            // Clamp the percentage value between 0 and 100 to prevent graphical issues.
            Percentage = math.clamp(Percentage, 0f, 100f);
        }
        
    }
}