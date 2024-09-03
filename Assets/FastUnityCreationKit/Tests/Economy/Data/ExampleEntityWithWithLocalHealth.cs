using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Tests.Economy.Data
{
    public class ExampleEntityWithWithLocalHealth : IWithWithLocalResource<ExampleHealthLocalResource>
    {
        ExampleHealthLocalResource IWithWithLocalResource<ExampleHealthLocalResource>.ResourceStorage { get; } =
            new ExampleHealthLocalResource();
    }
}