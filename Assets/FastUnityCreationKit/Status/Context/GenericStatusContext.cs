using JetBrains.Annotations;
using UnityEngine;

namespace FastUnityCreationKit.Status.Context
{
    public struct GenericStatusContext<TStatusType> : IStatusContext<TStatusType>
        where TStatusType : IStatus, new()
    {
        public GenericStatusContext([NotNull] IObjectWithStatus objectReference)
        {
            ObjectReference = objectReference;


            // Check if entity has status
            Status = objectReference.HasStatus<TStatusType>()
                ? objectReference.GetStatus<TStatusType>()
                : new TStatusType();
        }

        public GenericStatusContext([NotNull] IObjectWithStatus objectReference, [NotNull] TStatusType status)
        {
            ObjectReference = objectReference;

            // Check if entity has status
            if (objectReference.HasStatus<TStatusType>())
            {
                Status = objectReference.GetStatus<TStatusType>();
#if UNITY_EDITOR
                Debug.LogWarning("Entity already has status of type " + typeof(TStatusType).Name +
                                 ". Using existing status.");
#endif
            }
            else
                Status = status;
        }

        public IObjectWithStatus ObjectReference { get; }
        public TStatusType Status { get; }
    }
}