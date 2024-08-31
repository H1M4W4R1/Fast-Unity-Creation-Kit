namespace FastUnityCreationKit.Core.Utility.Initialization
{
    /// <summary>
    /// Extensions for the initialization system.
    /// </summary>
    public static class InitializationExtensions 
    {
        /// <summary>
        /// Initializes the object.
        /// </summary>
        public static void Initialize<TInitializable>(this TInitializable initializable)
            where TInitializable : IInitializable
        {
            // Ensure that the object is initialized
            initializable.EnsureInitialized();
        }
        
    }
}