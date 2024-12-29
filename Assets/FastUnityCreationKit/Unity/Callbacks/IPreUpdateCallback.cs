namespace FastUnityCreationKit.Unity.Callbacks
{
    public interface IPreUpdateCallback
    {
        void OnBeforeObjectUpdated(float deltaTime);
    }
}