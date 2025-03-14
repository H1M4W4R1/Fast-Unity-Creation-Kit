﻿using FastUnityCreationKit.Unity.Interfaces.Callbacks.Local;
using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Unity.Features
{
    /// <summary>
    ///     Base class for features that are attached to specific object.
    /// </summary>
    /// <remarks>
    ///     It is strongly recommended to add <see cref="RequireComponent" /> attribute to the derived class
    ///     where type is set to same as <see cref="TFeaturedObject" />.
    /// </remarks>
    public abstract class CKFeatureBase<TFeaturedObject> : CKFeatureBase
        where TFeaturedObject : Component
    {
        /// <summary>
        ///     Reference to the object this feature is attached to.
        /// </summary>
        [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] [NotNull]
        // ReSharper disable once NullableWarningSuppressionIsUsed
        protected TFeaturedObject FeaturedObject { get; private set; } = null!;

        public override void OnObjectInitialized()
        {
            FeaturedObject = GetComponent<TFeaturedObject>();
            base.OnObjectInitialized();
        }
    }

    /// <summary>
    ///     Represents a feature of GameObject.
    ///     It can be as simple as UI object being able to snap or draggable to
    ///     more complex features like a camera that can follow a target
    ///     ending up on automatic physic supports.
    /// </summary>
    public abstract class CKFeatureBase : CKMonoBehaviour, IOnObjectInitializedCallback
    {
        /// <summary>
        ///     GameObject this feature is attached to.
        /// </summary>
        [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] [NotNull]
        // ReSharper disable once NullableWarningSuppressionIsUsed
        protected GameObject GameObject { [UsedImplicitly] get; private set; } = null!;

        public virtual void OnObjectInitialized()
        {
            GameObject = gameObject;
            OnInitializeCompleted();
        }

        /// <summary>
        ///     Called when the initialization of the feature is completed.
        /// </summary>
        [UsedImplicitly] protected internal virtual void OnInitializeCompleted()
        {
        }

#region UPDATE_CONFIGURATION

        // UI objects are always updated (even when disabled or when time is paused) and
        // they are updated using unscaled delta time - to prevent UI from being dependent on time scale.
        public override UpdateTime UpdateTimeConfig => UpdateTime.UnscaledDeltaTime;
        public override UpdateMode UpdateMode => UpdateMode.UpdateWhenDisabled | UpdateMode.UpdateWhenTimePaused;

#endregion
    }
}