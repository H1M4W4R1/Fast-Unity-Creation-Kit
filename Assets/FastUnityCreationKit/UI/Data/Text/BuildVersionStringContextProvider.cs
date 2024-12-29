using UnityEngine.Device;

namespace FastUnityCreationKit.UI.Data.Text
{
    /// <summary>
    /// This context is used to store build version string.
    /// </summary>
    public sealed class BuildVersionStringContextProvider : StringContextBaseProvider
    {
        public override string Provide() => Application.version;
        
    }
}