namespace FastUnityCreationKit.Unity.Features.Physics.Abstract
{
    public abstract class CKTriggerFeatureBase : CKPhysicsFeatureBase
    {
        /// <summary>
        ///     Count of objects in the trigger.
        /// </summary>
        protected int Count => CurrentInteractionCont;
    }
}