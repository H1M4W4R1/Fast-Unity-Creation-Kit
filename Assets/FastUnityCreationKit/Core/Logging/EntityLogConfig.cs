using Unity.Logging;

namespace FastUnityCreationKit.Core.Logging
{
    /// <summary>
    ///     Represents the log configuration for any entity-related logs.
    ///     For example: Entity creation, deletion, status changes, etc.
    /// </summary>
    public sealed class EntityLogConfig : LogConfigBase
    {
        public override LogLevel MinimumLevel => LogLevel.Verbose;

        public override ConsoleWriteInfo ConsoleOut => new(LogLevel.Verbose);

        public override FileWriteInfo[] Files => new[]
        {
            new FileWriteInfo(
                "Logs/Entity.log",
                LogLevel.Verbose
            ),
            new FileWriteInfo(
                "Logs/EntityErrors.log",
                LogLevel.Error)
        };
    }
}