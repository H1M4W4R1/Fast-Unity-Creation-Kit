namespace FastUnityCreationKit.Economy.Abstract
{
    /// <summary>
    /// Represents a global resource.
    /// </summary>
    public interface IGlobalResource<out TSelf> where TSelf : IGlobalResource<TSelf>
    {
        /// <summary>
        /// Get a global reference to the resource from instance point of view.
        /// </summary>
        public TSelf GetGlobalReference();
    }
}