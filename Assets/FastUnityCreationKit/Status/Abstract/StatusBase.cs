using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Context.Interface;
using FastUnityCreationKit.Core.Numerics;
using FastUnityCreationKit.Core.Numerics.Limits;
using FastUnityCreationKit.Status.References;
using UnityEngine;

namespace FastUnityCreationKit.Status.Abstract
{
    /// <summary>
    /// This class represents a core status that is used to store status data.
    /// </summary>
    /// <remarks>
    /// Supports int64 limits.
    /// </remarks>
    public abstract class StatusBase<TStatusTarget> : StatusBase
    {
        /// <summary>
        /// Called when status is applied to the target when the target is not affected by the status.
        /// </summary>
        public virtual async UniTask OnStatusApplied(IContextWithTarget<TStatusTarget> context) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when status is removed from the target when the target is affected by the status.
        /// </summary>
        public virtual async UniTask OnStatusRemoved(IContextWithTarget<TStatusTarget> context) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when status level is changed. For percentage status it's also known as 100% stack level change.
        /// </summary>
        public virtual async UniTask OnStatusLevelChanged(IContextWithTarget<TStatusTarget> context) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when maximum percentage is reached.
        /// Must support IWithMaxLimit, otherwise it will never be called.
        /// </summary>
        public virtual async UniTask OnMaxLimitReached(IContextWithTarget<TStatusTarget> context) =>
            await UniTask.CompletedTask;

        /// <summary>
        /// Called when minimum percentage is reached.
        /// Must support IWithMinLimit, otherwise it will never be called.
        /// </summary>
        public virtual async UniTask OnMinLimitReached(IContextWithTarget<TStatusTarget> context) =>
            await UniTask.CompletedTask;
    }

    /// <summary>
    /// Internal status base class, do not use.
    /// </summary>
    public abstract class StatusBase : ScriptableObject
    {
        /// <summary>
        /// Ensure limits for specified status reference.
        /// </summary>
        public LimitHit EnsureLimitsFor(ref AppliedStatusReference statusReference)
        {
            // Check if this status is limited, if not, return.
            if (this is not ILimited) return LimitHit.None;

            // Check if reference has same status as this status, if not, return with error.
            if (!ReferenceEquals(statusReference.status, this))
            {
                Debug.LogError($"Status reference has different status [{statusReference.status}] than this status.",
                    this);
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