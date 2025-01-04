using FastUnityCreationKit.UI.Abstract;
using FastUnityCreationKit.UI.Features.Snapping;
using JetBrains.Annotations;

namespace FastUnityCreationKit.UI.Interfaces
{
    /// <summary>
    ///     Callback for snapping to an object.
    ///     This callback is performed on <see cref="ISnapTarget{TSelf}" /> that object was snapped to.
    /// </summary>
    public interface IOnObjectSnappedCallback<TSnapObject>
        where TSnapObject : UIObjectBase, ISnapTarget<TSnapObject>
    {
        public void OnSnapBreak([NotNull] SnapToFeatureBase<TSnapObject> objectBrokenFromSnap);
        public void OnSnap([NotNull] SnapToFeatureBase<TSnapObject> objectSnapped);
    }
}