using System;
using System.Collections.Generic;
using FastUnityCreationKit.Structure.Singleton;
using FastUnityCreationKit.Unity.Events;
using FastUnityCreationKit.Unity.Time;
using FastUnityCreationKit.Unity.Time.Enums;
using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Unity
{
    /// <summary>
    /// This class is implementation of <see cref="CKMonoBehaviour"/> processing.
    /// Should not be used directly.
    /// </summary>
    public sealed class CKEventsManager : MonoBehaviour,
        IMonoBehaviourSingleton<CKEventsManager> // Can't be FMB to prevent infinite loop
    {
        /// <summary>
        /// If true it means the object was destroyed.
        /// It's used to prevent accidental instance creation when
        /// quitting the application.
        /// </summary>
        public static bool WasDestroyed { get; set; }

        [NotNull] public static CKEventsManager Instance =>
            IMonoBehaviourSingleton<CKEventsManager>.GetInstance();
        
        // We can't use Time.timeScale because when it's 0 then this
        // object won't receive updates. We need to create a custom
        // pause system to handle this.
        public bool IsTimePaused => TimeAPI.IsTimePaused;
        
        /// <summary>
        /// List of all known <see cref="CKMonoBehaviour"/> in the scene.
        /// </summary>
        [NotNull] [ItemNotNull] private readonly List<CKMonoBehaviour> _fastMonoBehaviours = new();

        /// <summary>
        /// Returns the first object found in the scene of the specified type.
        /// </summary>
        internal void RegisterFastMonoBehaviour([NotNull] CKMonoBehaviour ckMonoBehaviour)
        {
            if (!_fastMonoBehaviours.Contains(ckMonoBehaviour))
                _fastMonoBehaviours.Add(ckMonoBehaviour);
        }

        /// <summary>
        /// Removes the specified object from the list of known <see cref="CKMonoBehaviour"/>.
        /// </summary>
        internal void UnregisterFastMonoBehaviour([NotNull] CKMonoBehaviour ckMonoBehaviour)
        {
            if (_fastMonoBehaviours.Contains(ckMonoBehaviour))
                _fastMonoBehaviours.Remove(ckMonoBehaviour);
        }

        /// <summary>
        /// Used to set up the instance of the <see cref="CKEventsManager"/>.
        /// </summary>
        private void Awake()
        {
            // Ensure this is on the root level, otherwise Unity may throw an error with DontDestroyOnLoad
            // as only root level objects can be made persistent
            transform.SetParent(null);

            // Make the object persistent
            DontDestroyOnLoad(gameObject);

            // Note that we're not setting instance here because it's handled by the singleton system
            // for more information see IMonoBehaviourSingleton.cs
            WasDestroyed = false;
        }

        private void OnDestroy()
        {
            WasDestroyed = true;
        }

        /// <summary>
        /// This is main update loop of the <see cref="CKMonoBehaviour"/> system.
        /// This allows for quick updating of multiple objects based on their interfaces.
        /// May be slightly slower (shouldn't be due to lack of P-Invoke operations) than Unity's built-in update loop,
        /// but allows for more control over the update process.
        /// </summary>
        private void Update()
        {
            // Get the delta time between frames
            // including time scale
            float deltaTime = TimeAPI.DeltaTime;
            
            // Loop through all behaviours and call their update methods
            ExecuteForAll(CKMonoBehaviour.HandleTemporaryObject, deltaTime);
            ExecuteForAll(CKMonoBehaviour.HandlePreUpdate, deltaTime);
            ExecuteForAll(CKMonoBehaviour.HandleUpdate, deltaTime);
            ExecuteForAll(CKMonoBehaviour.HandlePostUpdate, deltaTime);
            
            // Call the OnFrameRenderedEvent
            OnFrameRenderedEvent.TriggerEvent();
        }

        /// <summary>
        /// For more reference, see <see cref="Update"/> method.
        /// </summary>
        public void FixedUpdate()
        {
            ExecuteForAll(CKMonoBehaviour.HandleFixedUpdate, TimeAPI.FixedDeltaTime);
            
            // Call the OnFixedFrameRenderedEvent
            OnFixedFrameRenderedEvent.TriggerEvent();
        }

        private void ExecuteForAll(Action<CKMonoBehaviour, float> action, float deltaTime)
        {
            float realDeltaTime = TimeAPI.UnscaledDeltaTime;
            float timeSinceStartup = TimeAPI.RealtimeSinceStartup;
            
            for (int fastMonoBehaviourIndex = 0; fastMonoBehaviourIndex < _fastMonoBehaviours.Count;
                 fastMonoBehaviourIndex++)
            { 
                CKMonoBehaviour ckMonoBehaviour = _fastMonoBehaviours[fastMonoBehaviourIndex];
                
                // Skip destroyed objects
                if(ckMonoBehaviour.IsDestroyed) continue;
                
                // Skip if update is forbidden
                if((ckMonoBehaviour.UpdateMode & UpdateMode.Forbidden) != 0) continue;
                
                // Skip disabled objects if they don't update when disabled
                if(ckMonoBehaviour.IsDisabled && (ckMonoBehaviour.UpdateMode & UpdateMode.UpdateWhenDisabled) == 0) continue;
                
                // Skip objects that don't update when time is paused
                if (IsTimePaused && (ckMonoBehaviour.UpdateMode & UpdateMode.UpdateWhenTimePaused) == 0) continue;
                
                // Execute the action
                action(ckMonoBehaviour, ckMonoBehaviour.UpdateTimeConfig switch
                {
                    UpdateTime.DeltaTime => deltaTime,
                    UpdateTime.UnscaledDeltaTime => realDeltaTime,
                    UpdateTime.RealtimeSinceStartup => timeSinceStartup,
                    _ => throw new ArgumentOutOfRangeException()
                });
            }
        }
    }
}