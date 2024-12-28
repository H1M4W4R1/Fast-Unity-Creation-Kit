using System;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Identification.Identifiers;
using FastUnityCreationKit.Status.Abstract;
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

        public AppliedStatusReference(EntityStatusComponent context, [NotNull] StatusBase status, long statusLevel = 0)
        {
            this.statusIdentifier = status.Id;
            this.statusLevel = statusLevel;
            
            // Notify that status was applied
            status.OnStatusApplied(context).GetAwaiter().GetResult();
        }

        private async UniTask CheckLimitsAndRaiseEvents(EntityStatusComponent context)
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

        public async UniTask AddLevel(EntityStatusComponent context, long stacks)
             => await ModifyLevel(context, stacks);

        public async UniTask TakeLevel(EntityStatusComponent context, long stacks)
            => await ModifyLevel(context, -stacks);

        private async UniTask ModifyLevel(EntityStatusComponent context, long stacks)
        {
            // Get current status level
            long previousStacks = statusLevel;

            // Add stacks to status level
            statusLevel += stacks;

            // Notify about stack changes if difference is not 0
            // otherwise if difference is zero there is no change, so notification is not needed.
            // also we need to check limits if status went negative and min limit is zero.
            if (previousStacks != statusLevel)
            {
                await Status.OnStatusLevelChanged(context, statusLevel - previousStacks);
                await CheckLimitsAndRaiseEvents(context);
            }

            // Check if status level is 0, if so, remove status
            if (statusLevel == 0)
            {
                // Notify that status was removed only if it's level was changed
                // level change equal to zero means that status level was 0 earlier
                // and thus status was probably non-existent.
                if(previousStacks != statusLevel)
                    await Status.OnStatusRemoved(context);
                
                // Remove status reference from target
                context.DeleteStatusReference(this);
            }
        }
    }
}