using JetBrains.Annotations;

namespace FastUnityCreationKit.Core.Utility.Singleton
{
    /// <summary>
    /// Represents a singleton object - an object that has only one instance.
    /// This should be only used with regular C# classes. For MonoBehaviours
    /// see <see cref="IMonoBehaviourSingleton{TSelf}"/>.
    /// </summary>
    public interface ISingleton<TSelf> : IUnsafeSingleton<TSelf>
        where TSelf : ISingleton<TSelf>, new()
    {
       
    }
    
    /// <summary>
    /// This represents a singleton object that is not type-safe.
    /// It is strongly recommended to use <see cref="ISingleton{TSelf}"/> instead as
    /// this implementation may cause significant issues if not used properly.
    /// </summary>
    public interface IUnsafeSingleton<TSelf> : ISingleton where TSelf : new()
    {
        /// <summary>
        /// The instance of the singleton.
        /// </summary>
        [CanBeNull] protected static TSelf Instance { get; set; }

        [NotNull] public static TSelf GetInstance()
        {
            // Check if the instance exists
            if(Instance != null) return Instance;
            
            // Create a new instance
            Instance = new TSelf();
            
            // Return the instance
            return Instance!;
        }
    }

    public interface ISingleton
    {
        
    }
}