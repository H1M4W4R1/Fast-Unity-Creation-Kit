using System.IO;
using FastUnityCreationKit.Utility.Extensions;
using FastUnityCreationKit.Utility.Logging;
using FastUnityCreationKit.Utility.Serialization.Interfaces;
using JetBrains.Annotations;
using Sirenix.Serialization;

namespace FastUnityCreationKit.Utility.Serialization.Providers
{
    /// <summary>
    /// Serialization provider that uses Odin Serializer to serialize and deserialize data
    /// to and from JSON format.
    /// </summary>
    public sealed class OdinJsonSerializationProvider : ISerializationProvider
    {
        public bool WriteData<TSaveFile>(string filePath, [NotNull] TSaveFile saveData)
        {
            // Forbid empty path
            if (string.IsNullOrEmpty(filePath)) return false;

            // Serialize data
            byte[] binaryData = SerializationUtility.SerializeValue(saveData, DataFormat.JSON);

            // Write data to file and check if it was successful
            bool status = IOExtensions.TryWriteBytes(filePath, binaryData);
            if (status) Guard<SaveLogConfig>.Verbose($"Successfully wrote data to path: {filePath}.");
            else Guard<SaveLogConfig>.Error($"Failed to write data to path: {filePath}.");
            return status;
        }

        public (bool, TSaveFile) ReadData<TSaveFile>(string savePath)
        {
            // Check if file exists, also handles invalid path
            if (!File.Exists(savePath))
            {
                Guard<SaveLogConfig>.Error($"File does not exist at path: {savePath}. Returning default value.");
                return (false, default);
            }

            // Read data from file, check if it was successful
            // if not, return default value
            (bool status, byte[] data) = IOExtensions.TryReadBytes(savePath);
            if(!status) return (false, default);
            
            // Deserialize data
            TSaveFile deserializedValue =
                SerializationUtility.DeserializeValue<TSaveFile>(data, DataFormat.JSON);
            if (deserializedValue == null)
            {
                Guard<SaveLogConfig>.Error(
                    $"Failed to deserialize data from path: {savePath}. Returning default value.");
                return (false, default);
            }

            // Return deserialized value
            Guard<SaveLogConfig>.Verbose($"Successfully deserialized data from path: {savePath}.");
            return (true, deserializedValue);
        }
    }
}