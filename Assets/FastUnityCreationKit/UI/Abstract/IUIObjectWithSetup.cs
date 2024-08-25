namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// Represents a UI object with setup.
    /// </summary>
    public interface IUIObjectWithSetup
    {
        /// <summary>
        /// Sets up the object.
        /// </summary>
        public void Setup();
        
        /// <summary>
        /// Tears down the object.
        /// </summary>
        public void TearDown();
        
    }
}