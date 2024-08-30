using FastUnityCreationKit.Tests.Core.Events.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Events
{
    [TestFixture]
    public class LocalEventChannelTests
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
        
        [Test]
        public void RegisterListener_WorksCorrectly()
        {
            // Arrange
            SampleLocalEventChannel localEventChannel = new SampleLocalEventChannel();
            
            // Act
            localEventChannel.RegisterListener(OnEvent);
            
            // Assert
            Assert.AreEqual(1, localEventChannel.ListenerCount);
            
            // Additional assert - trigger event and check if callback was called
            // To prevent some weird edge cases
            localEventChannel.Trigger();
            Assert.AreEqual(1, callbackCounter);
        }
        
        [Test]
        public void UnregisterListener_WorksCorrectly()
        {
            // Arrange
            SampleLocalEventChannel localEventChannel = new SampleLocalEventChannel();
            
            // Act
            localEventChannel.RegisterListener(OnEvent);
            localEventChannel.UnregisterListener(OnEvent);
            
            // Assert
            Assert.AreEqual(0, localEventChannel.ListenerCount);
            
            // Additional assert - trigger event and check if callback was called
            // To prevent some weird edge cases
            localEventChannel.Trigger();
            Assert.AreEqual(0, callbackCounter);
        }
        
        [Test]
        public void Trigger_WorksCorrectly()
        {
            // Arrange
            SampleLocalEventChannel localEventChannel = new SampleLocalEventChannel();
            
            // Act
            localEventChannel.RegisterListener(OnEvent);
            localEventChannel.Trigger();
            
            // Assert
            Assert.AreEqual(1, callbackCounter);
        }
        
        [Test]
        public void Triggering_Event_MultipleTimes_WorksCorrectly()
        {
            // Arrange
            SampleLocalEventChannel localEventChannel = new SampleLocalEventChannel();
            
            // Act
            localEventChannel.RegisterListener(OnEvent);
            localEventChannel.Trigger();
            localEventChannel.Trigger();
            localEventChannel.Trigger();
            
            // Assert
            Assert.AreEqual(3, callbackCounter);
        }
        
        [Test]
        public void Triggering_Event_Without_Listener_WorksCorrectly()
        {
            // Arrange
            SampleLocalEventChannel localEventChannel = new SampleLocalEventChannel();
            
            // Act
            localEventChannel.Trigger();
            
            // Assert
            Assert.AreEqual(0, callbackCounter);
        }
        
        [Test]
        public void Triggering_Event_After_Unregistering_Listener_WorksCorrectly()
        {
            // Arrange
            SampleLocalEventChannel localEventChannel = new SampleLocalEventChannel();
            
            // Act
            localEventChannel.RegisterListener(OnEvent);
            localEventChannel.UnregisterListener(OnEvent);
            localEventChannel.Trigger();
            
            // Assert
            Assert.AreEqual(0, callbackCounter);
        }
        
        [Test]
        public void RegisterListener_RegistersOnlyOnce()
        {
            // Arrange
            SampleLocalEventChannel localEventChannel = new SampleLocalEventChannel();
            
            // Act
            localEventChannel.RegisterListener(OnEvent);
            localEventChannel.RegisterListener(OnEvent);
            
            // Assert
            Assert.AreEqual(1, localEventChannel.ListenerCount);
        }
        
        [Test]
        public void UnregisterListener_CalledMultipleTimes_DoesNotCrash()
        {
            // Arrange
            SampleLocalEventChannel localEventChannel = new SampleLocalEventChannel();
            
            // Act
            localEventChannel.RegisterListener(OnEvent);
            localEventChannel.UnregisterListener(OnEvent);
            localEventChannel.UnregisterListener(OnEvent);
            
            // Assert
            Assert.AreEqual(0, localEventChannel.ListenerCount);
        }
        
        [Test]
        public void RemoveAllListeners_WorksCorrectly()
        {
            // Arrange
            SampleLocalEventChannel localEventChannel = new SampleLocalEventChannel();
            
            // Act
            localEventChannel.RegisterListener(OnEvent);
            localEventChannel.RemoveAllListeners();
            
            // Assert
            Assert.AreEqual(0, localEventChannel.ListenerCount);
        }
        
        
    }
}