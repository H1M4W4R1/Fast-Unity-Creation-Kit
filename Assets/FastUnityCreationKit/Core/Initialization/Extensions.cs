namespace FastUnityCreationKit.Core.Initialization
{
    /// <summary>
    /// Extensions for the initialization system.
    /// </summary>
    public static class Extensions 
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