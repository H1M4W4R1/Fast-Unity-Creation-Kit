using FastUnityCreationKit.UI.Abstract;
using UnityEngine;

namespace FastUnityCreationKit.UI
{
    /// <summary>
    /// Represents a local data context.
    /// Local data context is stored on the same object as the UI object.
    /// </summary>
    /// <remarks>
    /// It is recommended to add <see cref="RequireComponent"/> attribute to the class
    /// that requires this data context.
    /// </remarks>
    public abstract class LocalDataContext<TSelf> : LocalDataContext
        where TSelf : LocalDataContext<TSelf>, new()
    {
       
    }
    
    public abstract class LocalDataContext : MonoBehaviour, IDataContext
    {
        public virtual void OnBind(UIObject uiObject)
        {
            // Do nothing
        }
    }
}