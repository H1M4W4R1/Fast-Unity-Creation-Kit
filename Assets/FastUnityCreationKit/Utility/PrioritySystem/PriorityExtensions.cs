using System.Collections.Generic;
using FastUnityCreationKit.Utility.PrioritySystem.Abstract;
using FastUnityCreationKit.Utility.PrioritySystem.Tools;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Utility.PrioritySystem
{
    /// <summary>
    /// An extension class used to provide priority support to objects.
    /// </summary>
    public static class PriorityExtensions
    {
        /// <summary>
        /// Sorts objects in list using their priority.
        /// </summary>
        public static void SortByPriority<TPriorityObject>([NotNull] [ItemNotNull] this IList<TPriorityObject> objects) 
            where TPriorityObject : IPrioritySupport
        {
            // Do not sort if the list is already a prioritized list.
            // Prioritized lists are assumed to be sorted by default.
            if (objects is PrioritizedList<TPriorityObject>) return;
            
            // Do not sort if there are no objects or only one object.
            if (objects.Count <= 1) return;
            
            // Sort the objects by their priority.
            for(int i = 0; i < objects.Count; i++)
            {
                int minIndex = i;
                for(int j = i + 1; j < objects.Count; j++)
                {
                    if (objects[j].Priority < objects[minIndex].Priority)
                        minIndex = j;
                }
                if (minIndex != i)
                {
                    // Swap the objects.
                    (objects[i], objects[minIndex]) = (objects[minIndex], objects[i]);
                }
            }
        }
    }
}