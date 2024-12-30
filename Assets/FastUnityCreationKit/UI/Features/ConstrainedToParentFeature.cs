using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Extensions;
using UnityEngine;

namespace FastUnityCreationKit.UI.Features
{
    /// <summary>
    /// Enforces constraints to limit the object position to the parent bounds.
    /// </summary>
    public sealed class ConstrainedToParentFeature : UIFeature
    {
        private void LateUpdate()
        {
            objectReference.FitIntoParent();
        }

    }
}