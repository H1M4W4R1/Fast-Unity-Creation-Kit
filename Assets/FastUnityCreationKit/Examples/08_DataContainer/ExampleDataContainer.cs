using System;
using FastUnityCreationKit.Data.Abstract;

namespace FastUnityCreationKit.Examples._08_DataContainer
{
    [Serializable]
    public sealed class ExampleDataContainer : DataContainerBase<string>
    {
        public ExampleDataContainer()
        {
            Add("Hello");
            Add("World");
        }
        
    }
}