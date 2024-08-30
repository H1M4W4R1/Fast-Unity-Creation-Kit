using FastUnityCreationKit.Tests.Core.Events.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Events
{
    [TestFixture]
    public class GlobalEventChannelTests
    {
        public int callbackCounter = 0;
        
        private void OnEvent()
        {
            callbackCounter++;
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
            SampleGlobalEventChannel.RegisterEventListener(OnEvent);
            
            // Assert
            Assert.AreEqual(1, SampleGlobalEventChannel.Instance.ListenerCount);
            
            // Additional assert - trigger event and check if callback was called
            // To prevent some weird edge cases
            SampleGlobalEventChannel.TriggerEvent();
            Assert.AreEqual(1, callbackCounter);
        }
        
        [Test]
        public void UnregisterListener_WorksCorrectly()
        {
            // Act
            SampleGlobalEventChannel.RegisterEventListener(OnEvent);
            SampleGlobalEventChannel.UnregisterEventListener(OnEvent);
            
            // Assert
            Assert.AreEqual(0, SampleGlobalEventChannel.Instance.ListenerCount);
            
            // Additional assert - trigger event and check if callback was called
            // To prevent some weird edge cases
            SampleGlobalEventChannel.TriggerEvent();
            Assert.AreEqual(0, callbackCounter);
        }
        
        [Test]
        public void Trigger_WorksCorrectly()
        {
            // Act
            SampleGlobalEventChannel.RegisterEventListener(OnEvent);
            SampleGlobalEventChannel.TriggerEvent();
            
            // Assert
            Assert.AreEqual(1, callbackCounter);
        }
        
        [Test]
        public void Triggering_Event_MultipleTimes_WorksCorrectly()
        {
            // Act
            SampleGlobalEventChannel.RegisterEventListener(OnEvent);
            SampleGlobalEventChannel.TriggerEvent();
            SampleGlobalEventChannel.TriggerEvent();
            SampleGlobalEventChannel.TriggerEvent();
            
            // Assert
            Assert.AreEqual(3, callbackCounter);
        }
        
        
    }
}