using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Core.Objects;
using FastUnityCreationKit.Saving.Interfaces;
using FastUnityCreationKit.Saving.Utility;
using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.Unity.Editor;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Global;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Local;
using FastUnityCreationKit.Unity.Interfaces.Interaction;
using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Unity
{
    /// <summary>
    ///     Base class for all MonoBehaviours compatible with the FastUnityCreationKit.
    ///     Used to automatically handle interface processing.
    /// </summary>
    public abstract class CKMonoBehaviour : MonoBehaviour
    {
        /// <summary>
        ///     Check if the object uses the update mode
        ///     Used to render the update mode in the inspector.
        ///     Does not account for <see cref="IOnObjectFixedUpdateCallback" />
        /// </summary>
        protected bool HasAnyUpdateCallback => this is IOnObjectUpdateCallback ||
                                               this is IOnObjectPreUpdateCallback ||
                                               this is IOnObjectPostUpdateCallback;

        /// <summary>
        ///     Check if the object raises any global events.
        /// </summary>
        protected bool RaisesAnyGlobalEvent => this is IGlobalCallback;

        /// <summary>
        ///     State of the object. If true, the object is disabled.
        /// </summary>
        // ReSharper disable once Unity.RedundantAttributeOnTarget
        [HideInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] public bool IsDisabled
        {
            get;
            private set;
        }

        /// <summary>
        ///     State of the object. If true, the object is enabled.
        ///     Directly opposite of <see cref="IsDisabled" />.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] public bool IsEnabled
            => !IsDisabled;

        /// <summary>
        ///     State of the object. If true, the object is destroyed.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)] public bool IsDestroyed
        {
            get;
            private set;
        }

        /// <summary>
        ///     If true, the object will be updated even when disabled.
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_CONFIGURATION)] [ShowIf(nameof(HasAnyUpdateCallback))]
        [Tooltip("Defines when object will be updated. Can switch this object to be updated even if time is " +
                 "paused or object is disabled (or both) which is not supported by classic Unity MonoBehaviours.")]
        public virtual UpdateMode UpdateMode => UpdateMode.MonoBehaviour;

        /// <summary>
        ///     Mode of time used for updating the object.
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_CONFIGURATION)] [ShowIf(nameof(HasAnyUpdateCallback))]
        public virtual UpdateTime UpdateTimeConfig => UpdateTime.DeltaTime;

#if UNITY_EDITOR
        [ShowInInspector] [TitleGroup(GROUP_DEBUG)]
        public EventsView EventsView { get; }
