using FastUnityCreationKit.Core.Utility.Properties;
using FastUnityCreationKit.Core.Utility.Properties.Data;

namespace FastUnityCreationKit.Tests.Core.Utility.Data
{
    public class ExampleLock : ILock
    {
        public int lockedTimes;
        public int unlockedTimes;
        
        bool ILockable.IsLocked { get; set; }

        public void OnLocked()
        {
            lockedTimes++;
        }

        public void OnUnlocked()
        {
            unlockedTimes++;
        }

    }
}