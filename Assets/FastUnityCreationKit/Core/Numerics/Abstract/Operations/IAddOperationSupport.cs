using System.Runtime.CompilerServices;

namespace FastUnityCreationKit.Core.Numerics.Abstract.Operations
{
    /// <summary>
    /// Represents that numbers supports addition operation
    /// </summary>
    public interface IAddOperationSupport<in TRightHandSide, out TResult>
        where TRightHandSide : INumber
    {
        /// <summary>
        /// Add another number
        /// </summary>
        public TResult Add(TRightHandSide rightHandSide);
    }
}