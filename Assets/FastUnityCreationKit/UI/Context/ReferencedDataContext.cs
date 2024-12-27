using System;
using FastUnityCreationKit.UI.Context.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FastUnityCreationKit.UI.Context
{
    /// <summary>
    /// Represents link to another data context.
    /// </summary>
    /// <typeparam name="TSelfSealed">Sealed context type.</typeparam>
    public abstract class ReferencedDataContext<TSelfSealed> : DataContext<TSelfSealed> 
        where TSelfSealed : DataContext<TSelfSealed>, new()
    {
        /// <summary>
        /// Represents type of the reference.
        /// </summary>
        [TabGroup("Configuration")] [ShowInInspector]
        public ContextReferenceType ReferenceType { get; set; }
        
        /// <summary>
        /// Represents the reference to the data context.
        /// </summary>
        [TabGroup("Configuration"), ShowIf(nameof(IsExactReference))] [ShowInInspector]
        public DataContext<TSelfSealed> Reference { get; set; }
        
        [TabGroup("Debug")] [ShowInInspector] [ReadOnly]
        public DataContext<TSelfSealed> PreviewReference => Reference;
        
        public bool IsExactReference => ReferenceType == ContextReferenceType.Exact;

        public sealed override void Setup()
        {
            switch (ReferenceType)
            {
                case ContextReferenceType.Self:
                    Reference = GetComponent<TSelfSealed>();
                    break;
                case ContextReferenceType.OwnParent:
                    Reference = transform.parent?.GetComponent<TSelfSealed>();
                    break;
                case ContextReferenceType.AnyParent:
                {
                    Transform parent = transform.parent;
                    while (parent != null)
                    {
                        Reference = parent.GetComponent<TSelfSealed>();
                        if (Reference != null) break;
                        parent = parent.parent;
                    }

                    break;
                }
                case ContextReferenceType.AnyChild:
                    Reference = transform.GetComponentInChildren<TSelfSealed>();
                    break;
                case ContextReferenceType.FirstChild:
                    Reference = transform.childCount > 1 ? transform.GetChild(0).GetComponent<TSelfSealed>() : null;
                    break;
                case ContextReferenceType.Exact when Reference == null:
                    Debug.LogError("Reference is not set for exact reference type.", this);
                    break;
                default:
                    Reference = null;
                    break;
            }

            // Link to the reference if it is not linked
            if (Reference != null)
                Reference.Link(this);
        }
        
        public sealed override void TearDown()
        {
            // Unlink from the reference if it is linked
            if (Reference != null)
                Reference.Unlink(this);
        }
    }
}