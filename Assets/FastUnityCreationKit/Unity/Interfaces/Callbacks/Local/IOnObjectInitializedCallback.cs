namespace FastUnityCreationKit.Unity.Interfaces.Callbacks.Local
{
    /// <summary>
    ///     This callback is called when the object is initialized (before it was created and enabled).
    /// </summary>
    public interface IOnObjectInitializedCallback : ILocalCallback
    {
        void OnObjectInitialized();
    }
}