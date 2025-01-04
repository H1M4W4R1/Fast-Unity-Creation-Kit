namespace FastUnityCreationKit.Unity.Interfaces.Callbacks
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