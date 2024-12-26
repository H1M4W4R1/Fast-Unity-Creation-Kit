using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Context.Interface;
using FastUnityCreationKit.Identification.Identifiers;
using FastUnityCreationKit.Status.Abstract;
using FastUnityCreationKit.Status.Interfaces;
using FastUnityCreationKit.Utility.Limits;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Status.References
{
    /// <summary>
    /// This class represents a reference to a status that is applied to the target.
    /// </summary>
    public sealed class AppliedStatusReference
    {
        /// <summary>
        /// Pointer to status that is applied.
        /// </summary>
        internal readonly Snowflake128 statusIdentifier;

        /// <summary>
        /// Level of the status. Interpreted differently based on the status type.
        /// </summary>
        internal long statusLevel;
        
        /// <summary>
        /// Reference to status instance within the database.
        /// </summary>
        public StatusBase Status => StatusDatabase.Instance.GetStatusByIdentifier(statusIdentifier);

        public AppliedStatusReference(IContextWithTarget context, [NotNull] StatusBase status, long statusLevel = 0)
        {
            this.statusIdentifier = status.Id;
            this.statusLevel = statusLevel;
            
            // Notify that status was applied
            status.OnStatusApplied(context).GetAwaiter().GetResult();
        }

        private async UniTask CheckLimitsAndRaiseEvents<TStatusTarget>(IContextWithTarget<TStatusTarget> context)
        {
            // Acquire limit information from referenced status
            LimitHit limitHit = Status.EnsureLimitsFor(this);
            switch (limitHit)
            {
                // Check if max limit is reached
                case LimitHit.UpperLimitHit:
                    await Status.OnMaxLimitReached(context);
                    break;
                case LimitHit.LowerLimitHit:
                    await Status.OnMinLimitReached(context);
                    break;
                case LimitHit.None:
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        public async UniTask AddLevel<TStatusTarget>(IContextWithTarget<TStatusTarget> context, long stacks)
            where TStatusTarget : IStatusTarget => await ModifyLevel(context, stacks);

        public async UniTask TakeLevel<TStatusTarget>(IContextWithTarget<TStatusTarget> context, long stacks)
            where TStatusTarget : IStatusTarget => await ModifyLevel(context, -stacks);

        private async UniTask ModifyLevel<TStatusTarget>(IContextWithTarget<TStatusTarget> context, long stacks)
            where TStatusTarget : IStatusTarget
        {
            // Get current status level
            long previousStacks = statusLevel;

            // Add stacks to status level
            statusLevel += stacks;

            // Notify about stack changes if difference is not 0
            // otherwise if difference is zero there is no change, so notification is not needed.
            if (previousStacks != statusLevel)
                await Status.OnStatusLevelChanged(context, statusLevel - previousStacks);

            // Check if status level is 0, if so, remove status
            if (statusLevel == 0)
            {
                // Notify that status was removed only if it's level was changed
                // level change equal to zero means that status level was 0 earlier
                // and thus status was probably non-existent.
                if(previousStacks != statusLevel)
                    await Status.OnStatusRemoved(context);
                
                // Remove status reference from target
                context.Target.RemoveStatusReference(this);
                
                // We don't want to raise limit events if status is removed
                return;
            }
            
            // Check if any limit is reached during this transformation
            // if level was not changed, we don't need to check limits
            // as there was no change in status level, and it might raise
            // limit events multiple times.
            if(previousStacks != statusLevel)
                await CheckLimitsAndRaiseEvents(context);
        }

        // TODO: Status level based on status type, check if should be removed
    }
}