using System;
using System.Collections.Generic;
using FastUnityCreationKit.Structure.Singleton;
using UnityEngine;

namespace FastUnityCreationKit.Unity
{
    /// <summary>
    /// This class is implementation of FastMonoBehaviour processing.
    /// Should not be used directly.
    /// </summary>
    public sealed class FastMonoBehaviourManager : MonoBehaviour,
        IMonoBehaviourSingleton<FastMonoBehaviourManager> // Can't be FMB to prevent infinite loop
    {
        /// <summary>
        /// If true it means the object was destroyed.
        /// It's used to prevent accidental instance creation when
        /// quitting the application.
        /// </summary>
        public static bool WasDestroyed { get; set; }

        public static FastMonoBehaviourManager Instance =>
            IMonoBehaviourSingleton<FastMonoBehaviourManager>.GetInstance();
        
        // We can't use Time.timeScale because when it's 0 then this
        // object won't receive updates. We need to create a custom
        // pause system to handle this.
        public bool IsTimePaused => TimeAPI.IsTimePaused;
        
        /// <summary>
        /// List of all known FastMonoBehaviours in the scene.
        /// </summary>
        private readonly List<FastMonoBehaviour> _fastMonoBehaviours = new();

        /// <summary>
        /// Returns the first object found in the scene of the specified type.
        /// </summary>
        internal void RegisterFastMonoBehaviour(FastMonoBehaviour fastMonoBehaviour)
        {
            if (!_fastMonoBehaviours.Contains(fastMonoBehaviour))
                _fastMonoBehaviours.Add(fastMonoBehaviour);
        }

        /// <summary>
        /// Removes the specified object from the list of known FastMonoBehaviours.
        /// </summary>
        internal void UnregisterFastMonoBehaviour(FastMonoBehaviour fastMonoBehaviour)
        {
            if (_fastMonoBehaviours.Contains(fastMonoBehaviour))
                _fastMonoBehaviours.Remove(fastMonoBehaviour);
        }

        /// <summary>
        /// Used to set up the instance of the FastMonoBehaviourManager.
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
        /// This is main update loop of the FastMonoBehaviour system.
        /// This allows for quick updating of multiple objects based on their interfaces.
        /// May be slightly slower (shouldn't be due to lack of P-Invoke operations) than Unity's built-in update loop,
        /// but allows for more control over the update process.
        /// </summary>
        private void Update()
        {
            // Get the delta time between frames
            // including time scale
            float deltaTime = TimeAPI.DeltaTime;
            
            // Loop through all FastMonoBehaviours and call their update methods
            ExecuteForAll(FastMonoBehaviour.HandlePreUpdate, deltaTime);
            ExecuteForAll(FastMonoBehaviour.HandleUpdate, deltaTime);
            ExecuteForAll(FastMonoBehaviour.HandlePostUpdate, deltaTime);
        }

        /// <summary>
        /// For more reference, see <see cref="Update"/> method.
        /// </summary>
        public void FixedUpdate()
        {
            ExecuteForAll(FastMonoBehaviour.HandleFixedUpdate, TimeAPI.FixedDeltaTime);
        }

        private void ExecuteForAll(Action<FastMonoBehaviour, float> action, float deltaTime)
        {
            float realDeltaTime = TimeAPI.UnscaledDeltaTime;
            float fixedDeltaTime = TimeAPI.FixedDeltaTime;
            float timeSinceStartup = TimeAPI.RealtimeSinceStartup;
            
            // Execute for all known FastMonoBehaviours
            for (int fastMonoBehaviourIndex = 0;
                 fastMonoBehaviourIndex < _fastMonoBehaviours.Count;
                 fastMonoBehaviourIndex++)
            {
                // Get the FastMonoBehaviour
                FastMonoBehaviour fastMonoBehaviour = _fastMonoBehaviours[fastMonoBehaviourIndex];
                
                // Skip destroyed objects
                if(fastMonoBehaviour.IsDestroyed) continue;
                
                // Skip if update is forbidden
                if((fastMonoBehaviour.UpdateMode & UpdateMode.Forbidden) != 0) continue;
                
                // Skip disabled objects if they don't update when disabled
                if(fastMonoBehaviour.IsDisabled && (fastMonoBehaviour.UpdateMode & UpdateMode.UpdateWhenDisabled) != 0) continue;
                
                // Skip objects that don't update when time is paused
                if (IsTimePaused && (fastMonoBehaviour.UpdateMode & UpdateMode.UpdateWhenTimePaused) == 0) continue;
                
                // Execute the action
                action(fastMonoBehaviour, fastMonoBehaviour.UpdateTimeConfig switch
                {
                    UpdateTime.DeltaTime => deltaTime,
                    UpdateTime.UnscaledDeltaTime => realDeltaTime,
                    UpdateTime.RealtimeSinceStartup => timeSinceStartup,
                    UpdateTime.FixedDeltaTime => fixedDeltaTime,
                    _ => throw new ArgumentOutOfRangeException()
                });
            }
        }
    }
}