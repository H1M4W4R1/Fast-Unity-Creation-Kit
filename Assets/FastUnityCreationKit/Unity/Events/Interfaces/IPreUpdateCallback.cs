namespace FastUnityCreationKit.Unity.Events.Interfaces
{
    public interface IPreUpdateCallback
    {
        void OnBeforeObjectUpdated(float deltaTime);
    }
}