using System.Runtime.CompilerServices;

namespace FastUnityCreationKit.Core.Numerics.Abstract.Operations
{
    /// <summary>
    /// Represents that numbers supports division operation
    /// </summary>
    public interface IDivideOperationSupport<in TRightHandSide, out TResult>
        where TRightHandSide : INumber
    {
        /// <summary>
        /// Divide this number by another number
        /// </summary>
        public TResult Divide(TRightHandSide rightHandSide);
    }
}