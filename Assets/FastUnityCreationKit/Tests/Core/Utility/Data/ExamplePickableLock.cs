using FastUnityCreationKit.Core.Utility.Properties;
using FastUnityCreationKit.Core.Utility.Properties.Data;

namespace FastUnityCreationKit.Tests.Core.Utility.Data
{
    public sealed class ExamplePickableLock : IPickableLock, IJammableLock
    {
        public int jammedTimes;
        public int unjammedTimes;
        
        bool ILockable.IsLocked { get; set; }
        bool IJammableLock.IsJammed { get; set; }
        public void OnLockJammed()
        {
            jammedTimes++;
        }

        public void OnLockUnjammed()
        {
            unjammedTimes++;
        }

        public void OnLocked()
        {
            
        }

        public void OnUnlocked()
        {
        }

        public void OnLockpickingSuccess()
        {
        }

        public void OnLockpickingFailure()
        {
        }

    }
}