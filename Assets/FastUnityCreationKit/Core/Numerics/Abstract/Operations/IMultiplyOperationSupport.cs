using System.Runtime.CompilerServices;

namespace FastUnityCreationKit.Core.Numerics.Abstract.Operations
{
    /// <summary>
    /// Represents that numbers supports addition operation
    /// </summary>
    public interface IMultiplyOperationSupport<in TRightHandSide, out TResult>
        where TRightHandSide : INumber
    {
        /// <summary>
        /// Add two numbers together
        /// </summary>
        public TResult Multiply(TRightHandSide rightHandSide);
    }
}