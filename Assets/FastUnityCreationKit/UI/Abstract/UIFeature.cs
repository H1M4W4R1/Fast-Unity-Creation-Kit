using FastUnityCreationKit.Structure.Initialization;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Callbacks;
using UnityEngine;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// Feature that can be added to a UI object.
    /// </summary>
    [RequireComponent(typeof(UIObjectBase))]
    public abstract class UIFeature : UIBehaviour, ICreateCallback, IDestroyCallback, IInitializable
    {
        bool IInitializable.InternalInitializationStatusStorage { get; set; }

        /// <summary>
        /// Reference to the object this feature is attached to.
        /// </summary>
        protected UIObjectBase objectBaseReference;

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

        void IInitializable.OnInitialize()
        {
            objectBaseReference = GetComponent<UIObjectBase>();
        }
    }
}