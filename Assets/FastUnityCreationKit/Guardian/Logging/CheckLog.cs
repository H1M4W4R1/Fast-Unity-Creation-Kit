using System.Runtime.CompilerServices;
using UnityEngine;

namespace FastUnityCreationKit.Guardian.Logging
{
    /// <summary>
    /// Represents a log printing subsystem for Guardian.
    /// </summary>
    public readonly ref struct CheckLog
    {
        private readonly string _message;
        private readonly ILogger _logger;
        private readonly LogType _type;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CheckLog(LogType type, string message, ILogger logger = null)
        {
            _message = message;
            _logger = logger;
            _type = type;
            
            // If logger was not set assign default Unity logger
            _logger ??= Debug.unityLogger;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Assert()
        {
            _logger.Log(_type, _message);
        }
    }
}