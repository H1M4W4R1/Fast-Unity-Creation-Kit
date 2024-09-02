namespace FastUnityCreationKit.Unity.Interfaces
{
    /// <summary>
    /// Represents an object that can be double clicked.
    /// </summary>
    public interface IDoubleClickable
    {
        /// <summary>
        /// Represents the time of the last click.
        /// </summary>
        internal float LastClickTime { get; set; }

        /// <summary>
        /// Represents the time threshold for a double click.
        /// If the time between two clicks is less than this value, the click is considered a double click.
        /// </summary>
        internal float DoubleClickTimeThreshold { get; }

        /// <summary>
        /// Called when the object is double-clicked.
        /// </summary>
        public void OnDoubleClick();
    }
}