using FastUnityCreationKit.Core.Events;

namespace FastUnityCreationKit.Tests.Core.Events.Data
{
    public struct ExampleChannelData : IEventChannelData
    {
        public int digit;
        
        public ExampleChannelData(int digit)
        {
            this.digit = digit;
        }
    }
}