using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Extensions;

namespace FastUnityCreationKit.UI.Features
{
    /// <summary>
    /// Enforces constraints to limit the object position to the parent bounds.
    /// </summary>
    public sealed class ConstrainedToParentFeature : UIFeature
    {
        private void LateUpdate()
        {
            objectBaseReference.FitIntoParent();
        }

    }
}