using FastUnityCreationKit.Tests.Core.Events.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Events
{
    [TestFixture]
    public class GlobalEventChannelWithDataTests : TestFixtureBase
    {
        public int callbackCounter = 0;
        
        private void OnEvent(ExampleChannelData data)
        {
            callbackCounter += data.digit;
        }
        
        [SetUp]
        public void SetUp()
        {
            callbackCounter = 0;
        }
        
        [TearDown]
        public void TearDown()
        {
            SampleGlobalEventChannelWithData.RemoveAllEventListeners();
        }
        
        [Test]
        public void RegisterListener_WorksCorrectly()
        {
            // Act
            SampleGlobalEventChannelWithData.RegisterEventListener(OnEvent);
            
            // Assert
            Assert.AreEqual(1, SampleGlobalEventChannelWithData.Instance.ListenerCount);
            
            // Additional assert - trigger event and check if callback was called
            // To prevent some weird edge cases
            SampleGlobalEventChannelWithData.TriggerEvent(new ExampleChannelData(32));
            Assert.AreEqual(32, callbackCounter);
        }
        
        [Test]
        public void UnregisterListener_WorksCorrectly()
        {
            // Act
            SampleGlobalEventChannelWithData.RegisterEventListener(OnEvent);
            SampleGlobalEventChannelWithData.UnregisterEventListener(OnEvent);
            
            // Assert
            Assert.AreEqual(0, SampleGlobalEventChannelWithData.Instance.ListenerCount);
            
            // Additional assert - trigger event and check if callback was called
            // To prevent some weird edge cases
            SampleGlobalEventChannelWithData.TriggerEvent(new ExampleChannelData(15));
            Assert.AreEqual(0, callbackCounter);
        }
        
        [Test]
        public void Trigger_WorksCorrectly()
        {
            // Act
            SampleGlobalEventChannelWithData.RegisterEventListener(OnEvent);
            SampleGlobalEventChannelWithData.TriggerEvent(new ExampleChannelData(155));
            
            // Assert
            Assert.AreEqual(155, callbackCounter);
        }
        
        [Test]
        public void Triggering_Event_MultipleTimes_WorksCorrectly()
        {
            // Act
            SampleGlobalEventChannelWithData.RegisterEventListener(OnEvent);
            SampleGlobalEventChannelWithData.TriggerEvent(new ExampleChannelData(1));
            SampleGlobalEventChannelWithData.TriggerEvent(new ExampleChannelData(5));
            SampleGlobalEventChannelWithData.TriggerEvent(new ExampleChannelData(4));
            
            // Assert
            Assert.AreEqual(10, callbackCounter);
        }
        
        
    }
}