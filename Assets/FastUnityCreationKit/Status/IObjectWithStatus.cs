using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// Represents that an object can have specific status.
    /// This is a marker interface and should be used with a generic type to specify the status type.
    /// For more reference see <see cref="IObjectWithStatus.IsStatusSupported{T}"/>.
    /// </summary>
    public interface IObjectWithStatus<[UsedImplicitly] TStatus> : IObjectWithStatus
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
                IStatus status = Statuses[i];

                // Check if the status is stackable and has 0 stack count.
                // If not skip to the next status.
                if (status is not IStackableStatus stackableStatus || stackableStatus.StackCount != 0) continue;

                // Remove if the status is percentage and has 0 percentage.
                if (status is IPercentageStatus {Percentage: <= math.EPSILON})
                    RemoveStatus(i);
                else if (status is not IPercentageStatus)
                    RemoveStatus(i);

                // Otherwise skip to the next status - stack count is 0 and status still has percentage.
                // This is to support status with both stack count and percentage.
                // Don't ask me why you would need that, but it's supported.
            }

            // Loop through all statuses and remove the ones that have 0 percentage.
            // In reverse order to prevent index out of range exception.
            for (int i = Statuses.Count - 1; i >= 0; i--)
            {
                IStatus status = Statuses[i];

                // Check if the status is percentage and has 0 percentage.
                // Comparing with math.EPSILON to prevent floating point errors.
                if (status is not IPercentageStatus {Percentage: <= math.EPSILON} percentageStatus) continue;

                // Check if status is stackable 
                if (status is IStackableStatus stackableStatus)
                {
                    // Check if the stack count is 0.
                    if (stackableStatus.StackCount == 0)
                        RemoveStatus(i);
                    else
                    {
                        // Reduce stack count and set percentage to 100%.
                        stackableStatus.DecreaseStackCount(this, stackableStatus.StackCount);
                        percentageStatus.IncreasePercentage(this, 1f);
                    }
                }
                else
                    RemoveStatus(i);
            }
        }

        /// <summary>
        /// Removes the status at the given index.
        /// </summary>
        private void RemoveStatus(int index)
        {
            EnsureInitialized();

            // Check if the index is valid.
            if (index < 0 || index >= Statuses.Count) return;

            // Remove the status at the given index.
            Statuses[index].OnStatusRemoved(this);

            // Remove the status from the list.
            Statuses.RemoveAt(index);
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

#if UNITY_EDITOR
            switch (status)
            {
                // Check if is stackable or percentage status.
                case IStackableStatus:
                    Debug.LogWarning(
                        "Adding stackable status to the object. Use IncreaseStatusStackCount method instead for better safety.");
                    break;
                case IPercentageStatus:
                    Debug.LogWarning(
                        "Adding percentage status to the object. Use IncreaseStatusPercentage method instead for better safety.");
                    break;
            }
#endif

            // Check if the object already has the status.
            if (HasStatus<TStatusType>())
            {
                // If status is stackable, increase the stack count.
                if (status is IStackableStatus stackableStatus)
                {
                    stackableStatus.IncreaseStackCount(this);
                    return;
                }

                // If status is percentage, set the percentage to 100%.
                if (status is IPercentageStatus percentageStatus)
                {
                    Debug.LogWarning("The object already has the status. Increasing the percentage to 100%.");
                    percentageStatus.IncreasePercentage(this, 1f);
                    ClearZeroLevelStatuses();
                    return;
                }
                
                // Otherwise, just return as the status is already added.
                // This is to prevent adding the same status multiple times.
                // If you want to have multiple instances of the same status,
                // you need to implement IStackableStatus interface.
            }
            else
            {
                // Add the status to the list.
                Statuses.Add(status);

                // Check if the status is stackable and add single stack.
                if (status is IStackableStatus stackableStatus)
                    stackableStatus.IncreaseStackCount(this);

                // Check if the status is percentage and set it to 100%.
                if (status is IPercentageStatus percentageStatus)
                    percentageStatus.IncreasePercentage(this, 1f);

                status.OnStatusAdded(this);
            }

            // Clear the statuses that have 0 stack count or 0 percentage.
            ClearZeroLevelStatuses();
        }
        
        /// <summary>
        /// Used internally to add the status to the object.
        /// </summary>
        private void _AddStatus<TStatusType>([NotNull] TStatusType status) where TStatusType : IStatus
        {
            EnsureInitialized();

            // Check if the object already has the status.
            if (HasStatus<TStatusType>()) return;
            
            // Add the status to the list.
            Statuses.Add(status);
            status.OnStatusAdded(this);
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
                    // Decrease percentage to 0% if the status is percentage.
                    // This is to prevent graphical issues and UI errors.
                    if (status is IPercentageStatus percentageStatus)
                    {
                        // Decrease the percentage to 0%.
                        percentageStatus.DecreasePercentage(this, 1f);
                    }
                    
                    // Decrease stack count to 0 if the status is stackable.
                    // Order of those operations matters as the status may be both stackable and percentage.
                    if (status is IStackableStatus stackableStatus)
                    {
                        // Decrease the stack count to 0.
                        stackableStatus.DecreaseStackCount(this, stackableStatus.StackCount);
                    }

                    // Proceed with removing the status.
                    RemoveStatus(i);
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
                    status.DecreaseStackCount(this, amount);
                    ClearZeroLevelStatuses();
                    return;
                }
            }

            // Create status instance and add it to the object.
            TStatusType newStatus = new();
            newStatus.DecreaseStackCount(this, amount);
            _AddStatus(newStatus);
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
                    status.IncreaseStackCount(this, amount);
                    ClearZeroLevelStatuses();
                    return;
                }
            }

            // Create status instance and add it to the object.
            TStatusType newStatus = new();
            newStatus.IncreaseStackCount(this, amount);
            _AddStatus(newStatus);
        }

        /// <summary>
        /// Increase the percentage of the status of the given type.
        /// </summary>
        public void IncreaseStatusPercentage<TStatusType>(float amount = 1f)
            where TStatusType : IPercentageStatus, new()
        {
            EnsureInitialized();

            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (Statuses[i] is TStatusType status)
                {
                    status.IncreasePercentage(this, amount);
                    ClearZeroLevelStatuses();
                    return;
                }
            }

            // Create status instance and add it to the object.
            TStatusType newStatus = new();
            _AddStatus(newStatus);
            newStatus.IncreasePercentage(this, amount);
        }

        /// <summary>
        /// Decrease the percentage of the status of the given type.
        /// </summary>
        public void DecreaseStatusPercentage<TStatusType>(float amount = 1f)
            where TStatusType : IPercentageStatus, new()
        {
            EnsureInitialized();

            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                if (Statuses[i] is TStatusType status)
                {
                    status.DecreasePercentage(this, amount);
                    ClearZeroLevelStatuses();
                    return;
                }
            }

            // Create status instance and add it to the object.
            TStatusType newStatus = new();
            newStatus.DecreasePercentage(this, amount);
            _AddStatus(newStatus);
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
        
        /// <summary>
        /// Gets the amount of times the status of the given type is added to the object.
        /// In case the status is stackable, it will return the total stack count.
        /// </summary>
        public int GetAmountOfTimesStatusIsAdded<TStatusType>() where TStatusType : IStatus
        {
            EnsureInitialized();

            int count = 0;
            
            // Loop through all statuses and check if the object has the status.
            for (int i = 0; i < Statuses.Count; i++)
            {
                // Check if the status is stackable
                if(Statuses[i] is IStackableStatus stackableStatus and TStatusType)
                {
                    count += stackableStatus.StackCount;
                    continue;
                }
                
                // Check if the status is of the given type
                if (Statuses[i] is TStatusType)
                    count++;
            }

            return count;
        }
    }
}