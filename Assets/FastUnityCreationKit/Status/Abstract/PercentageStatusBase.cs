using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Context.Interface;
using FastUnityCreationKit.Status.Interfaces;

namespace FastUnityCreationKit.Status.Abstract
{
    /// <summary>
    /// Represents status that is represented by percentage.
    /// Can be used for statuses like slowness, speed boost etc.
    /// </summary>
    public abstract class PercentageStatusBase<TStatusTarget> : StatusBase<TStatusTarget>, IPercentageStatus
    {
        /// <summary>
        /// Get status percentage from status level.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetPercentageFrom(long statusLevel) => statusLevel / (float) IPercentageStatus.FULL_STACK;

        /// <summary>
        /// Calculate status level from percentage (aka. count of full stacks).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetStackLevelFrom(long statusLevel) => (int) statusLevel / IPercentageStatus.FULL_STACK;
        
        /// <summary>
        /// Called when percentage of the status is changed.
        /// </summary>
        /// <param name="context">Context of the status change.</param>
        /// <param name="percentageDifference">Positive if increased, negative if decreased.</param>
        public virtual async UniTask OnPercentageChanged(IContextWithTarget<TStatusTarget> context,
            float percentageDifference) =>
            await UniTask.CompletedTask;
        
        /// <summary>
        /// Calculate maximum limit for the status.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float CalculateMaxLimitFor(int stacks, float percentage) =>
            stacks * IPercentageStatus.FULL_STACK + percentage * IPercentageStatus.FULL_STACK;
        
        /// <summary>
        /// Calculate minimum limit for the status.
        /// This is inverted value of the maximum limit.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CalculateMinLimitFor(int stacks, float percentage) =>
            -1 * CalculateMaxLimitFor(stacks, percentage);
    }
}