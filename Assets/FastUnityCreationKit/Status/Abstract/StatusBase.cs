using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Addressables;
using FastUnityCreationKit.Annotations.Data;
using FastUnityCreationKit.Annotations.Info;
using FastUnityCreationKit.Annotations.Unity;
using FastUnityCreationKit.Identification;
using FastUnityCreationKit.Status.Interfaces;
using FastUnityCreationKit.Utility.Limits;
using FastUnityCreationKit.Utility.Logging;

namespace FastUnityCreationKit.Status.Abstract
{
    /// <summary>
    /// This class represents a core status that is used to store status data. <br/><br/>
    /// It is strongly recommended to use <see cref="StatusContainer"/> to store status data.
    /// Otherwise, you will need to implement low-level features from <see cref="SupportedFeatureAttribute"/>
    /// list manually.
    /// </summary> 
    /// <remarks>
    /// Supports int64 limits.
    /// </remarks>
    [AutoCreatedObject(LocalConstants.STATUS_OBJECT_DIRECTORY)]
    [AddressableGroup(LocalConstants.STATUS_ADDRESSABLE_TAG)]
    [AutoRegisterIn(typeof(StatusDatabase))]
    [SupportedFeature(typeof(IPercentageStatus))]
    [SupportedFeature(typeof(ILimited))]
    [SupportedFeature(typeof(ITemporaryStatus))]
    public abstract class StatusBase : UniqueDefinitionBase
    {
        /// <summary>
        /// Gets the database for the object.
        /// </summary>
        public StatusDatabase Database => StatusDatabase.Instance;
        
        /// <summary>
        /// Called when status is applied to the target when the target is not affected by the status. 
        /// </summary>
        public virtual async UniTask OnStatusApplied(StatusContainer context) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when status is removed from the target when the target is affected by the status.
        /// </summary>
        public virtual async UniTask OnStatusRemoved(StatusContainer context) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when status level is changed. For percentage status it's also known as 100% stack level change.
        /// </summary>
        public virtual async UniTask OnStatusLevelChanged(StatusContainer context, long difference) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when maximum percentage is reached.
        /// Must support IWithMaxLimit, otherwise it will never be called.
        /// </summary>
        public virtual async UniTask OnMaxLimitReached(StatusContainer context) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when minimum percentage is reached.
        /// Must support IWithMinLimit, otherwise it will never be called.
        /// </summary>
        public virtual async UniTask OnMinLimitReached(StatusContainer context) =>
            await UniTask.CompletedTask;
        
        /// <summary>
        /// Called when frame is updated.
        /// </summary>
        public virtual async UniTask OnUpdate(StatusContainer context) =>
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
                Guard<ValidationLogConfig>.Error(
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
    }
}