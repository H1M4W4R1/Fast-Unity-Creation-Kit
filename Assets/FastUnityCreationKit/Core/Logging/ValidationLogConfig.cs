﻿using Unity.Logging;

namespace FastUnityCreationKit.Core.Logging
{
    public sealed class ValidationLogConfig : LogConfigBase
    {
        public override LogLevel MinimumLevel => LogLevel.Verbose;

        public override ConsoleWriteInfo ConsoleOut => new(LogLevel.Verbose);

        public override FileWriteInfo[] Files => new[]
        {
            new FileWriteInfo("Logs/EditorAutomata.log", LogLevel.Verbose),
            new FileWriteInfo("Logs/EditorAutomataErrors.log", LogLevel.Error)
        };
    }
}