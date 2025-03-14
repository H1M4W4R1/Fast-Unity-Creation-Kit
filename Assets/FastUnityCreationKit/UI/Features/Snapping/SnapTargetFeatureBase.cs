﻿using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Interfaces;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.UI.Features.Snapping
{
    /// <summary>
    ///     Represents a snap target - object that can be snapped to (for example inventory slot).
    ///     This object does not support multi-snap instances.
    /// </summary>
    /// <typeparam name="TSelf">Type of the snap target.</typeparam>
    public abstract class SnapTargetFeatureBase<TSelf> : UIObjectBase, ISnapTarget<TSelf>,
        IOnObjectSnappedCallback<TSelf>
        where TSelf : SnapTargetFeatureBase<TSelf>, ISnapTarget<TSelf>, new()
    {
        [UsedImplicitly] public virtual void OnSnapBreak(SnapToFeatureBase<TSelf> objectBrokenFromSnap)
        {
            HasSnappedObject = false;
        }

        [UsedImplicitly] public virtual void OnSnap(SnapToFeatureBase<TSelf> objectSnapped)
        {
            HasSnappedObject = true;
        }

        /// <summary>
        ///     If true, an object is snapped to this object.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] public bool HasSnappedObject
        {
            get;
            private set;
        }

        /// <summary>
        ///     If true, multiple objects can be snapped to this object.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)]
        public virtual bool MultipleSnapsPossible { get; private set; }

        /// <summary>
        ///     Checks if it is possible to snap to the object.
        /// </summary>
        /// <param name="snapObject">Object you wish to snap to.</param>
        /// <returns>True if it is possible to snap to the object, false otherwise</returns>
        public virtual bool IsPossibleToSnap(SnapToFeatureBase<TSelf> snapObject)
        {
            return !HasSnappedObject || MultipleSnapsPossible;
        }
    }
}