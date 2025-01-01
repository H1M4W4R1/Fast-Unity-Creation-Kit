using System;
using Cysharp.Threading.Tasks;
using Unity.Logging;

namespace FastUnityCreationKit.Utility.Logging
{
    /// <summary>
    /// Guard is a logging utility that can be used to log messages.
    /// </summary>
    /// <typeparam name="TLogType">Type of log to use.</typeparam>
    public static class Guard<TLogType>
        where TLogType : LogConfigBase, new()
    {
        private static LoggerWrapper<TLogType> _loggerReference;
        
        static Guard()
        {
            // Create logger
            _loggerReference = new LoggerWrapper<TLogType>(true);
        }
            
        public static void Verbose(string message)
        {
            Log.Logger = _loggerReference.logger;
            Log.Verbose(message);
            UniTask.RunOnThreadPool(Log.FlushAll);
        }
        
        public static void Info(string message)
        {
            Log.Logger = _loggerReference.logger;
            Log.Info(message);
            UniTask.RunOnThreadPool(Log.FlushAll);
        }
        
        public static void Debug(string message)
        {
            Log.Logger = _loggerReference.logger;
            Log.Debug(message);
            UniTask.RunOnThreadPool(Log.FlushAll);
            
            #if UNITY_EDITOR
            UnityEngine.Debug.Log(message);
            #endif
        }
        
        public static void Warning(string message)
        {
            Log.Logger = _loggerReference.logger;
            Log.Warning(message);
            UniTask.RunOnThreadPool(Log.FlushAll);
            
            #if UNITY_EDITOR
            UnityEngine.Debug.LogWarning(message);
            #endif
        }
        
        public static void Error(string message)
        {
            Log.Logger = _loggerReference.logger;
            Log.Error(message);
            UniTask.RunOnThreadPool(Log.FlushAll);
            
            #if UNITY_EDITOR
            UnityEngine.Debug.LogError(message);
            #endif 
        } 
        
        public static void Fatal(string message)
        {
            Log.Logger = _loggerReference.logger;
            Log.Fatal(message);
            UniTask.RunOnThreadPool(Log.FlushAll);
            
            #if UNITY_EDITOR
            UnityEngine.Debug.LogError(message);
            #endif
        }
    }

    internal readonly struct LoggerWrapper<TLogType> 
        where TLogType : LogConfigBase, new()
    {
        public readonly TLogType logConfiguration;
        public readonly Logger logger;
        
        public LoggerWrapper(bool uselessVariableToAvoidCompilationIssues)
        {
            logConfiguration = new TLogType();
            logger = new Logger(logConfiguration.GetConfig());
            Log.Logger = logger;
        }
    }
    
    /// <summary>
    /// Guard is a logging utility that can be used to log messages.
    /// </summary>
    public static class Guard
    {

        public static LoggerConfig SetMinimumLevel(this LoggerConfig config, LogLevel level) => 
            config.MinimumLevel.SetMinimumLevel(level);

        public static LoggerConfig SetMinimumLevel(this LoggerMinimumLevelConfig config, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Verbose:
                    return config.Verbose();
                case LogLevel.Debug:
                    return config.Debug();
                case LogLevel.Info:
                    return config.Info();
                case LogLevel.Warning:
                    return config.Warning();
                case LogLevel.Error:
                    return config.Error();
                case LogLevel.Fatal:
                    return config.Fatal();
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
        
        
    }
}