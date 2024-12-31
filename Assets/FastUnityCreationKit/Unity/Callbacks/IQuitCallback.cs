namespace FastUnityCreationKit.Unity.Callbacks
{
    /// <summary>
    /// Callback for quitting the application.
    /// </summary>
    public interface IQuitCallback 
    {
        /// <summary>
        /// Invoked when the application is quitting.
        /// </summary>
        void OnQuit();
    }
}