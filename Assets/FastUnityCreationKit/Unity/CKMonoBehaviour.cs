using FastUnityCreationKit.Annotations.Info;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Core.Objects;
using FastUnityCreationKit.Saving.Interfaces;
using FastUnityCreationKit.Saving.Utility;
using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.Unity.Events.Unity;
using FastUnityCreationKit.Unity.Interfaces.Callbacks;
using FastUnityCreationKit.Unity.Interfaces.Interaction;
using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.Unity
{
    /// <summary>
    ///     Base class for all MonoBehaviours compatible with the FastUnityCreationKit.
    ///     Used to automatically handle interface processing.
    /// </summary>
    [SupportedFeature(typeof(ICreateCallback))]
    [SupportedFeature(typeof(IDestroyCallback))]
    [SupportedFeature(typeof(IEnabledCallback))]
    [SupportedFeature(typeof(IDisabledCallback))]
    [SupportedFeature(typeof(IFixedUpdateCallback))]
    [SupportedFeature(typeof(IPreUpdateCallback))]
    [SupportedFeature(typeof(IUpdateCallback))]
    [SupportedFeature(typeof(IPostUpdateCallback))]
    [SupportedFeature(typeof(IQuitCallback))]
    [SupportedFeature(typeof(ISaveableObject))]
    [SupportedFeature(typeof(ITemporaryObject))]
    [SupportedFeature(typeof(IPersistentObject))]
    [SupportedFeature(typeof(IClickable))]
    [SupportedFeature(typeof(IHoverable))]
    [SupportedFeature(typeof(IDoubleClickable))]
    [SupportedFeature(typeof(ISelectable))]
    [SupportedFeature(typeof(IInitializable))]
    public abstract class CKMonoBehaviour : MonoBehaviour
    {
        protected const string GROUP_STATE = "State";
        protected const string GROUP_DEBUG = "Debug";
        protected const string GROUP_CONFIGURATION = "Configuration";

        /// <summary>
        ///     Check if the object uses the update mode
        ///     Used to render the update mode in the inspector.
        ///     Does not account for <see cref="IFixedUpdateCallback" />
        /// </summary>
        protected bool HasAnyUpdateCallback => this is IUpdateCallback ||
                                               this is IPreUpdateCallback ||
                                               this is IPostUpdateCallback;

        /// <summary>
        ///     Check if the object raises any global events.
        /// </summary>
        protected bool RaisesAnyGlobalEvent => RaisedGlobalEvents != CKGlobalEvents.None;

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
        ///     Configure which global events are raised by this object.
        ///     By default <see cref="CKMonoBehaviour" /> raises no global events to avoid performance issues.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_CONFIGURATION)] [ShowIf(nameof(RaisesAnyGlobalEvent))]
        public CKGlobalEvents RaisedGlobalEvents { get; protected set; }

        /// <summary>
        ///     If true, the object will be updated even when disabled.
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_CONFIGURATION)] [ShowIf(nameof(HasAnyUpdateCallback))]
        public virtual UpdateMode UpdateMode => UpdateMode.MonoBehaviour;

        /// <summary>
        ///     Mode of time used for updating the object.
        /// </summary>
        [ShowInInspector] [TitleGroup(GROUP_CONFIGURATION)] [ShowIf(nameof(HasAnyUpdateCallback))]
        public virtual UpdateTime UpdateTimeConfig => UpdateTime.DeltaTime;

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

        internal static void HandleTemporaryObject(CKMonoBehaviour behaviour, float deltaTime)
        {
            // Check if object is temporary, if not skip
            if (behaviour is not ITemporaryObject temporaryObject) return;

            // If object is not expired, skip
            if (!temporaryObject.HasExpired) return;

            // Notify object was expired
            if ((behaviour.RaisedGlobalEvents & CKGlobalEvents.ObjectExpiredEvent) > 0)
                OnObjectExpiredEvent.TriggerEvent(behaviour);

            // Destroy the object
            Destroy(behaviour.gameObject);
        }

        protected virtual void NotifyObjectWasCreated()
        {
            if (this is ICreateCallback createCallback) createCallback.OnObjectCreated();

            if ((RaisedGlobalEvents & CKGlobalEvents.ObjectCreatedEvent) > 0)
                OnObjectCreatedEvent.TriggerEvent(this);
        }

        protected virtual void NotifyObjectWasDestroyed()
        {
            if (this is IDestroyCallback destroyCallback) destroyCallback.OnObjectDestroyed();

            if ((RaisedGlobalEvents & CKGlobalEvents.ObjectDestroyedEvent) > 0)
                OnObjectDestroyedEvent.TriggerEvent(this);
        }

        protected virtual void NotifyObjectWasEnabled()
        {
            if (this is IEnabledCallback enabledCallback) enabledCallback.OnObjectEnabled();

            if ((RaisedGlobalEvents & CKGlobalEvents.ObjectEnabledEvent) > 0)
                OnObjectEnabledEvent.TriggerEvent(this);
        }

        protected virtual void NotifyObjectWasDisabled()
        {
            if (this is IDisabledCallback disabledCallback) disabledCallback.OnObjectDisabled();

            if ((RaisedGlobalEvents & CKGlobalEvents.ObjectDisabledEvent) > 0)
                OnObjectDisabledEvent.TriggerEvent(this);
        }

        protected virtual void NotifyObjectWasFixedUpdated()
        {
            if (this is IFixedUpdateCallback fixedUpdateCallback) fixedUpdateCallback.OnObjectFixedUpdated();

            if ((RaisedGlobalEvents & CKGlobalEvents.ObjectFixedUpdateEvent) > 0)
                OnObjectFixedUpdateEvent.TriggerEvent(this);
        }

        protected virtual void NotifyObjectWasPreUpdated(float deltaTime)
        {
            if (this is IPreUpdateCallback preUpdateCallback) preUpdateCallback.OnBeforeObjectUpdated(deltaTime);

            if ((RaisedGlobalEvents & CKGlobalEvents.ObjectPreUpdateEvent) > 0)
                OnObjectPreUpdateEvent.TriggerEvent(this);
        }

        protected virtual void NotifyObjectWasUpdated(float deltaTime)
        {
            if (this is IUpdateCallback updateCallback) updateCallback.OnObjectUpdated(deltaTime);

            if ((RaisedGlobalEvents & CKGlobalEvents.ObjectUpdateEvent) > 0)
                OnObjectUpdateEvent.TriggerEvent(this);
        }

        protected virtual void NotifyObjectWasPostUpdated(float deltaTime)
        {
            if (this is IPostUpdateCallback postUpdateCallback) postUpdateCallback.OnAfterObjectUpdated(deltaTime);

            if ((RaisedGlobalEvents & CKGlobalEvents.ObjectPostUpdateEvent) > 0)
                OnObjectPostUpdateEvent.TriggerEvent(this);
        }

        /// <summary>
        ///     Enable global event for this object.
        /// </summary>
        protected void EnableGlobalEvent(CKGlobalEvents globalEvent)
        {
            RaisedGlobalEvents |= globalEvent;
        }

        /// <summary>
        ///     Disable global event for this object.
        /// </summary>
        protected void DisableGlobalEvent(CKGlobalEvents globalEvent)
        {
            RaisedGlobalEvents &= ~globalEvent;
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

        protected void OnApplicationQuit()
        {
            if (this is IQuitCallback quitCallback) quitCallback.OnQuit();
        }

#endregion
    }
}