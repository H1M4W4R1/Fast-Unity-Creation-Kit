using FastUnityCreationKit.Core.Identification;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Identification
{
    [TestFixture]
    public class IDToStringTests : TestFixtureBase
    {
        
        [Test]
        public void ID8_ToString_WorksCorrectly()
        {
            // Act
            ID8 id = new ID8(1);
            
            // Assert
            Assert.AreEqual("1", id.ToString());
        }
        
        [Test]
        public void ID16_ToString_WorksCorrectly()
        {
            // Act
            ID16 id = new ID16(1);
            
            // Assert
            Assert.AreEqual("1", id.ToString());
        }
        
        [Test]
        public void ID32_ToString_WorksCorrectly()
        {
            // Act
            ID32 id = new ID32(1);
            
            // Assert
            Assert.AreEqual("1", id.ToString());
        }
        
        [Test]
        public void ID64_ToString_WorksCorrectly()
        {
            // Act
            ID64 id = new ID64(1);
            
            // Assert
            Assert.AreEqual("1", id.ToString());
        }
        
        [Test]
        public void SnowflakeID_ToString_WorksCorrectly()
        {
            long ticks = 0;
            
            // Act
            Snowflake128 id = new Snowflake128(ticks, 32, 16);
            
            // Assert
            Assert.AreEqual("0000000000000000-00000020-0010", id.ToString());
         
        }
        
    }
}