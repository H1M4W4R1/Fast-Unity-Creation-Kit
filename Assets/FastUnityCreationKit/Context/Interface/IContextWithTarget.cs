namespace FastUnityCreationKit.Context.Interface
{
    /// <summary>
    /// Represents context with target.
    /// </summary>
    public interface IContextWithTarget<out TTargetType> : IContextWithTarget
    {
        /// <summary>
        /// Target of the context.
        /// </summary>
        public TTargetType Target { get; }
        
        object IContextWithTarget.RawTarget => Target;
    }

    /// <summary>
    /// Internal interface for context with target.
    /// </summary>
    public interface IContextWithTarget
    {
        public object RawTarget { get; }
        
    }
}