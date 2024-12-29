using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Status.Abstract;
using FastUnityCreationKit.Status.Interfaces;
using FastUnityCreationKit.Status.References;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// This component is used to store entity status.
    /// </summary>
    public sealed class EntityStatusComponent : FastMonoBehaviour
    {
        /// <summary>
        /// List of all statuses that are applied to the entity.
        /// </summary>
        [ShowInInspector] [TabGroup("Debug")] [ReadOnly]
        public List<AppliedStatusReference> AppliedStatuses { get; } = new List<AppliedStatusReference>();

        /// <summary>
        /// Adds status of type <typeparamref name="TStatusType"/> to the entity.
        /// </summary>
        /// <param name="nLevels">Number of levels to add. Default is 1.</param>
        /// <returns>True if status was added, false if status already exists.</returns>
        /// <remarks>
        /// If status already exists, it's level will be increased by <paramref name="nLevels"/>.
        /// </remarks>
        public async UniTask<bool> AddStatus<TStatusType>(long nLevels = 1)
            where TStatusType : StatusBase
        {
            // Get status from the database
            TStatusType status = StatusDatabase.Instance.GetStatus<TStatusType>();

            if (EditorCheck.Perform(status == null)
                .WithError($"Status of type {typeof(TStatusType).Name} not found in the database. Probably" +
                           $"database was not repopulated correctly.")) return false;
            
            // Check if status already exists
            AppliedStatusReference reference = GetStatusReference<TStatusType>();
            if (reference != null)
            {
                await IncreaseLevel<TStatusType>(nLevels);
                return true;
            }
            
            // Create new status reference
            reference = new AppliedStatusReference(this, status, nLevels);
            AppliedStatuses.Add(reference);
            return true;
        }
        
        /// <summary>
        /// Checks if status of type <typeparamref name="TStatusType"/> is applied to the entity.
        /// </summary>
        public bool HasStatus<TStatusType>()
            where TStatusType : StatusBase =>
            GetStatusReference<TStatusType>() != null;

        /// <summary>
        /// Removes status of type <typeparamref name="TStatusType"/> from the entity.
        /// </summary>
        /// <param name="nLevels">Number of levels to remove. Default is 1.</param>
        /// <returns>True if status was removed, false if status does not exist.</returns>
        public async UniTask<bool> RemoveStatus<TStatusType>(int nLevels = 1)
            where TStatusType : StatusBase
        {
            // Check if status exists
            AppliedStatusReference reference = GetStatusReference<TStatusType>();

            // If status does not exist, return false
            if (reference == null) return false;
            
            // Otherwise, remove status and return true
            await DecreaseLevel<TStatusType>(nLevels);
            return true;
        }
        
        /// <summary>
        /// Clears status of type <typeparamref name="TStatusType"/> from the entity.
        /// </summary>
        public async UniTask Clear<TStatusType>()
            where TStatusType : StatusBase
        {
            // Get status reference if exists
            AppliedStatusReference reference = GetStatusReference<TStatusType>();
            
            // If status does not exist, return
            if (reference == null) return;
            
            // Otherwise, remove status
            await DecreaseLevel<TStatusType>(reference.statusLevel);
        }
        
        /// <summary>
        /// Clears all statuses from the entity.
        /// </summary>
        public async UniTask ClearAll()
        {
            for (int i = AppliedStatuses.Count - 1; i >= 0; i--)
            {
                AppliedStatusReference reference = AppliedStatuses[i];
                
                long nLevels = reference.statusLevel;
                
                // Otherwise, decrease status level
                if (reference.Status is IPercentageStatus)
                    await reference.TakeLevel(this, nLevels * IPercentageStatus.PERCENTAGE_SCALE);
                else
                    await reference.TakeLevel(this, nLevels);
            }
        }
        
        /// <summary>
        /// Increases the level of status of type <typeparamref name="TStatusType"/>.
        /// </summary>
        /// <param name="nLevels">Number of levels to increase. Default is 1.</param>
        /// <typeparam name="TStatusType">Type of status to increase.</typeparam>
        /// <returns>True if status was increased, false if status does not exist.</returns>
        public async UniTask<bool> IncreaseLevel<TStatusType>(long nLevels = 1)
            where TStatusType : StatusBase
        {
            // Get status reference if exists
            AppliedStatusReference reference = GetStatusReference<TStatusType>();
            
            // If status does not exist, add status
            if (reference == null)
                return await AddStatus<TStatusType>(nLevels);
            
            // Otherwise, increase status level
            if(reference.Status is IPercentageStatus)
                await reference.AddLevel(this, nLevels * IPercentageStatus.PERCENTAGE_SCALE);
            else
                await reference.AddLevel(this, nLevels);
            
            return true;
        }
        
        /// <summary>
        /// Decreases the level of status of type <typeparamref name="TStatusType"/>.
        /// </summary>
        /// <param name="nLevels">Number of levels to decrease. Default is 1.</param>
        /// <typeparam name="TStatusType">Type of status to decrease.</typeparam>
        /// <returns>True if status was decreased, false if status does not exist.</returns>
        public async UniTask<bool> DecreaseLevel<TStatusType>(long nLevels = 1)
            where TStatusType : StatusBase
        {
            // Get status reference if exists
            AppliedStatusReference reference = GetStatusReference<TStatusType>();
            
            // If status does not exist, return false
            if (reference == null) return false;
            
            // Otherwise, decrease status level
            if (reference.Status is IPercentageStatus)
                await reference.TakeLevel(this, nLevels * IPercentageStatus.PERCENTAGE_SCALE);
            else
                await reference.TakeLevel(this, nLevels);
            
            return true;
        }

        /// <summary>
        /// Gets the level of status of type <typeparamref name="TStatusType"/>.
        /// </summary>
        public long GetLevel<TStatusType>()
            where TStatusType : StatusBase
        {
            AppliedStatusReference statusRef = GetStatusReference<TStatusType>();
            
            // If status is percentage status, return total percentage loops
            if(statusRef.Status is IPercentageStatus percentageStatus)
                return statusRef.statusLevel / IPercentageStatus.PERCENTAGE_SCALE;
            
            return statusRef.statusLevel;
        }
        
        /// <summary>
        /// Gets the percentage of status of type <typeparamref name="TStatusType"/>.
        /// </summary>
        /// <typeparam name="TStatusType">Type of status to get percentage of.</typeparam>
        /// <returns>Percentage of status level.</returns>
        /// <remarks>If status is not percentage status, returns 0.</remarks>
        public float GetPercentage<TStatusType>()
            where TStatusType : StatusBase
        {
            AppliedStatusReference statusRef = GetStatusReference<TStatusType>();
            
            // If status is percentage status, return total percentage loops
            if (statusRef.Status is IPercentageStatus percentageStatus)
            {
                long percentageRemnant = statusRef.statusLevel % IPercentageStatus.PERCENTAGE_SCALE;
                return percentageRemnant / (float) IPercentageStatus.PERCENTAGE_SCALE;
            }

            Debug.LogError($"Status {nameof(TStatusType)} is not a percentage status.", this);
            return 0;
        }
        
        public async UniTask<bool> IncreasePercentage<TStatusType>(float percentage)
            where TStatusType : StatusBase
        {
            // Get status reference if exists
            AppliedStatusReference reference = GetStatusReference<TStatusType>();
            
            // Otherwise, increase status level
            if(reference?.Status is IPercentageStatus)
                await reference.AddLevel(this, (long) (percentage * IPercentageStatus.PERCENTAGE_SCALE));
            else EditorCheck.Perform(true) // Log error if status is not percentage status
                .WithError($"Status {nameof(TStatusType)} is not a percentage status.");
            
            return false;
        }
        
        public async UniTask<bool> DecreasePercentage<TStatusType>(float percentage)
            where TStatusType : StatusBase
        {
            // Get status reference if exists
            AppliedStatusReference reference = GetStatusReference<TStatusType>();
            
            // Otherwise, increase status level
            if(reference?.Status is IPercentageStatus)
                await reference.TakeLevel(this, (long) (percentage * IPercentageStatus.PERCENTAGE_SCALE));
            else EditorCheck.Perform(true) // Log error if status is not percentage status
                    .WithError($"Status {nameof(TStatusType)} is not a percentage status.");
            
            return false;
        }

        /// <summary>
        /// Gets a reference to the status of type <typeparamref name="TStatusType"/> that
        /// </summary>
        public AppliedStatusReference GetStatusReference<TStatusType>()
            where TStatusType : StatusBase
        {
            // Search for status reference
            for (int i = 0; i < AppliedStatuses.Count; i++)
            {
                if (AppliedStatuses[i].Status is TStatusType)
                    return AppliedStatuses[i];
            }

            return null;
        }

        /// <summary>
        /// Deletes status reference from the list. Used internally to remove status if it's level is 0.
        /// </summary>
        internal void DeleteStatusReference(AppliedStatusReference appliedStatusReference)
        {
            AppliedStatuses.Remove(appliedStatusReference);
        }
    }
}