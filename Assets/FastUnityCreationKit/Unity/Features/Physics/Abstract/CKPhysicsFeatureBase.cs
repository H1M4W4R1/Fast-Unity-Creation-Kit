namespace FastUnityCreationKit.Unity.Features.Physics.Abstract
{
    /// <summary>
    /// Base class for all physics features
    /// </summary>
    public abstract class CKPhysicsFeatureBase : CKFeatureBase
    {
        /// <summary>
        /// The number of interactions that currently exist.
        /// </summary>
        public int CurrentInteractionCont { get; protected set; }
    }
}