namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Local
{
    public interface IOnObjectUpdateCallback : ILocalCallback
    {
        void OnObjectUpdate(float deltaTime);
    }
}