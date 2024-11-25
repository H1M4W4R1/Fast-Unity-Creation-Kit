using System;
using System.Runtime.CompilerServices;
using Sirenix.Utilities;

namespace FastUnityCreationKit.Core.Utility.Internal
{
	/// <summary>
	/// This class is responsible for validation of objects in Unity Editor.
	/// </summary>
    public static class Validation
    {
	    /// <summary>
	    /// This ensures that SourceType inherits from RequestedType.
	    /// </summary>
	    /// <typeparam name="TSourceType">Source type.</typeparam>
	    /// <typeparam name="TRequestedType">Requested type.</typeparam>
	    /// <exception cref="ArgumentException">When the SourceType does not inherit from RequestedType.</exception>
	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static void AssertType<TSourceType, TRequestedType>()
	    {
		    #if UNITY_EDITOR
		    if(!typeof(TSourceType).ImplementsOrInherits(typeof(TRequestedType)))
			    throw new ArgumentException($"The type must inherit from {nameof(TRequestedType)}.");
		    
		    #endif
	    }
	    
	    /// <summary>
	    /// Ensures that SourceType does not inherit from RequestedType.
	    /// </summary>
	    /// <typeparam name="TSourceType">Source type.</typeparam>
	    /// <typeparam name="TRequestedType">Requested type.</typeparam>
	    /// <exception cref="ArgumentException">When the SourceType inherits from RequestedType.</exception>
	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
	    public static void AssertNotType<TSourceType, TRequestedType>()
	    {
		    #if UNITY_EDITOR
		    if(typeof(TSourceType).ImplementsOrInherits(typeof(TRequestedType)))
			    throw new ArgumentException($"The type must not inherit from {nameof(TRequestedType)}.");
		    
		    #endif
	    }
        
    }
}