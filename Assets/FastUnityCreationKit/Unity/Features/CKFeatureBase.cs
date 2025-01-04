using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.Unity.Features
{
    /// <summary>
    ///     Base class for features that are attached to specific object.
    /// </summary>
    /// <remarks>
    ///     It is strongly recommended to add <see cref="RequireComponent" /> attribute to the derived class
    ///     where type is set to same as <see cref="TFeaturedObject" />.
    /// </remarks>
    public abstract class CKFeatureBase<TFeaturedObject> : CKFeatureBase, IInitializable
        where TFeaturedObject : Component
    {
        /// <summary>
        ///     Reference to the object this feature is attached to.
        /// </summary>
        [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] [NotNull]
        // ReSharper disable once NullableWarningSuppressionIsUsed
        protected TFeaturedObject FeaturedObject { get; private set; } = null!;

        public new void OnInitialize()
        {
            FeaturedObject = GetComponent<TFeaturedObject>();
            base.OnInitialize();
        }
    }

    /// <summary>
    ///     Represents a feature of GameObject.
    ///     It can be as simple as UI object being able to snap or draggable to
    ///     more complex features like a camera that can follow a target
    ///     ending up on automatic physic supports.
    /// </summary>
    public abstract class CKFeatureBase : CKMonoBehaviour, IInitializable
    {
        /// <summary>
        ///     GameObject this feature is attached to.
        /// </summary>
        [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] [NotNull]
        // ReSharper disable once NullableWarningSuppressionIsUsed
        protected GameObject GameObject { [UsedImplicitly] get; private set; } = null!;

        bool IInitializable.InternalInitializationStatusStorage { get; set; }

        public void OnInitialize()
        {
            GameObject = gameObject;
            OnInitializeCompleted();
        }

        /// <summary>
        ///     Called when the initialization of the feature is completed.
        /// </summary>
        protected virtual void OnInitializeCompleted()
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