using System.Collections.Generic;
using FastUnityCreationKit.Core.PrioritySystem.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.PrioritySystem.Tools
{
    /// <summary>
    /// Represents a list of prioritized objects.
    /// Objects are automatically sorted by their priority when added to the list.
    ///
    /// Assumes that the priority 0 is the highest priority.
    /// </summary>
    public class PrioritizedList<TPriorityObject> : List<TPriorityObject> where TPriorityObject : IPrioritySupport
    {
        /// <inheritdoc cref="List{T}.Add"/>
        public new void Add([NotNull] TPriorityObject item)
        {
            // Loop through the list and insert the object at the correct position.
            for(int i = 0; i < Count; i++)
            {
                // If the priority of the object is greater or equal to the current object in the list, continue.
                // This is because the priority 0 is the highest priority.
                // So if item priority is less or equal to the current object we assume
                // that we should insert the item before the current object.
                if (item.Priority > this[i].Priority) continue;
                
                Insert(i, item);
                return;
            }
            
            // If object was not inserted just add it to the end of the list.
            // This is case where the object has the lowest priority or the list is empty.
            base.Add(item);
        }
        
    }
}