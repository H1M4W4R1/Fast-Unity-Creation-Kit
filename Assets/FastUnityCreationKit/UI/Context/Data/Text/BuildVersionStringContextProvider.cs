using JetBrains.Annotations;
using UnityEngine.Device;

namespace FastUnityCreationKit.UI.Context.Data.Text
{
    /// <summary>
    ///     This context is used to store build version string.
    ///     Always returns the build version string (<see cref="Application.version" />)
    /// </summary>
    public sealed class BuildVersionStringContextProvider : StringContextBaseProvider
    {
        [NotNull] public override string Provide()
        {
            return Application.version;
        }
    }
}