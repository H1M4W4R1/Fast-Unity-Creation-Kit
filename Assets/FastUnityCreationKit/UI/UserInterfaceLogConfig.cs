using FastUnityCreationKit.Utility.Logging;
using Unity.Logging;

namespace FastUnityCreationKit.UI
{
    public sealed class UserInterfaceLogConfig : LogConfigBase
    {
        public override LogLevel MinimumLevel => LogLevel.Verbose;

        public override ConsoleWriteInfo ConsoleOut => new(LogLevel.Info);

        public override FileWriteInfo[] Files => new[]
        {
            new FileWriteInfo(
                "Logs/UserInterface.log", 
                LogLevel.Verbose
            ),
            new FileWriteInfo(
                "Logs/UserInterfaceErrors.log",
                LogLevel.Error
            )
        };
    }
}