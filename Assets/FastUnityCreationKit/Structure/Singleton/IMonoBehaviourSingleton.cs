using System;
using FastUnityCreationKit.Utility;
using FastUnityCreationKit.Utility.Logging;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FastUnityCreationKit.Structure.Singleton
{
    /// <summary>
    /// Represents a singleton that is a <see cref="MonoBehaviour"/>.
    /// Use this interface to mark a MonoBehaviour as a singleton.
    /// </summary>
    public interface IMonoBehaviourSingleton<TSelf> : ISingleton<TSelf>
        where TSelf : MonoBehaviour, IMonoBehaviourSingleton<TSelf>, new()
    {
        /// <summary>
        /// Gets the instance of the singleton.
        /// Overwrites <see cref="ISingleton{TSelf}.GetInstance"/>
        /// </summary>
        [NotNull]
        public new static TSelf GetInstance()
        {
            // Ensure that the type is a MonoBehaviour
#if UNITY_EDITOR
            if (!typeof(TSelf).IsSubclassOf(typeof(MonoBehaviour)))
            {
                Guard<EditorAutomationLogConfig>.Error($"Type {typeof(TSelf).Name} is not a MonoBehaviour. " +
                                                       $"For regular C# classes use ISingleton<TSelf>.");
                return default!;
            }
#endif

            // Check if the instance exists
            if (Instance) return Instance;

            // Find the instance
            Instance = Object.FindAnyObjectByType<TSelf>();

            // Check if the instance exists
            if (Instance) return Instance;

            // Create a new instance
            Instance = new GameObject(typeof(TSelf).Name).AddComponent<TSelf>();

            // Return the instance, most likely won't be null as long as TSelf is a MonoBehaviour
            return Instance!;
        }
    }
}