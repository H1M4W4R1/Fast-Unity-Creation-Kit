using System;
using System.Collections.Generic;
using System.IO;
using FastUnityCreationKit.Saving.Abstract;
using FastUnityCreationKit.Utility.Logging;
using FastUnityCreationKit.Utility.Serialization.Interfaces;
using FastUnityCreationKit.Utility.Serialization.Providers;

namespace FastUnityCreationKit.Saving.Utility
{
    /// <summary>
    /// Low-level API for saving and loading data. Try to avoid reading/writing methods for
    /// header and save files directly. Use <see cref="SaveBase{TSelfSealed,TSerializationProvider}"/>
    /// methods instead as it's more safe and easier to use.
    /// </summary>
    public static class SaveAPI
    {
        public const string HEADER_NAME = "CK_METADATA.sav";
        
        /// <summary>
        /// Read all saves in a directory.
        /// </summary>
        public static List<TSaveHeader> GetAllSavesIn<TSaveHeader>(string directoryPath)
            where TSaveHeader : SaveBase =>
            GetAllSavesIn<TSaveHeader, OdinBinarySerializationProvider>(directoryPath);

        /// <summary>
        /// Get all saves in a directory.
        /// </summary>
        public static List<TSaveHeader> GetAllSavesIn<TSaveHeader, THeaderSerializationProvider>(string directoryPath)
            where TSaveHeader : SaveBase
            where THeaderSerializationProvider : ISerializationProvider, new()
        {
            // Ensure that path is not empty
            if (string.IsNullOrEmpty(directoryPath)) return new List<TSaveHeader>();

            // Ensure that path ends with correct symbol
            directoryPath = directoryPath.TrimEnd('/', '\\');

            // GetDirectories returns full path.
            string[] directories = Directory.GetDirectories(directoryPath);

            List<TSaveHeader> saves = new List<TSaveHeader>();
            foreach (string directory in directories)
            {
                // Ensure that path ends with correct symbol
                string dir = directory;
                if (!dir.EndsWith('/') && !dir.EndsWith('\\')) dir += '/';

                // Get header
                string headerPath = dir + HEADER_NAME;

                // Check if header exists
                if (!File.Exists(headerPath))
                {
                    Guard<SaveLogConfig>.Verbose($"Header '{HEADER_NAME}' not found in '{dir}'.");
                    continue;
                }

                // Read header
                (bool status, TSaveHeader header) =
                    ReadHeaderFile<TSaveHeader, THeaderSerializationProvider>(headerPath);
                if (!status) continue; // Skip if no header was found

                // Add header
                saves.Add(header);
            }

            return saves;
        }

        /// <summary>
        /// Read header file from path
        /// </summary>
        public static (bool, THeaderFile) ReadHeaderFile<THeaderFile>(string headerPath)
            where THeaderFile : SaveBase
        {
            return ReadHeaderFile<THeaderFile, OdinBinarySerializationProvider>(headerPath);
        }

        /// <summary>
        /// Read header file from path
        /// </summary>
        internal static (bool, THeaderFile) ReadHeaderFile<THeaderFile, TSerializationProvider>(string headerPath)
            where THeaderFile : SaveBase
            where TSerializationProvider : ISerializationProvider, new()
        {
            // Create provider
            TSerializationProvider serializationProvider = new TSerializationProvider();

            // Read the save data and return the result if successful
            (bool status, THeaderFile headerFile) = serializationProvider.ReadData<THeaderFile>(headerPath);
            if (status) return (true, headerFile);
            
            Guard<SaveLogConfig>.Fatal("Cannot read file. Please check the implementation of the SaveFileBase.");
            return (false, null);
        }
        
        /// <summary>
        /// Write header file to path
        /// </summary>
        public static bool WriteHeaderFile<THeaderFile>(string directoryPath, THeaderFile headerFile) where THeaderFile : SaveBase =>
            WriteHeaderFile<THeaderFile, OdinBinarySerializationProvider>(directoryPath, headerFile);

        /// <summary>
        /// Write header file to path
        /// </summary>
        public static bool WriteHeaderFile<THeaderFile, TSerializationProvider>(string directoryPath, THeaderFile headerFile)
            where THeaderFile : SaveBase
            where TSerializationProvider : ISerializationProvider, new()
        {
            // Create provider
            TSerializationProvider serializationProvider = new TSerializationProvider();
            
            // Check if path ends with correct symbol and update it with header name
            if (!directoryPath.EndsWith('/') && !directoryPath.EndsWith('\\')) directoryPath += '/';
            directoryPath += HEADER_NAME;
            
            // Set header update information
            headerFile.LastModified = DateTime.UtcNow;

            // Write the save data and return the result if successful
            return serializationProvider.WriteData(directoryPath, headerFile);
        }

        /// <summary>
        /// Read save file from path
        /// </summary>
        public static (bool, TSaveFile) ReadSaveFile<TSaveFile>(string savePath)
            where TSaveFile : SaveFileBase
        {
            return ReadSaveFile<TSaveFile, OdinBinarySerializationProvider>(savePath);
        }

        /// <summary>
        /// Read save file from path
        /// </summary>
        public static (bool, TSaveFile) ReadSaveFile<TSaveFile, TSerializationProvider>(string savePath)
            where TSaveFile : SaveFileBase
            where TSerializationProvider : ISerializationProvider, new()
        {
            // Create provider
            TSerializationProvider serializationProvider = new TSerializationProvider();

            // Read the save data and return the result if successful
            (bool status, TSaveFile saveFile) = serializationProvider.ReadData<TSaveFile>(savePath);
            if (status)
            {
                // Get path and file name
                saveFile.FilePath = savePath;
                saveFile.FileName = Path.GetFileName(savePath);

                return (true, saveFile);
            }

            Guard<SaveLogConfig>.Fatal("Cannot read file. Please check the implementation of the SaveFileBase.");
            return (false, null);
        }

        /// <summary>
        /// Write save file to path
        /// </summary>
        public static bool WriteSaveFile<TSaveFile>(string toPath, TSaveFile saveFile) where TSaveFile : SaveFileBase =>
            WriteSaveFile<TSaveFile, OdinBinarySerializationProvider>(toPath, saveFile);

        /// <summary>
        /// Write save file to path
        /// </summary>
        public static bool WriteSaveFile<TSaveFile, TSerializationProvider>(string toPath, TSaveFile saveFile)
            where TSaveFile : SaveFileBase
            where TSerializationProvider : ISerializationProvider, new()
        {
            // Create provider
            TSerializationProvider serializationProvider = new TSerializationProvider();
            
            saveFile.FilePath = toPath;
            saveFile.FileName = Path.GetFileName(toPath);

            // Write the save data and return the result if successful
            return serializationProvider.WriteData(toPath, saveFile);
        }
    }
}