using FastUnityCreationKit.Core.Utility.Initialization;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Events.Interfaces;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// The base class for all UI objects in Fast Unity Creation Kit.
    /// </summary>
    public abstract class UIObject : FastMonoBehaviour, IUpdateCallback, IInitializable
    {
        bool IInitializable.InternalInitializationStatusStorage { get; set; }

        public void OnObjectUpdated(float deltaTime)
        {
            // TODO: Refresh if context is implemented and dirty
        }
        
        void IInitializable.OnInitialize()
        {
           // TODO: Initialize if supports setup for current object
        }
    }
}