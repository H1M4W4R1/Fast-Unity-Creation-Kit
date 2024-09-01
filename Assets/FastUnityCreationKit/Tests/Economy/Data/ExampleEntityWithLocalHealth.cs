using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Tests.Economy.Data
{
    public class ExampleEntityWithLocalHealth : IWithLocalResource<ExampleHealthLocalResource>
    {
        ExampleHealthLocalResource IWithLocalResource<ExampleHealthLocalResource>.ResourceStorage { get; } =
            new ExampleHealthLocalResource();
    }
}