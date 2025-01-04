namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Local
{
    public interface IOnObjectPreUpdateCallback : ILocalCallback
    {
        void OnBeforeObjectUpdate(float deltaTime);
    }
}