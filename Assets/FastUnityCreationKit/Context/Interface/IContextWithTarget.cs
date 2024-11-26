namespace FastUnityCreationKit.Context.Interface
{
    /// <summary>
    /// Represents context with target.
    /// </summary>
    public interface IContextWithTarget<out TTargetType>
    {
        /// <summary>
        /// Target of the context.
        /// </summary>
        public TTargetType Target { get; }
        
    }
}