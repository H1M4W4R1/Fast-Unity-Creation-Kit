using System;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace FastUnityCreationKit.Core.Logging
{
	/// <summary>
	///     Represents a type of log used by the logging system.
	/// </summary>
	public abstract class LogConfigBase
    {
        [NotNull] public string OutputTemplate => "[{Level}] {Timestamp} - {Message}";
        public abstract LogLevel MinimumLevel { get; }
        [NotNull] public virtual FileWriteInfo[] Files { get; } = Array.Empty<FileWriteInfo>();
        public virtual ConsoleWriteInfo ConsoleOut { get; } = new();


        public LoggerConfig GetConfig()
        {
            LoggerConfig config = new LoggerConfig()
                .SetMinimumLevel(MinimumLevel)
                .OutputTemplate(OutputTemplate)
                .CaptureStacktrace(ConsoleOut.WithTrace || Files.Any(file => file.WithTrace));

            // Add console sink
            if (ConsoleOut.IsConfigured)
                config = config.WriteTo.StdOut(
                    minLevel: ConsoleOut.MinimumLevel,
                    captureStackTrace: ConsoleOut.WithTrace);

            // Add file sinks
            foreach (FileWriteInfo file in Files)
            {
                string path = file.DirectoryType switch
                {
                    LogPath.PersistentDataPath => Application.persistentDataPath,
                    LogPath.DataPath => Application.dataPath,
                    LogPath.TemporaryCachePath => Application.temporaryCachePath,
                    _ => Application.consoleLogPath
                } + "/" + file.FilePath;

                string directory = Path.GetDirectoryName(path);

                // If the directory is empty, skip current file
                if (string.IsNullOrEmpty(directory)) continue;

                // Create directory if it doesn't exist
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                // Add file sink
                config = config.WriteTo.File(
                    path,
                    minLevel: file.MinimumLevel,
                    maxFileSizeBytes: file.MaxSize,
                    maxTimeSpan: file.MaxAge,
                    captureStackTrace: file.WithTrace);
            }

            return config;
        }
    }

    public struct ConsoleWriteInfo
    {
        public bool IsConfigured { get; }
        public LogLevel MinimumLevel { get; }
        public bool WithTrace { get; }

        public ConsoleWriteInfo(
            LogLevel minimumLevel = LogLevel.Debug,
            bool withTrace = false
        )
        {
            IsConfigured = true;
            MinimumLevel = minimumLevel;
            WithTrace = withTrace;
        }
    }

    public struct FileWriteInfo
    {
        public bool IsConfigured { [UsedImplicitly] get; }
        public LogPath DirectoryType { get; }
        public string FilePath { get; }
        public LogLevel MinimumLevel { get; }
        public TimeSpan MaxAge { get; }
        public int MaxSize { get; }
        public bool WithTrace { get; }

        public FileWriteInfo(
            string filePath,
            LogLevel minimumLevel = LogLevel.Debug,
            TimeSpan maxAge = default,
            int maxSize = 4 * 1024 * 1024, // 4MB
            bool withTrace = false,
            LogPath directoryType = LogPath.PersistentDataPath
        )
        {
            IsConfigured = true;
            DirectoryType = directoryType;
            FilePath = filePath;
            MinimumLevel = minimumLevel;
            MaxSize = maxSize;
            WithTrace = withTrace;

            // Default to 7 days
            MaxAge = maxAge != default ? maxAge : TimeSpan.FromDays(7);
        }
    }

    public enum LogPath
    {
        PersistentDataPath,
        DataPath,
        TemporaryCachePath
    }
}