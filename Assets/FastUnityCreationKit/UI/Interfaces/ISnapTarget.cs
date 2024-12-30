namespace FastUnityCreationKit.UI.Interfaces
{
    /// <summary>
    /// Represents a snap target.
    /// </summary>
    public interface ISnapTarget
    {
        public bool HasSnappedObject { get; }
        
        public bool MultipleSnapsPossible { get; }
        
        public bool CanBeSnappedTo { get; }
    }
}