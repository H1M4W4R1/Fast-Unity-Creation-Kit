using System.Runtime.CompilerServices;
using FastUnityCreationKit.Status.Abstract;

namespace FastUnityCreationKit.Status
{
    public static class StatusAPI
    {
        /// <summary>
        /// Get definition of status of type <typeparamref name="TStatusBase"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TStatusBase GetStatusDefinition<TStatusBase>()
            where TStatusBase : StatusBase
        {
            return StatusDatabase.Instance.GetStatus<TStatusBase>();
        }
    }
}