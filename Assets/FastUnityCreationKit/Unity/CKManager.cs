using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Structure.Singleton;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity
{
    /// <summary>
    /// This class represents a manager that is used to manage a specific feature in the game.
    /// One manager should be dedicated for one feature. Do not break SOLID principles. <br/>
    /// Most systems of FastUnityCreationKit are avoiding the use of managers, but it is implemented
    /// to provide a way to manage features that are not possible to be implemented in a different way.
    /// </summary>
    /// <typeparam name="TManagerType">Type that represents the manager.</typeparam>
    public abstract class CKManager<TManagerType> : CKMonoBehaviour,
        IMonoBehaviourSingleton<TManagerType> where TManagerType : CKManager<TManagerType>, new()
    {
        protected override void Awake()
        {
            // Check if types match
            if (GetType() != typeof(TManagerType))
                Guard<ValidationLogConfig>.Fatal("Type mismatch. Type of the manager should be the same as the generic type.");
            
            base.Awake();
        }

        /// <summary>
        /// Gets the instance of the manager.
        /// </summary>
        [NotNull] public static TManagerType Instance => IMonoBehaviourSingleton<TManagerType>.GetInstance();
    }
}