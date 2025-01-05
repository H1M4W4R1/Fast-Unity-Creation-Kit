using System;

namespace FastUnityCreationKit.Core.Extensions.Enums
{
    /// <summary>
    ///     Enum used to define search mode for types.
    ///     It is used to filter types in search operations depending on
    ///     their generic parameters.
    /// </summary>
    [Flags]
    public enum TypeSearchMode
    {
        None = 0,
        
        // Non-generic types: generic parameters don't exist
        NonGeneric = 1 << 0,
        
        // Generic types: generic parameters exist and are not set
        Generic = 1 << 1,
        
        // Valid types, if generic parameters are not set will be skipped
        Valid = 1 << 2,
        
        // Include self type
        IncludeSelf = 1 << 3,
     
        // Valid generic types (in case there are non-generic markers)
        ValidGeneric = Valid | Generic,
        
        // All types - non-generic and valid generic types
        All = NonGeneric | Generic | Valid
    }
}