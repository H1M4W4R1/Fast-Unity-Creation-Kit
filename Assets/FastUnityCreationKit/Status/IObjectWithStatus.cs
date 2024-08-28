using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Represents that an object can have specific status.
    /// </summary>
    /// <remarks>
    /// <see cref="TStatus"/> should be an abstract class or interface that represents the status as
    /// ...
    /// </remarks>
    public interface IObjectWithStatus<TStatus> : IObjectWithStatus
        where TStatus : IStatus
    {
    }

    /// <summary>
    /// Represents that an object can have a status.
    /// </summary>
    public interface IObjectWithStatus
    {
        /// <summary>
        /// List of statuses that the object has.
        /// </summary>
        protected List<IStatus> Statuses { get; set; }

        /// <summary>
        /// Ensures that the status list is initialized.
        /// </summary>
        private void EnsureInitialized() => Statuses ??= new List<IStatus>();

        /// <summary>
        /// Used to clear the statuses that have 0 stack count or 0 percentage.
        /// Automatically cleans up the status list.
        /// This is used to remove UI errors and prevent graphical issues.
        /// </summary>
        private void ClearZeroLevelStatuses()
        {
            EnsureInitialized();

            // Loop through all statuses and remove the ones that have 0 stack count.
            // In reverse order to prevent index out of range exception.
            for (int i = Statuses.Count - 1; i >= 0; i--)
            {
                // If the status is stackable and has 0 stack count, remove it.
                if (Statuses[i] is IStackableStatus stackableStatus && stackableStatus.StackCount == 0)
                    Statuses.RemoveAt(i);
            }

            // Loop through all statuses and remove the ones that have 0 percentage.
            // In reverse order to prevent index out of range exception.
            for (int i = Statuses.Count - 1; i >= 0; i--)
            {
                // If the status is percentage and has 0 percentage, remove it.
                // Comparing with math.EPSILON to prevent floating point errors.
                if (Statuses[i] is IPercentageStatus {Percentage: <= math.EPSILON})
                    Statuses.RemoveAt(i);
            }
        }

        /// <summary>
        /// Checks if the object has the given status.
        /// </summary>
        public bool HasStatus<TStatusType>() where TStatusType : IStatus
        {
            EnsureInitialized();

            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (Statuses[i] is TStatusType) return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the status of the given type.
        /// </summary>
        [CanBeNull]
        public TStatusType GetStatus<TStatusType>() where TStatusType : IStatus
        {
            EnsureInitialized();

            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (Statuses[i] is TStatusType status) return status;
            }

            return default;
        }

        /// <summary>
        /// Add the given status to the object.
        /// </summary>
        public void AddStatus<TStatusType>([NotNull] TStatusType status) where TStatusType : IStatus
        {
            EnsureInitialized();

            // Check if the object already has the status.
            if (HasStatus<TStatusType>())
            {
                // If status is stackable, increase the stack count.
                if (status is IStackableStatus stackableStatus)
                {
                    stackableStatus.IncreaseStackCount();
                    return;
                }

                // If status is percentage, set the percentage to 100%.
                if (status is IPercentageStatus percentageStatus)
                {
                    Debug.LogWarning("The object already has the status. Increasing the percentage to 100%.");
                    percentageStatus.IncreasePercentage(100f);
                    ClearZeroLevelStatuses();
                    return;
                }
            }

            // Add the status to the list.
            Statuses.Add(status);
            ClearZeroLevelStatuses();
        }
        
        /// <summary>
        /// Remove the status of the given type from the object.
        /// </summary>
        public void RemoveStatus<TStatusType>() where TStatusType : IStatus
        {
            EnsureInitialized();

            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (Statuses[i] is TStatusType status)
                {
                    Statuses.RemoveAt(i);
                    ClearZeroLevelStatuses();
                    return;
                }
            }
        }
        
        /// <summary>
        /// Decrease the stack count of the status of the given type.
        /// </summary>
        public void DecreaseStatusStackCount<TStatusType>(int amount = 1) where TStatusType : IStackableStatus, new()
        {
            EnsureInitialized();

            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (Statuses[i] is TStatusType status)
                {
                    status.DecreaseStackCount(amount);
                    ClearZeroLevelStatuses();
                    return;
                }
            }

            // Create status instance and add it to the object.
            TStatusType newStatus = new();
            newStatus.DecreaseStackCount(amount);
            AddStatus(newStatus);
        }
        
        /// <summary>
        /// Increase the stack count of the status of the given type.
        /// </summary>
        public void IncreaseStatusStackCount<TStatusType>(int amount = 1) where TStatusType : IStackableStatus, new()
        {
            EnsureInitialized();

            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (Statuses[i] is TStatusType status)
                {
                    status.IncreaseStackCount(amount);
                    ClearZeroLevelStatuses();
                    return;
                }
            }
            
            // Create status instance and add it to the object.
            TStatusType newStatus = new();
            newStatus.IncreaseStackCount(amount);
            AddStatus(newStatus);
        }
        
        /// <summary>
        /// Increase the percentage of the status of the given type.
        /// </summary>
        public void IncreaseStatusPercentage<TStatusType>(float amount = 1f) where TStatusType : IPercentageStatus, new()
        {
            EnsureInitialized();

            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (Statuses[i] is TStatusType status)
                {
                    status.IncreasePercentage(amount);
                    ClearZeroLevelStatuses();
                    return;
                }
            }
            
            // Create status instance and add it to the object.
            TStatusType newStatus = new();
            newStatus.IncreasePercentage(amount);
            AddStatus(newStatus);
        }
        
        /// <summary>
        /// Decrease the percentage of the status of the given type.
        /// </summary>
        public void DecreaseStatusPercentage<TStatusType>(float amount = 1f) where TStatusType : IPercentageStatus, new()
        {
            EnsureInitialized();

            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (Statuses[i] is TStatusType status)
                {
                    status.DecreasePercentage(amount);
                    ClearZeroLevelStatuses();
                    return;
                }
            }
            
            // Create status instance and add it to the object.
            TStatusType newStatus = new();
            newStatus.DecreasePercentage(amount);
            AddStatus(newStatus);
        }
        
        /// <summary>
        /// Checks if the object supports the given status type.
        /// </summary>
        public bool IsStatusSupported<TStatusType>() where TStatusType : IStatus =>
            this is IObjectWithStatus<TStatusType>;
        
        /// <summary>
        /// Gets the stack count of the status of the given type.
        /// </summary>
        public int GetStatusStackCount<TStatusType>() where TStatusType : IStackableStatus
        {
            EnsureInitialized();

            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (Statuses[i] is TStatusType status) return status.StackCount;
            }

            return 0;
        }
        
        /// <summary>
        /// Gets the percentage of the status of the given type.
        /// </summary>
        public float GetStatusPercentage<TStatusType>() where TStatusType : IPercentageStatus
        {
            EnsureInitialized();

            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (Statuses[i] is TStatusType status) return status.Percentage;
            }

            return 0f;
        }
        
    }
}