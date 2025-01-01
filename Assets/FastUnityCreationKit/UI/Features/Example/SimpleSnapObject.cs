using FastUnityCreationKit.UI.Features.Snapping;

namespace FastUnityCreationKit.UI.Features.Example
{
    public sealed class SimpleSnapObject : SnapToFeatureBase<SimpleSnapTargetFeature>
    {
        protected override bool StartSnapped => true;
    }
}