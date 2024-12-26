using System.Collections.Generic;
using FastUnityCreationKit.Context.Interface;
using FastUnityCreationKit.Status.Abstract;
using FastUnityCreationKit.Status.References;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Status.Interfaces
{
    public interface IStatusTarget
    {
        /// <summary>
        /// List of all applied statuses.
        /// </summary>
        protected List<AppliedStatusReference> AppliedStatuses { get; set; }

        /// <summary>
        /// Creates a new status of type <typeparamref name="TStatusType"/> and applies it to the target.
        /// Does nothing if status exists already.
        /// </summary>
        internal AppliedStatusReference GetOrCreateStatusReference<TStatusType, TStatusTarget>(
            IContextWithTarget<TStatusTarget> context, TStatusType statusDefinition)
            where TStatusType : StatusBase<TStatusTarget>
            where TStatusTarget : IStatusTarget
        {
            // Check if status already exists
            AppliedStatusReference reference = GetStatusReference<TStatusType, TStatusTarget>();
            if (reference != null) return reference;
            
            // Create new status reference
            reference = new AppliedStatusReference(context, statusDefinition);

            lock (AppliedStatuses)
            {
                AppliedStatuses ??= new List<AppliedStatusReference>();
                AppliedStatuses.Add(reference);
            }

            return reference;
        }
        
        /// <summary>
        /// Gets a reference to the status of type <typeparamref name="TStatusType"/> that
        /// is applied to the target. Returns null if status is not applied.
        /// </summary>
        [CanBeNull]
        internal AppliedStatusReference GetStatusReference<TStatusType, TStatusTarget>()
            where TStatusType : StatusBase<TStatusTarget>
            where TStatusTarget : IStatusTarget
        {
            lock (AppliedStatuses)
            {
                AppliedStatuses ??= new List<AppliedStatusReference>();

                // Search for status reference
                for (int i = 0; i < AppliedStatuses.Count; i++)
                {
                    if (AppliedStatuses[i].Status is TStatusType)
                        return AppliedStatuses[i];
                }
            }

            return null;
        }

        internal void RemoveStatusReference<TStatusType, TStatusTarget>()
            where TStatusType : StatusBase<TStatusTarget>
            where TStatusTarget : IStatusTarget
        {
            lock (AppliedStatuses)
            {
                AppliedStatuses ??= new List<AppliedStatusReference>();

                // Search for status reference
                for (int i = 0; i < AppliedStatuses.Count; i++)
                {
                    if (AppliedStatuses[i].Status is TStatusType)
                    {
                        AppliedStatuses.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        internal void RemoveStatusReference(AppliedStatusReference referenceObj)
        {
            lock (AppliedStatuses)
            {
                AppliedStatuses ??= new List<AppliedStatusReference>();
                AppliedStatuses.Remove(referenceObj);
            }
        }
        
        /// <summary>
        /// Check if target has status of type <typeparamref name="TStatusType"/>.
        /// </summary>
        internal bool HasStatusReference<TStatusType, TStatusTarget>()
            where TStatusType : StatusBase<TStatusTarget>
            where TStatusTarget : IStatusTarget =>
            GetStatusReference<TStatusType, TStatusTarget>() != null;
    }
}