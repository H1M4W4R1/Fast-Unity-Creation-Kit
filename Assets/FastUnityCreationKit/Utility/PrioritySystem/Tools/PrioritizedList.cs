using System;
using System.Collections.Generic;
using FastUnityCreationKit.Utility.PrioritySystem.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Utility.PrioritySystem.Tools
{
    /// <summary>
    /// Represents a list of prioritized objects.
    /// Objects are automatically sorted by their priority when added to the list.
    ///
    /// Assumes that the priority 0 is the highest priority.
    /// </summary>
    /// BUG: This may cause issues if cast to underlying types, especially interfaces
    /// TODO: Rework this as wrapper for List-T instead of inheritance
    public class PrioritizedList<TPriorityObject> : List<TPriorityObject> where TPriorityObject : IPrioritySupport
    {
        /// <inheritdoc cref="List{T}.Add"/>
        public new void Add([NotNull] TPriorityObject item)
        {
            // Loop through the list and insert the object at the correct position.
            for (int i = 0; i < Count; i++)
            {
                // If the priority of the object is greater or equal to the current object in the list, continue.
                // This is because the priority 0 is the highest priority.
                // So if item priority is less or equal to the current object we assume
                // that we should insert the item before the current object.
                if (item.Priority > this[i].Priority) continue;

                // We need to use base method to prevent throwing exception.
                base.Insert(i, item);
                return;
            }

            // If object was not inserted just add it to the end of the list.
            // This is case where the object has the lowest priority or the list is empty.
            base.Add(item);
        }

#region DISABLED_METHODS

        public new void Insert(int index, [NotNull] TPriorityObject item)
        {
            throw new NotSupportedException("Use Add method to insert object at the correct position automatically.");
        }

        public new void Reverse() =>
            throw new NotSupportedException("This is not supported for prioritized list.");

        public new void Reverse(int index, int count) =>
            throw new NotSupportedException("This is not supported for prioritized list.");

        public new void Sort() =>
            throw new NotSupportedException("This is not supported for prioritized list.");

        public new void Sort(Comparison<TPriorityObject> comparison) =>
            throw new NotSupportedException("This is not supported for prioritized list.");

        public new void Sort(IComparer<TPriorityObject> comparer) =>
            throw new NotSupportedException("This is not supported for prioritized list.");

        public new void Sort(int index, int count, IComparer<TPriorityObject> comparer) =>
            throw new NotSupportedException("This is not supported for prioritized list.");

#endregion
    }
}