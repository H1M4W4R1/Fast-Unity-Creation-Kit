using System;
using FastUnityCreationKit.Core.Limits;
using FastUnityCreationKit.Economy;
using FastUnityCreationKit.Identification.Identifiers;

namespace FastUnityCreationKit.Examples._07_ResourceContainer
{
    [Serializable]
    public sealed class ExampleResourceContainer : ResourceContainerBase,
        IWithMinLimit, IWithMaxLimit
    {
        public ExampleResourceContainer(Snowflake128 resourceIdentifier) : base(resourceIdentifier)
        {
        }

        public double MinLimit => -25;
        public double MaxLimit => 100;
    }
}