#endif

        /// <summary>
        ///     Avoid overriding the Awake method. Implement the <see cref="IInitializable" /> interface instead
        ///     and use the <see cref="IInitializable.OnInitialize" /> method.
        /// </summary>
        protected virtual void Awake()
        {
            // Register this object to the object registry.
            CKEventsManager.Instance.RegisterFastMonoBehaviour(this);

            // Check if supports persistent interface.
            if (this is IPersistentObject)
            {
                // Ensure that object is on the root level as Unity only allows root level objects to be made persistent.
                if (transform.parent)
                {
                    transform.SetParent(null);

                    // Log warning in editor's console to notify the user.
#if UNITY_EDITOR
                    Guard<ValidationLogConfig>.Warning(
                        $"Persistent object {name} is not on the root level. This may cause unexpected behavior." +
                        "Fail-safe triggered: moving object to root layer.");
#endif
                }

                // Make the object persistent.
                DontDestroyOnLoad(gameObject);

                // Check if is temporary object.
                if (this is ITemporaryObject)
                    // Log error in console if object is both persistent and temporary.
                    Guard<ValidationLogConfig>.Warning(
                        $"Object {name} is both persistent and temporary. This will cause severe issues!");
            }

            // Initialize the object if it implements the IInitializable interface.
            if (this is IInitializable initializable) initializable.Initialize();

            // Register the object to the save system if it implements the ISaveableObject interface.
            if (this is ISaveableObject saveableObject) SaveAPI.RegisterSavableObject(saveableObject);

            NotifyObjectWasCreated();

            // If this is both clickable and selectable print warning.
            if (this is IClickable and ISelectable)
                Guard<ValidationLogConfig>.Warning(
                    $"Object {name} is both clickable and selectable. This may cause unexpected behavior.");
        }

        internal static void HandlePreUpdate([NotNull] CKMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasPreUpdated(deltaTime);
        }

        internal static void HandleUpdate([NotNull] CKMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasUpdated(deltaTime);
        }

        internal static void HandlePostUpdate([NotNull] CKMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasPostUpdated(deltaTime);
        }

        internal static void HandleFixedUpdate([NotNull] CKMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectWasFixedUpdated();
        }

        internal static void HandleTemporaryObject([NotNull] CKMonoBehaviour behaviour, float deltaTime)
        {
            behaviour.NotifyObjectHasExpired();
        }

        private void NotifyObjectHasExpired()
        {
            // Check if object is temporary, if not skip
            if (this is not ITemporaryObject temporaryObject) return;

            // If object is not expired, skip
            if (!temporaryObject.HasExpired) return;

            // Notify object was expired
            if (this is IOnObjectExpiredCallback expiredCallback) expiredCallback.OnObjectExpired();

            // Notify global object was expired
            if (this is IOnObjectExpiredGlobalCallback globalExpiredCallback)
                globalExpiredCallback.TriggerOnObjectExpiredEvent(this);
        }

        private void NotifyObjectWasCreated()
        {
            if (this is IOnObjectCreatedCallback createCallback) createCallback.OnObjectCreated();

            if (this is IOnObjectCreatedGlobalCallback globalCreateCallback)
                globalCreateCallback.TriggerOnObjectCreatedEvent(this);
        }

        private void NotifyObjectWasDestroyed()
        {
            if (this is IOnObjectDestroyedCallback destroyCallback) destroyCallback.OnObjectDestroyed();

            if (this is IOnObjectDestroyedGlobalCallback globalDestroyCallback)
                globalDestroyCallback.TriggerOnObjectDestroyedEvent(this);
        }

        private void NotifyObjectWasEnabled()
        {
            if (this is IOnObjectEnabledCallback enabledCallback) enabledCallback.OnObjectEnabled();

            if (this is IOnObjectEnabledGlobalCallback globalEnabledCallback)
                globalEnabledCallback.TriggerOnObjectEnabledEvent(this);
        }

        private void NotifyObjectWasDisabled()
        {
            if (this is IOnObjectDisabledCallback disabledCallback) disabledCallback.OnObjectDisabled();

            if (this is IOnObjectDisabledGlobalCallback globalDisabledCallback)
                globalDisabledCallback.TriggerOnObjectDisabledEvent(this);
        }

        private void NotifyObjectWasFixedUpdated()
        {
            if (this is IOnObjectFixedUpdateCallback fixedUpdateCallback)
                fixedUpdateCallback.OnObjectFixedUpdate();

            if (this is IOnObjectFixedUpdateGlobalCallback globalFixedUpdateCallback)
                globalFixedUpdateCallback.TriggerOnObjectFixedUpdateEvent(this);
        }

        private void NotifyObjectWasPreUpdated(float deltaTime)
        {
            if (this is IOnObjectPreUpdateCallback preUpdateCallback)
                preUpdateCallback.OnBeforeObjectUpdate(deltaTime);

            if (this is IOnObjectPreUpdateGlobalCallback globalPreUpdateCallback)
                globalPreUpdateCallback.TriggerOnObjectPreUpdateEvent(this);
        }

        private void NotifyObjectWasUpdated(float deltaTime)
        {
            if (this is IOnObjectUpdateCallback updateCallback) updateCallback.OnObjectUpdate(deltaTime);

            if (this is IOnObjectUpdateGlobalCallback globalUpdateCallback)
                globalUpdateCallback.TriggerOnObjectUpdateEvent(this);
        }

        private void NotifyObjectWasPostUpdated(float deltaTime)
        {
            if (this is IOnObjectPostUpdateCallback postUpdateCallback)
                postUpdateCallback.OnAfterObjectUpdate(deltaTime);

            if (this is IOnObjectPostUpdateGlobalCallback globalPostUpdateCallback)
                globalPostUpdateCallback.TriggerOnObjectPostUpdateEvent(this);
        }

#region UNITY_EVENTS_IMPLEMENTATION

        protected void OnEnable()
        {
            IsDisabled = false;
            NotifyObjectWasEnabled();
        }

        protected void OnDisable()
        {
            IsDisabled = true;
            NotifyObjectWasDisabled();
        }

        protected void OnDestroy()
        {
            NotifyObjectWasDestroyed();
            IsDestroyed = true;

            // Unregister the object from the save system if it implements the ISaveableObject interface.
            if (this is ISaveableObject saveableObject) SaveAPI.UnregisterSavableObject(saveableObject);

            // Unregister this object from the object registry.
            CKEventsManager.Instance.UnregisterFastMonoBehaviour(this);
        }
#endregion
    }
}