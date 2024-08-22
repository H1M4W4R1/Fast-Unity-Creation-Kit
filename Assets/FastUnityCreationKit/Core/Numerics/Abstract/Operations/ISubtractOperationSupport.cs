using System.Runtime.CompilerServices;

namespace FastUnityCreationKit.Core.Numerics.Abstract.Operations
{
    /// <summary>
    /// Represents that numbers supports subtraction operation
    /// </summary>
    public interface ISubtractOperationSupport<in TRightHandSide, out TResult>
        where TRightHandSide : INumber
    {
        /// <summary>
        /// Subtract another number from this number
        /// </summary>
        public TResult Subtract(TRightHandSide rightHandSide);
    }
}