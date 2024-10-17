using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Status.Context;
using JetBrains.Annotations;
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
        public UniTask OnMaxPercentageReachedAsync([NotNull] IStatusContext context);
        
        /// <summary>
        /// Triggered when the percentage value reaches 0%.
        /// </summary>
        public UniTask OnMinPercentageReachedAsync([NotNull] IStatusContext context);
        
        /// <summary>
        /// Increases the percentage value by the given amount where 1.0 is 100%.
        /// </summary>
        public void IncreasePercentage([NotNull] IStatusContext context, float amount) =>
            ChangePercentage(context, amount).GetAwaiter().GetResult();
        
        /// <summary>
        /// Increases the percentage value by the given amount where 1.0 is 100%.
        /// </summary>
        public async UniTask IncreasePercentageAsync([NotNull] IStatusContext context, float amount) =>
            await ChangePercentage(context, amount);

        /// <summary>
        /// Decreases the percentage value by the given amount where 1.0 is 100%.
        /// </summary>
        public void DecreasePercentage([NotNull] IStatusContext context, float amount) =>
            ChangePercentage(context, -amount).GetAwaiter().GetResult();
        
        /// <summary>
        /// Decreases the percentage value by the given amount where 1.0 is 100%.
        /// </summary>
        public async UniTask DecreasePercentageAsync([NotNull] IStatusContext context, float amount) => 
            await ChangePercentage(context, -amount);
        
        /// <summary>
        /// Increases the percentage value by the given amount.
        /// </summary>
        private async UniTask ChangePercentage([NotNull] IStatusContext context, float amount)
        {
            float previousPercentage = Percentage;
            
            // Increase the percentage value by the given amount.
            Percentage += amount;

            // Trigger events based on the percentage value.
            // Warning: this may trigger multiple times if the percentage is decreased
            // to minimum value and status is instantly removed from the object.
            switch (Percentage)
            {
                case >= 1f when previousPercentage < 1f:
                    await OnMaxPercentageReachedAsync(context);
                    break;
                case <= 0f when previousPercentage > 0f:
                    await OnMinPercentageReachedAsync(context);
                    break;
            }
            
            // Clamp the percentage value between 0 and 1 to prevent graphical issues.
            Percentage = math.clamp(Percentage, 0f, 1f);
        }

        /// <summary>
        /// Sets the percentage value to the given value.
        /// </summary>
        internal async UniTask SetPercentageAsync([NotNull] IStatusContext context, float amount)
        {
            float previousPercentage = Percentage;
            Percentage = amount;
            
            if(previousPercentage < amount && amount >= 1f)
                await OnMaxPercentageReachedAsync(context);
            else if(previousPercentage > amount && amount <= 0f)
                await OnMinPercentageReachedAsync(context);
            
        }
    }
}