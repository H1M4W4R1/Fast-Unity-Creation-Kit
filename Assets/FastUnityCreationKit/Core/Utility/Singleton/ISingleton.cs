namespace FastUnityCreationKit.Core.Utility.Singleton
{
    /// <summary>
    /// Represents a singleton object - an object that has only one instance.
    /// This should be only used with regular C# classes. For MonoBehaviours
    /// see <see cref="IMonoBehaviourSingleton{TSelf}"/>.
    /// </summary>
    public interface ISingleton<out TSelf> : ISingleton
        where TSelf : ISingleton<TSelf>, new()
    {
        /// <summary>
        /// The instance of the singleton.
        /// </summary>
        protected static TSelf Instance { get; set; }

        public static TSelf GetInstance()
        {
            // Check if the instance exists
            if(Instance != null) return Instance;
            
            // Create a new instance
            Instance = new TSelf();
            
            // Return the instance
            return Instance;
        }
    }

    public interface ISingleton
    {
        
    }
}