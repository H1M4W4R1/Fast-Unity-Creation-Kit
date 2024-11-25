using FastUnityCreationKit.Context.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Status.Context
{
	/// <summary>
	/// Represents a status context.
	/// </summary>
	/// <typeparam name="TStatusType">The type of the status.</typeparam>
	public interface IStatusContext<TStatusType> : IStatusContext
		where TStatusType : IStatus, new()
	{
		public TStatusType Status { get; }
	}
	
	/// <summary>
	/// This context is used to represent a status context.
	/// </summary>
    public interface IStatusContext : IContext
    {
	    /// <summary>
	    /// Reference to the object that has the status.
	    /// </summary>
	    [NotNull]
	    public IObjectWithStatus ObjectReference { get; }
        
    }
}