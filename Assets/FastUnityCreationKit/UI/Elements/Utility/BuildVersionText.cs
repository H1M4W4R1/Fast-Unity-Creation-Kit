using FastUnityCreationKit.UI.Data.Text;
using FastUnityCreationKit.UI.Elements.Abstract;
using UnityEngine;

namespace FastUnityCreationKit.UI.Elements.Utility
{
    /// <summary>
    /// Displays the build version string.
    /// </summary>
    [RequireComponent(typeof(BuildVersionStringContextProvider))] // Add context automatically
    public sealed class BuildVersionText : UIText<BuildVersionText>
    {
        
    }
}