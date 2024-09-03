using FastUnityCreationKit.Core.Utility.Properties;

namespace FastUnityCreationKit.Tests.Core.Utility.Data
{
    public sealed class ExampleLockableObject : IWithLock<ExampleLock>,
        IWithLock<ExamplePickableLock>
    {
        ExampleLock IWithLock<ExampleLock>.LockRepresentation { get; set; } = new ExampleLock();

        ExamplePickableLock IWithLock<ExamplePickableLock>.LockRepresentation { get; set; } = new ExamplePickableLock();
    }
}