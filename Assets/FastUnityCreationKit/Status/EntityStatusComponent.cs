using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Status.Abstract;
using FastUnityCreationKit.Status.References;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Utility;

namespace FastUnityCreationKit.Status
{
    /// <summary>
    /// This component is used to store entity status.
    /// </summary>
    public sealed class EntityStatusComponent : FastMonoBehaviour<EntityStatusComponent>
    {
        /// <summary>
        /// List of all statuses that are applied to the entity.
        /// </summary>
        public List<AppliedStatusReference> AppliedStatuses { get; } = new List<AppliedStatusReference>();

        /// <summary>
        /// Adds status of type <typeparamref name="TStatusType"/> to the entity.
        /// </summary>
        /// <param name="nLevels">Number of levels to add. Default is 1.</param>
        /// <returns>True if status was added, false if status already exists.</returns>
        /// <remarks>
        /// If status already exists, it's level will be increased by <paramref name="nLevels"/>.
        /// </remarks>
        public async UniTask<bool> AddStatus<TStatusType>(int nLevels = 1)
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
                await reference.AddLevel(this, nLevels);
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
            await reference.TakeLevel(this, nLevels);
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
            await reference.TakeLevel(this, reference.statusLevel);
        }
        
        /// <summary>
        /// Increases the level of status of type <typeparamref name="TStatusType"/>.
        /// </summary>
        /// <param name="nLevels">Number of levels to increase. Default is 1.</param>
        /// <typeparam name="TStatusType">Type of status to increase.</typeparam>
        /// <returns>True if status was increased, false if status does not exist.</returns>
        public async UniTask<bool> IncreaseLevel<TStatusType>(int nLevels = 1)
            where TStatusType : StatusBase
        {
            // Get status reference if exists
            AppliedStatusReference reference = GetStatusReference<TStatusType>();
            
            // If status does not exist, add status
            if (reference == null)
                return await AddStatus<TStatusType>(nLevels);
            
            // Otherwise, increase status level
            await reference.AddLevel(this, nLevels);
            return true;
        }
        
        /// <summary>
        /// Decreases the level of status of type <typeparamref name="TStatusType"/>.
        /// </summary>
        /// <param name="nLevels">Number of levels to decrease. Default is 1.</param>
        /// <typeparam name="TStatusType">Type of status to decrease.</typeparam>
        /// <returns>True if status was decreased, false if status does not exist.</returns>
        public async UniTask<bool> DecreaseLevel<TStatusType>(int nLevels = 1)
            where TStatusType : StatusBase
        {
            // Get status reference if exists
            AppliedStatusReference reference = GetStatusReference<TStatusType>();
            
            // If status does not exist, return false
            if (reference == null) return false;
            
            // Otherwise, decrease status level
            await reference.TakeLevel(this, nLevels);
            return true;
        }
        
        /// <summary>
        /// Gets the level of status of type <typeparamref name="TStatusType"/>.
        /// </summary>
        public long GetLevel<TStatusType>()
            where TStatusType : StatusBase =>
            GetStatusReference<TStatusType>()?.statusLevel ?? 0;
        
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