using FastUnityCreationKit.UI.Abstract;
using UnityEngine;

namespace FastUnityCreationKit.UI
{
    /// <summary>
    /// Represents a data context binder utility.
    /// </summary>
    public static class DataContextBinderUtility
    {
        /// <summary>
        /// Tries to automatically bind the data context to the UI object.
        /// </summary>
        public static void TryToAutomaticallyBindDataContext<TDataContext>(UIObject uiObject) 
            where TDataContext : class, IDataContext, new()
        {
            // Ensure this is a UI object with data context
            if (uiObject is not IUIObjectWithDataContext<TDataContext> uiObjectWithData)
            {
                // Log error
                Debug.LogError("This object does not have proper data context."); 
                return;
            }
            
            uiObjectWithData._TryAutomaticContextBinding();
        }
        
        public static void TryToAutomaticallyBindMultipleDataContexts<TDataContext1, TDataContext2>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
        {
            TryToAutomaticallyBindDataContext<TDataContext1>(uiObject);
            TryToAutomaticallyBindDataContext<TDataContext2>(uiObject);
        }
        
        public static void TryToAutomaticallyBindMultipleDataContexts<TDataContext1, TDataContext2, TDataContext3>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
            where TDataContext3 : class, IDataContext, new()
        {
            TryToAutomaticallyBindDataContext<TDataContext1>(uiObject);
            TryToAutomaticallyBindDataContext<TDataContext2>(uiObject);
            TryToAutomaticallyBindDataContext<TDataContext3>(uiObject);
        }
        
        public static void TryToAutomaticallyBindMultipleDataContexts<TDataContext1, TDataContext2, TDataContext3, TDataContext4>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
            where TDataContext3 : class, IDataContext, new()
            where TDataContext4 : class, IDataContext, new()
        {
            TryToAutomaticallyBindDataContext<TDataContext1>(uiObject);
            TryToAutomaticallyBindDataContext<TDataContext2>(uiObject);
            TryToAutomaticallyBindDataContext<TDataContext3>(uiObject);
            TryToAutomaticallyBindDataContext<TDataContext4>(uiObject);
        }
        
        public static void TryToAutomaticallyBindMultipleDataContexts<TDataContext1, TDataContext2, TDataContext3, TDataContext4, TDataContext5>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
            where TDataContext3 : class, IDataContext, new()
            where TDataContext4 : class, IDataContext, new()
            where TDataContext5 : class, IDataContext, new()
        {
            TryToAutomaticallyBindDataContext<TDataContext1>(uiObject);
            TryToAutomaticallyBindDataContext<TDataContext2>(uiObject);
            TryToAutomaticallyBindDataContext<TDataContext3>(uiObject);
            TryToAutomaticallyBindDataContext<TDataContext4>(uiObject);
            TryToAutomaticallyBindDataContext<TDataContext5>(uiObject);
        }
        
        public static void AttachDataContextEvents<TDataContext>(UIObject uiObject) 
            where TDataContext : class, IDataContext, new()
        {
            // Ensure this is a UI object with data context
            if (uiObject is not IUIObjectWithDataContext<TDataContext> uiObjectWithData)
            {
                // Log error
                Debug.LogError("This object does not have proper data context."); 
                return;
            }
            
            uiObjectWithData._AttachDataContextEvents();
        }
        
        public static void AttachMultipleDataContextEvents<TDataContext1, TDataContext2>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
        {
            AttachDataContextEvents<TDataContext1>(uiObject);
            AttachDataContextEvents<TDataContext2>(uiObject);
        }
        
        public static void AttachMultipleDataContextEvents<TDataContext1, TDataContext2, TDataContext3>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
            where TDataContext3 : class, IDataContext, new()
        {
            AttachDataContextEvents<TDataContext1>(uiObject);
            AttachDataContextEvents<TDataContext2>(uiObject);
            AttachDataContextEvents<TDataContext3>(uiObject);
        }
        
        public static void AttachMultipleDataContextEvents<TDataContext1, TDataContext2, TDataContext3, TDataContext4>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
            where TDataContext3 : class, IDataContext, new()
            where TDataContext4 : class, IDataContext, new()
        {
            AttachDataContextEvents<TDataContext1>(uiObject);
            AttachDataContextEvents<TDataContext2>(uiObject);
            AttachDataContextEvents<TDataContext3>(uiObject);
            AttachDataContextEvents<TDataContext4>(uiObject);
        }
        
        public static void AttachMultipleDataContextEvents<TDataContext1, TDataContext2, TDataContext3, TDataContext4, TDataContext5>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
            where TDataContext3 : class, IDataContext, new()
            where TDataContext4 : class, IDataContext, new()
            where TDataContext5 : class, IDataContext, new()
        {
            AttachDataContextEvents<TDataContext1>(uiObject);
            AttachDataContextEvents<TDataContext2>(uiObject);
            AttachDataContextEvents<TDataContext3>(uiObject);
            AttachDataContextEvents<TDataContext4>(uiObject);
            AttachDataContextEvents<TDataContext5>(uiObject);
        }
        
        public static void DetachDataContextEvents<TDataContext>(UIObject uiObject) 
            where TDataContext : class, IDataContext, new()
        {
            // Ensure this is a UI object with data context
            if (uiObject is not IUIObjectWithDataContext<TDataContext> uiObjectWithData)
            {
                // Log error
                Debug.LogError("This object does not have proper data context."); 
                return;
            }
            
            uiObjectWithData._DetachDataContextEvents();
        }
        
        public static void DetachMultipleDataContextEvents<TDataContext1, TDataContext2>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
        {
            DetachDataContextEvents<TDataContext1>(uiObject);
            DetachDataContextEvents<TDataContext2>(uiObject);
        }
        
        public static void DetachMultipleDataContextEvents<TDataContext1, TDataContext2, TDataContext3>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
            where TDataContext3 : class, IDataContext, new()
        {
            DetachDataContextEvents<TDataContext1>(uiObject);
            DetachDataContextEvents<TDataContext2>(uiObject);
            DetachDataContextEvents<TDataContext3>(uiObject);
        }
        
        public static void DetachMultipleDataContextEvents<TDataContext1, TDataContext2, TDataContext3, TDataContext4>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
            where TDataContext3 : class, IDataContext, new()
            where TDataContext4 : class, IDataContext, new()
        {
            DetachDataContextEvents<TDataContext1>(uiObject);
            DetachDataContextEvents<TDataContext2>(uiObject);
            DetachDataContextEvents<TDataContext3>(uiObject);
            DetachDataContextEvents<TDataContext4>(uiObject);
        }
        
        public static void DetachMultipleDataContextEvents<TDataContext1, TDataContext2, TDataContext3, TDataContext4, TDataContext5>(UIObject uiObject) 
            where TDataContext1 : class, IDataContext, new()
            where TDataContext2 : class, IDataContext, new()
            where TDataContext3 : class, IDataContext, new()
            where TDataContext4 : class, IDataContext, new()
            where TDataContext5 : class, IDataContext, new()
        {
            DetachDataContextEvents<TDataContext1>(uiObject);
            DetachDataContextEvents<TDataContext2>(uiObject);
            DetachDataContextEvents<TDataContext3>(uiObject);
            DetachDataContextEvents<TDataContext4>(uiObject);
            DetachDataContextEvents<TDataContext5>(uiObject);
        }

        
        
        
    }
}