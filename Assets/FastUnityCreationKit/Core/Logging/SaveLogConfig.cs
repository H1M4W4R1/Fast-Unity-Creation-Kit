using Unity.Logging;

namespace FastUnityCreationKit.Core.Logging
{
    public sealed class SaveLogConfig : LogConfigBase
    {
        public override LogLevel MinimumLevel => LogLevel.Verbose;

        public override ConsoleWriteInfo ConsoleOut => new(LogLevel.Info);

        public override FileWriteInfo[] Files => new[]
        {
            new FileWriteInfo(
                "Logs/Save.log",
                LogLevel.Verbose
            ),
            new FileWriteInfo(
                "Logs/SaveErrors.log",
                LogLevel.Error
            )
        };
    }
}