using FastUnityCreationKit.UI.Extensions;

namespace FastUnityCreationKit.UI.Features
{
    /// <summary>
    /// Enforces constraints to limit the object position to the viewport bounds.
    /// </summary>
    public sealed class ConstrainedToViewportFeature : UIFeature
    {
        private void LateUpdate()
        {
            objectBaseReference.FitIntoViewport();
        }
    }
}