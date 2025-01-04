namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Local
{
    public interface IOnObjectPostUpdateCallback : ILocalCallback
    {
        void OnAfterObjectUpdate(float deltaTime);
    }
}