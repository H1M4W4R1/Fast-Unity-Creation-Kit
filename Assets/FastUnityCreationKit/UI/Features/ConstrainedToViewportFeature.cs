using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Extensions;
using FastUnityCreationKit.Unity;
using UnityEngine;

namespace FastUnityCreationKit.UI.Features
{
    /// <summary>
    /// Enforces constraints to limit the object position to the viewport bounds.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public sealed class ConstrainedToViewportFeature : CKFeatureBase<RectTransform>
    {
        private void LateUpdate()
        {
            FeaturedObject.FitIntoViewport();
        }
    }
}