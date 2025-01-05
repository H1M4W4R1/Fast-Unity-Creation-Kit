using FastUnityCreationKit.Unity.Features;
using FastUnityCreationKit.Unity.Interfaces.Callbacks.Local;
using UnityEngine;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    ///     Feature that can be added to a UI object.
    /// </summary>
    [RequireComponent(typeof(UIObjectBase))]
    public abstract class UIObjectFeatureBase : CKFeatureBase<UIObjectBase>, IOnObjectCreatedCallback, IOnObjectDestroyedCallback
    {
        public void OnObjectCreated()
        {
            Setup();
        }

        public void OnObjectDestroyed()
        {
            Teardown();
        }

        /// <summary>
        ///     Called when the object is created.
        /// </summary>
        public virtual void Setup()
        {
        }

        /// <summary>
        ///     Called when the object is destroyed.
        /// </summary>
        public virtual void Teardown()
        {
        }
    }
}