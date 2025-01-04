using FastUnityCreationKit.UI.Extensions;
using FastUnityCreationKit.Unity.Features;
using UnityEngine;

namespace FastUnityCreationKit.UI.Features
{
    /// <summary>
    /// Enforces constraints to limit the object position to the parent bounds.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public sealed class ConstrainedToParentFeature : CKFeatureBase<RectTransform>
    {
        private void LateUpdate()
        {
            FeaturedObject.FitIntoParent();
        }

    }
}