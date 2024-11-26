using FastUnityCreationKit.Status.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Status.References
{
    public struct AppliedStatusReference
    {
        /// <summary>
        /// Pointer to status that is applied.
        /// </summary>
        [NotNull] internal readonly StatusBase status;

        /// <summary>
        /// Level of the status. Interpreted differently based on the status type.
        /// </summary>
        internal long statusLevel;

        public AppliedStatusReference([NotNull] StatusBase status, long statusLevel = 0)
        {
            this.status = status;
            this.statusLevel = statusLevel;
        }

        // TODO: Status level based on status type, check if should be removed
    }
}