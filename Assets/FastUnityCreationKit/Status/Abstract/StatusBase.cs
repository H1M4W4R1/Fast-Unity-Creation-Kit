using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Data.Containers.Interfaces;
using FastUnityCreationKit.Data.Interfaces;
using FastUnityCreationKit.Identification;
using FastUnityCreationKit.Status.References;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Attributes;
using FastUnityCreationKit.Utility.Limits;
using FastUnityCreationKit.Utility.Logging;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.Status.Abstract
{
    /// <summary>
    /// This class represents a core status that is used to store status data.
    /// </summary> 
    /// <remarks>
    /// Supports int64 limits.
    /// </remarks>
    [AutoCreatedObject(LocalConstants.STATUS_OBJECT_DIRECTORY)]
    [AddressableGroup(LocalConstants.STATUS_ADDRESSABLE_TAG)]
    public abstract class StatusBase : UniqueDefinitionBase, IDefinition<StatusBase>,
        IWithDatabase<StatusDatabase, StatusBase>, ISelfValidator
    {
        /// <summary>
        /// Gets the database for the object.
        /// </summary>
        public StatusDatabase Database => StatusDatabase.Instance;
        
        /// <summary>
        /// Called when status is applied to the target when the target is not affected by the status. 
        /// </summary>
        public virtual async UniTask OnStatusApplied(EntityStatusComponent context) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when status is removed from the target when the target is affected by the status.
        /// </summary>
        public virtual async UniTask OnStatusRemoved(EntityStatusComponent context) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when status level is changed. For percentage status it's also known as 100% stack level change.
        /// </summary>
        public virtual async UniTask OnStatusLevelChanged(EntityStatusComponent context, long difference) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when maximum percentage is reached.
        /// Must support IWithMaxLimit, otherwise it will never be called.
        /// </summary>
        public virtual async UniTask OnMaxLimitReached(EntityStatusComponent context) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when minimum percentage is reached.
        /// Must support IWithMinLimit, otherwise it will never be called.
        /// </summary>
        public virtual async UniTask OnMinLimitReached(EntityStatusComponent context) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Ensure limits for specified status reference.
        /// </summary>
        public LimitHit EnsureLimitsFor(AppliedStatusReference statusReference)
        {
            // Check if this status is limited, if not, return.
            if (this is not ILimited) return LimitHit.None;

            // Check if reference has same status as this status, if not, return with error.
            if (!ReferenceEquals(statusReference.Status, this))
            {
                Guard<EditorAutomationLogConfig>.Error(
                    $"Status reference has different status [{statusReference.Status}] than current status [{this}].");
                return LimitHit.None;
            }

            // Check max status limit
            if (this is IWithMaxLimit maxLimit && statusReference.statusLevel >= maxLimit.MaxLimit)
            {
                statusReference.statusLevel = (long) maxLimit.MaxLimit;
                return LimitHit.UpperLimitHit;
            }

            // Check min status limit
            if (this is IWithMinLimit minLimit && statusReference.statusLevel <= minLimit.MinLimit)
            {
                statusReference.statusLevel = (long) minLimit.MinLimit;
                return LimitHit.LowerLimitHit;
            }

            return LimitHit.None;
        }

        private void Awake()
        {
            // Prevent null database
            if (!Database) return;
            
            if(!Database.Contains(this))
                Database.Add(this);
        }

        private void OnDestroy()
        {
            // Prevent null database
            if (!Database) return;
            
            Database.Remove(this);
        }

        public void Validate(SelfValidationResult result)
        {
            // Check if database contains this status
            if (!Database.Contains(this))
                Database.Add(this);
        }
    }
}