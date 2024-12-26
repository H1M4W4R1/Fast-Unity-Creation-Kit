using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.Unity.Events;
using FastUnityCreationKit.Unity.Events.Data;
using FastUnityCreationKit.Unity.Events.Input;
using FastUnityCreationKit.Unity.Events.Interfaces;
using FastUnityCreationKit.Unity.Events.Unity;
using FastUnityCreationKit.Unity.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FastUnityCreationKit.Unity
{
    /// <summary>
    /// Base class for all MonoBehaviours compatible with the FastUnityCreationKit.
    /// Used to automatically handle interface processing.
    /// </summary>
    public abstract class FastMonoBehaviour<TSelf> : FastMonoBehaviour
        where TSelf : FastMonoBehaviour<TSelf>, new()
    {
        /// <summary>
        /// Avoid overriding the Awake method. Implement the <see cref="IInitializable"/> interface instead
        /// and use the <see cref="IInitializable.OnInitialize"/> method.
        /// </summary>
        protected virtual void Awake()
        {
            // Register this object to the object registry.
            FastMonoBehaviourManager.Instance.RegisterFastMonoBehaviour(this);

            // Check if supports persistent interface.
            if (this is IPersistent persistent)
            {
                // Ensure that object is on the root level as Unity only allows root level objects to be made persistent.
                if (transform.parent)
                {
                    transform.SetParent(null);

                    // Log warning in editor's console to notify the user.
#if UNITY_EDITOR
                    Debug.LogWarning($"Persistent object {name} is not on the root level. This may cause unexpected behavior." +
                                     $"Fail-safe triggered: moving object to root layer.", this);
#endif
                }

                // Make the object persistent.
                DontDestroyOnLoad(gameObject);
            }

            // Initialize the object if it implements the IInitializable interface.
            if (this is IInitializable initializable)
            {
                initializable.Initialize();

                // Trigger the OnObjectInitialized event.
                OnObjectInitializedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourEventData<TSelf>((this as TSelf)!));
            }

            // Call creation event to notify listeners that the object has been created.
            OnObjectCreatedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourEventData<TSelf>((this as TSelf)!));

            // Check if this object supports "create" callback.
            if (this is ICreateCallback createCallback)
                createCallback.OnObjectCreated();

            // If this is both clickable and selectable print warning.
            if (this is IClickable<TSelf> and ISelectable)
                Debug.LogWarning("Object is both clickable and selectable. This may cause unexpected behavior.");
        }

        protected void OnEnable()
        {
            // Trigger the OnObjectActivated event.
            OnObjectActivatedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourEventData<TSelf>((this as TSelf)!));
        }

        protected void OnDisable()
        {
            // Trigger the OnObjectDeactivated event.
            OnObjectDeactivatedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourEventData<TSelf>((this as TSelf)!));
        }

        protected void OnDestroy()
        {
            // Trigger the OnObjectDestroyed event.
            OnObjectDestroyedEvent<TSelf>.TriggerEvent(new FastMonoBehaviourEventData<TSelf>((this as TSelf)!));

            // Callback must be called after event to prevent weird behavior.
            if (this is IDestroyCallback destroyCallback)
                destroyCallback.OnObjectDestroyed();

            // Unregister this object from the object registry.
            FastMonoBehaviourManager.Instance.UnregisterFastMonoBehaviour(this);
        }
    }

    public abstract class FastMonoBehaviour : MonoBehaviour
    {
    }
}