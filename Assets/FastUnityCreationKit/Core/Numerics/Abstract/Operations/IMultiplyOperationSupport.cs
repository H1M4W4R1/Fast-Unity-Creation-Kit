using System.Runtime.CompilerServices;

namespace FastUnityCreationKit.Core.Numerics.Abstract.Operations
{
    /// <summary>
    /// Represents that numbers supports multiplication operation
    /// </summary>
    public interface IMultiplyOperationSupport<in TRightHandSide, out TResult>
        where TRightHandSide : INumber
    {
        /// <summary>
        /// Multiply by another number
        /// </summary>
        public TResult Multiply(TRightHandSide rightHandSide);
    }
}