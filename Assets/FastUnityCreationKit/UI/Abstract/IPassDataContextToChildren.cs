using UnityEngine;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// Supports passing data context to children.
    /// </summary>
    public interface IPassDataContextToChildren<TData>
        where TData : class, IDataContext, new()
    {
        /// <summary>
        /// Passes the data context to the children.
        /// </summary>
        public void PassDataContextToChildren()
        {
            // Ensure this is a UI object with data context
            if (this is not IUIObjectWithDataContext<TData> uiObjectWithData)
            {
                // Log error
                Debug.LogError("This object does not have proper data context."); 
                return;
            }
            
            if(this is not UIObject uiObject)
            {
                // Log error
                Debug.LogError("This object does not have proper data context.");
                return;
            }
            
            // Pass the data context to the children
            // Get all components that are UIObjectWithData
            IUIObjectWithDataContext<TData>[] childrenUIObjects = 
                uiObject.GetComponentsInChildren<IUIObjectWithDataContext<TData>>();
            
            // Iterate through all the children
            for (int index = 0; index < childrenUIObjects.Length; index++)
            {
                // Pass the data context to the child
                IUIObjectWithDataContext<TData> child = childrenUIObjects[index];
                child.BindDataContext(uiObjectWithData.DataContext);
            }
        }
        
    }
}