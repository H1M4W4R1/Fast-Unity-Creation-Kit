using System.Collections.Generic;
using FastUnityCreationKit.Context.Interface;
using FastUnityCreationKit.Unity;
using FastUnityCreationKit.Unity.Events.Interfaces;
using Sirenix.OdinInspector;

namespace FastUnityCreationKit.UI.Context
{
    /// <summary>
    /// Represents data context for UI objects. Data Context is a <see cref="FastMonoBehaviour"/> class that holds data for the UI object.
    /// This can be referred as a ViewModel in MVVM pattern or a Model in MVC pattern.
    /// </summary>
    /// <remarks>
    /// To separate game data layer from presentation data layer a custom data object should be created. Then you can
    /// attach references to your data object to make sure that the data is updated in the UI object.
    /// </remarks>
    public abstract class DataContext<TSelfSealed> : FastMonoBehaviour<TSelfSealed>, IContext,
        ICreateCallback, IDestroyCallback
        where TSelfSealed : DataContext<TSelfSealed>, new()
    {
        /// <summary>
        /// Check if this context is a reference to another context.
        /// </summary>
        public bool IsReference => this is ReferencedDataContext<TSelfSealed>;
        
        /// <summary>
        /// Represents the dirty state of the data context.
        /// If the data context is dirty, rendered UI should be updated.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Debug")]
        public virtual bool IsDirty { get; protected set; }
        
        /// <summary>
        /// List of linked contexts that are updated when this context is updated.
        /// </summary>
        [ShowInInspector] [ReadOnly] [TabGroup("Debug")]
        public List<DataContext<TSelfSealed>> LinkedContexts { get; } = new List<DataContext<TSelfSealed>>();

        /// <summary>
        /// Link this context to another context.
        /// </summary>
        /// <param name="context">Context to link.</param>
        public void Link(DataContext<TSelfSealed> context)
        {
            if(!LinkedContexts.Contains(context))
                LinkedContexts.Add(context);
        }

        /// <summary>
        /// Unlink this context from another context.
        /// </summary>
        /// <param name="context">Context to unlink.</param>
        public void Unlink(DataContext<TSelfSealed> context)
        {
            if(LinkedContexts.Contains(context))
                LinkedContexts.Remove(context);
        }
        
        /// <summary>
        /// Make this context dirty and update linked contexts.
        /// Call this when anything within this context has been changed and changes are ready to be rendered.
        /// </summary>
        public void MakeDirty()
        {
            IsDirty = true;
            for (int index = 0; index < LinkedContexts.Count; index++)
            {
                DataContext<TSelfSealed> linkedContext = LinkedContexts[index];
                linkedContext.MakeDirty();
            }
        }

        /// <summary>
        /// Setup this context
        /// </summary>
        public virtual void Setup(){}

        /// <summary>
        /// Tear down this context
        /// </summary>
        public virtual void TearDown(){}

        public void OnObjectCreated() => Setup();
        public void OnObjectDestroyed() => TearDown();
        
    }
}