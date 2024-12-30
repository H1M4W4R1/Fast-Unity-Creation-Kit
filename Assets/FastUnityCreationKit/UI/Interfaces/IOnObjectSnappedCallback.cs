using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Features;
using FastUnityCreationKit.UI.Features.Snapping;

namespace FastUnityCreationKit.UI.Interfaces
{
    /// <summary>
    /// Callback for snapping to an object.
    /// </summary>
    public interface IOnObjectSnappedCallback<TSnapObject> 
        where TSnapObject : UIObjectBase, ISnapTarget<TSnapObject>
    {
        public void OnSnapBreak(SnapToFeatureBase<TSnapObject> objectBrokenFromSnap);
        public void OnSnap(SnapToFeatureBase<TSnapObject> objectSnapped);
    }
}