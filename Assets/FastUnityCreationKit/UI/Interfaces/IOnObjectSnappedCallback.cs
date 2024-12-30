using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Features;
using FastUnityCreationKit.UI.Features.Snapping;

namespace FastUnityCreationKit.UI.Interfaces
{
    /// <summary>
    /// Callback for snapping to an object.
    /// </summary>
    public interface IOnObjectSnappedCallback<TSnapObject> 
        where TSnapObject : UIObject, ISnapTarget
    {
        public void OnSnapBreak(SnapToFeature<TSnapObject> objectBrokenFromSnap);
        public void OnSnap(SnapToFeature<TSnapObject> objectSnapped);
    }
}