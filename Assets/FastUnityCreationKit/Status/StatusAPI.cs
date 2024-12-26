using FastUnityCreationKit.Context.Interface;
using FastUnityCreationKit.Status.Abstract;
using FastUnityCreationKit.Status.Interfaces;
using FastUnityCreationKit.Status.References;
using UnityEngine;

namespace FastUnityCreationKit.Status
{
    public static class StatusAPI
    {
        public static async void AddStatus<TStatusType, TStatusTarget>(IContextWithTarget<TStatusTarget> context,
            long level = 1)
            where TStatusType : StatusBase<TStatusTarget>
            where TStatusTarget : IStatusTarget
        {
            // Nothing will be changed, we can skip if stacks are 0
            if (level == 0) return;
            
            // Get status from the database
            TStatusType status = StatusDatabase.Instance.GetStatus<TStatusType>();
            if (status == null)
            {
                Debug.LogError($"Status of type {typeof(TStatusType).Name} not found in the database. Probably" +
                               $"database was not repopulated correctly.");
                return;
            }

            AppliedStatusReference reference =
                context.Target.GetOrCreateStatusReference(context, status);

            await reference.AddLevel(context, level);
        }

        public static void IncreaseStatusLevel<TStatusType, TStatusTarget>(IContextWithTarget<TStatusTarget> context,
            long level)
            where TStatusType : StatusBase<TStatusTarget>
            where TStatusTarget : IStatusTarget
        {
            // Nothing will be changed, we can skip if stacks are 0
            if (level == 0) return;

            // AddStatus also supports increasing status level, so just map it to AddStatus
            // as if status is missing we would like to add it anyway.
            AddStatus<TStatusType, TStatusTarget>(context, level);
        }

        public static async void ReduceStatusLevel<TStatusType, TStatusTarget>(
            IContextWithTarget<TStatusTarget> context,
            long level)
            where TStatusType : StatusBase<TStatusTarget>
            where TStatusTarget : IStatusTarget
        {
            // Nothing will be changed, we can skip if stacks are 0
            if (level == 0) return;

            // Check if status exists
            AppliedStatusReference reference = context.Target.GetStatusReference<TStatusType, TStatusTarget>();

            if (reference == null) AddStatus<TStatusType, TStatusTarget>(context, -level);
            else await reference.TakeLevel(context, level);
        }

        public static void Clear<TStatusType, TStatusTarget>(IContextWithTarget<TStatusTarget> context)
            where TStatusType : StatusBase<TStatusTarget>
            where TStatusTarget : IStatusTarget
        {
            // Get status reference if exists
            AppliedStatusReference reference = context.Target.GetStatusReference<TStatusType, TStatusTarget>();
            
            // Take all stacks from the status
            // this also automatically removes status if level is 0
            reference?.TakeLevel(context, reference.statusLevel).GetAwaiter().GetResult();
        }

        public static long GetLevel<TStatusType, TStatusTarget>(IContextWithTarget<TStatusTarget> context)
            where TStatusType : StatusBase<TStatusTarget>
            where TStatusTarget : IStatusTarget =>
            context.Target.GetStatusReference<TStatusType, TStatusTarget>()?.statusLevel ?? 0;
    }
}