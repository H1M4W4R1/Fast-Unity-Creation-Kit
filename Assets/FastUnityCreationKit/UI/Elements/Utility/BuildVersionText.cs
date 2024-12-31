using FastUnityCreationKit.UI.Context.Data.Text;
using FastUnityCreationKit.UI.Elements.Abstract;
using FastUnityCreationKit.UI.Elements.Base;
using UnityEngine;

namespace FastUnityCreationKit.UI.Elements.Utility
{
    /// <summary>
    /// Displays the build version string.
    /// Automatically adds a <see cref="BuildVersionStringContextProvider"/> to the same GameObject which
    /// is required for this element to work.
    /// </summary>
    [RequireComponent(typeof(BuildVersionStringContextProvider))] 
    public sealed class BuildVersionText : UITextBase
    {
        
    }
}