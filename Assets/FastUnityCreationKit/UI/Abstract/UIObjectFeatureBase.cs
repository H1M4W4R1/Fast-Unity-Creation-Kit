using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.Unity.Features;
using FastUnityCreationKit.Unity.Interfaces.Callbacks;
using UnityEngine;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// Feature that can be added to a UI object.
    /// </summary>
    [RequireComponent(typeof(UIObjectBase))]
    public abstract class UIObjectFeatureBase : CKFeatureBase<UIObjectBase>, ICreateCallback, IDestroyCallback, IInitializable
    {

        /// <summary>
        /// Called when the object is created.
        /// </summary>
        public virtual void Setup()
        {
        }

        /// <summary>
        /// Called when the object is destroyed.
        /// </summary>
        public virtual void Teardown()
        {
        }

        public void OnObjectCreated() => Setup();
        public void OnObjectDestroyed() => Teardown();

   
    }
}