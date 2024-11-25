using FastUnityCreationKit.Core.Utility.Initialization;
using FastUnityCreationKit.Core.Utility.Singleton;
using FastUnityCreationKit.Unity.Events.Interfaces;

namespace FastUnityCreationKit.Unity.Structure.Managers
{
    /// <summary>
    /// This class represents a manager that is used to manage a specific feature in the game.
    /// One manager should be dedicated for one feature. Do not break SOLID principles. <br/>
    /// Most systems of FastUnityCreationKit are avoiding the use of managers, but it is implemented
    /// to provide a way to manage features that are not possible to be implemented in a different way.
    /// </summary>
    /// <typeparam name="TManagerType">Type that represents the manager.</typeparam>
    public abstract class FastManager<TManagerType> : FastMonoBehaviour<TManagerType>,
        IMonoBehaviourSingleton<TManagerType> where TManagerType : FastManager<TManagerType>, new()
    {
        /// <summary>
        /// Gets the instance of the manager.
        /// </summary>
        public static TManagerType Instance => IMonoBehaviourSingleton<TManagerType>.GetInstance();
    }
}