using FastUnityCreationKit.Core.Utility.Properties;
using FastUnityCreationKit.Core.Utility.Properties.Data;
using FastUnityCreationKit.Tests.Core.Utility.Data;
using NUnit.Framework;

namespace FastUnityCreationKit.Tests.Core.Utility
{
    [TestFixture]
    public class LockTests : TestFixtureBase
    {

        [Test]
        public void Lock_LocksTheLock()
        {
            // Arrange
            ExampleLockableObject lockableObject = new ExampleLockableObject();
            IWithLock<ExampleLock> lockable = lockableObject;
            ExampleLock exampleLock = lockable.LockRepresentation;
            
            // Act
            lockable.Lock();
            
            // Assert
            Assert.IsTrue(lockable.IsLocked);
            Assert.AreEqual(1, exampleLock.lockedTimes);
        }
        
        [Test]
        public void Unlock_UnlocksTheLock()
        {
            // Arrange
            ExampleLockableObject lockableObject = new ExampleLockableObject();
            IWithLock<ExampleLock> lockable = lockableObject;
            ExampleLock exampleLock = lockable.LockRepresentation;
            
            // Act
            lockable.Lock();
            lockable.Unlock();
            
            // Assert
            Assert.IsFalse(lockable.IsLocked);
            Assert.AreEqual(1, exampleLock.unlockedTimes);
        }
        
        [Test]
        public void CanBeLockpicked_ReturnsFalse_IfLockIsNotPickable()
        {
            // Arrange
            ExampleLockableObject lockableObject = new ExampleLockableObject();
            IWithLock<ExampleLock> lockable = lockableObject;
            ILock exampleLock = lockable.LockRepresentation;
            
            // Act
            bool canBeLockpicked = exampleLock.CanBeLockpicked;
            
            // Assert
            Assert.IsFalse(canBeLockpicked);
        }
        
        [Test]
        public void CanBeLockpicked_ReturnsTrue_IfLockIsPickable()
        {
            // Arrange
            ExampleLockableObject lockableObject = new ExampleLockableObject();
            IWithLock<ExamplePickableLock> lockable = lockableObject;
            IJammableLock exampleLock = lockable.LockRepresentation;
            
            // Act
            bool canBeLockpicked = exampleLock.CanBeLockpicked;
            
            // Assert
            Assert.IsTrue(canBeLockpicked);
        }
        
        [Test]
        public void CanBeJammed_ReturnsFalse_IfLockIsNotJammable()
        {
            // Arrange
            ExampleLockableObject lockableObject = new ExampleLockableObject();
            IWithLock<ExampleLock> lockable = lockableObject;
            ILock exampleLock = lockable.LockRepresentation;
            
            // Act
            bool canBeJammed = exampleLock.CanBeJammed;
            
            // Assert
            Assert.IsFalse(canBeJammed);
        }
        
        [Test]
        public void CanBeJammed_ReturnsTrue_IfLockIsJammable()
        {
            // Arrange
            ExampleLockableObject lockableObject = new ExampleLockableObject();
            IWithLock<ExamplePickableLock> lockable = lockableObject;
            IJammableLock exampleLock = lockable.LockRepresentation;
            
            // Act
            bool canBeJammed = exampleLock.CanBeJammed;
            
            // Assert
            Assert.IsTrue(canBeJammed);
        }
        
        [Test]
        public void Jam_JamsTheLock()
        {
            // Arrange
            ExampleLockableObject lockableObject = new ExampleLockableObject();
            IWithLock<ExamplePickableLock> lockable = lockableObject;
            IJammableLock exampleLock = lockable.LockRepresentation;
            
            // Act
            exampleLock.Jam();
            
            // Assert
            Assert.IsTrue(exampleLock.IsJammed);
        }
        
        [Test]
        public void Unjam_UnjamsTheLock()
        {
            // Arrange
            ExampleLockableObject lockableObject = new ExampleLockableObject();
            IWithLock<ExamplePickableLock> lockable = lockableObject;
            IJammableLock exampleLock = lockable.LockRepresentation;
            
            // Act
            exampleLock.Jam();
            exampleLock.Unjam();
            
            // Assert
            Assert.IsFalse(exampleLock.IsJammed);
        }

        [Test]
        public void Unlock_DoesNotUnlock_IfTheLockIsJammed()
        {
            // Arrange
            ExampleLockableObject lockableObject = new ExampleLockableObject();
            IWithLock<ExamplePickableLock> lockable = lockableObject;
            IJammableLock exampleLock = lockable.LockRepresentation;
            
            // Act
            lockable.Lock();
            exampleLock.Jam();
            lockable.Unlock();
            
            // Assert
            Assert.IsTrue(exampleLock.IsJammed);
            Assert.IsTrue(lockable.IsLocked);
        }
        
        [Test]
        public void Lock_DoesNotLock_IfTheLockIsJammed()
        {
            // Arrange
            ExampleLockableObject lockableObject = new ExampleLockableObject();
            IWithLock<ExamplePickableLock> lockable = lockableObject;
            IJammableLock exampleLock = lockable.LockRepresentation;
            
            // Act
            exampleLock.Jam();
            lockable.Lock();
            
            // Assert
            Assert.IsTrue(exampleLock.IsJammed);
            Assert.IsFalse(lockable.IsLocked);
        }
        
    }
}