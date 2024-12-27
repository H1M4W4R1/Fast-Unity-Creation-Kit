using FastUnityCreationKit.UI.Data.Text;
using FastUnityCreationKit.UI.Elements.Basic;
using UnityEngine;

namespace FastUnityCreationKit.UI.Elements.Utility
{
    /// <summary>
    /// Displays the build version string.
    /// </summary>
    [RequireComponent(typeof(BuildVersionStringContext))] // Add context automatically
    public sealed class BuildVersionText : UIText<BuildVersionText, BuildVersionStringContext>
    {
        
    }
}