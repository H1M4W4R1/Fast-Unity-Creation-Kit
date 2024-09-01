using FastUnityCreationKit.Core.Utility.Initialization;

namespace FastUnityCreationKit.Tests.Core.Utility.Data
{
    public class ExampleInitializableObject : IInitializable
    {
        public int hasBeenInitialized = 0;
        bool IInitializable.InternalInitializationStatusStorage { get; set; }

        void IInitializable.OnInitialize()
        {
            hasBeenInitialized++;
        }
    }
}