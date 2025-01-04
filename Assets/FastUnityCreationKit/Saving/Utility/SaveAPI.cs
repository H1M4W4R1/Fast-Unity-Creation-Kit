using System;
using System.Collections.Generic;
using System.IO;
using FastUnityCreationKit.Saving.Abstract;
using FastUnityCreationKit.Saving.Interfaces;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Core.Serialization.Interfaces;
using FastUnityCreationKit.Core.Serialization.Providers;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Saving.Utility
{
    /// <summary>
    /// Low-level API for saving and loading data. Try to avoid reading/writing methods for
    /// header and save files directly. Use <see cref="SaveBase{TSelfSealed,TSerializationProvider}"/>
    /// methods instead as it's more safe and easier to use.
    /// </summary>
    public static class SaveAPI
    {
        public const string DEFAULT_HEADER_NAME = "CK_METADATA.sav";

        /// <summary>
        /// List of all saveable objects currently registered.
        /// </summary>
        internal static List<ISaveableObject> SaveableObjects = new List<ISaveableObject>();

        /// <summary>
        /// Invoke the OnSave event on all registered saveable objects.
        /// </summary>m>
        internal static void InvokeOnFileSaved([NotNull] SaveBase saveFile)
        {
            // Loop through all saveable objects and call OnSave
            // must be reversed as some objects may be destroyed during the process
            for (int index = SaveableObjects.Count - 1; index >= 0; index--)
            {
                ISaveableObject saveableObject = SaveableObjects[index];
                saveableObject.BeforeSaveSaved(saveFile);
            }
        }
        
        /// <summary>
        /// Invoke the OnLoad event on all registered saveable objects.
        /// </summary>
        internal static void InvokeOnFileLoaded([NotNull] SaveBase saveFile)
        {
            // Loop through all saveable objects and call OnLoad
            // must be for loop as objects may be added during the process:
            // We don't want to crash the game by using foreach
            for (int index = 0; index < SaveableObjects.Count; index++)
            {
                ISaveableObject saveableObject = SaveableObjects[index];
                saveableObject.AfterSaveLoaded(saveFile);
            }
        }
        
        /// <summary>
        /// Register a saveable object to catch save/load events.
        /// </summary>
        /// <param name="saveableObject">Saveable object to register.</param>
        public static void RegisterSavableObject([NotNull] ISaveableObject saveableObject)
        {
            if (SaveableObjects.Contains(saveableObject))
            {
                Guard<SaveLogConfig>.Warning("Saveable object is already registered.");
                return;
            }

            SaveableObjects.Add(saveableObject);
        }
        
        /// <summary>
        /// Unregister a saveable object to stop catching save/load events.
        /// </summary>
        /// <param name="saveableObject">Saveable object to unregister.</param>
        public static void UnregisterSavableObject([NotNull] ISaveableObject saveableObject)
        {
            if (!SaveableObjects.Contains(saveableObject)) return;
            SaveableObjects.Remove(saveableObject);
        }
        
        /// <summary>
        /// Read all saves in a directory.
        /// </summary>
        [NotNull] [ItemNotNull]
        public static List<TSaveHeader> GetAllSavesIn<TSaveHeader>(string directoryPath)
            where TSaveHeader : SaveBase, new() =>
            GetAllSavesIn<TSaveHeader, OdinBinarySerializationProvider>(directoryPath);

        /// <summary>
        /// Get all saves in a directory.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public static List<TSaveHeader> GetAllSavesIn<TSaveHeader, THeaderSerializationProvider>(string directoryPath)
            where TSaveHeader : SaveBase, new()
            where THeaderSerializationProvider : ISerializationProvider, new()
        {
            // Ensure that path is not empty
            if (string.IsNullOrEmpty(directoryPath)) return new List<TSaveHeader>();

            // Ensure that path ends with correct symbol
            directoryPath = directoryPath.TrimEnd('/', '\\');

            // GetDirectories returns full path.
            string[] directories = Directory.GetDirectories(directoryPath);
            
            // Create temp header object to get save file path
            TSaveHeader tempHeader = new TSaveHeader();

            List<TSaveHeader> saves = new List<TSaveHeader>();
            foreach (string directory in directories)
            {
                // Ensure that path does not end with separator to support Path.GetFileName returning
                // last path part.
                string dir = directory;
                dir = dir.TrimEnd('/', '\\');
                
                // Get directory name, directory is name of save.
                string dirName = Path.GetFileName(dir);
                tempHeader.SaveName = dirName;

                // Get header file path (full path)
                string expectedHeaderFilePath = tempHeader.GetSaveFilePath(tempHeader.HeaderName);
                
                // Check if header exists
                if (!File.Exists(expectedHeaderFilePath))
                {
                    Guard<SaveLogConfig>.Verbose($"Header '{tempHeader.HeaderName}' not found in '{dir}'. Skipping.");
                    continue;
                }

                // Read header
                (bool status, TSaveHeader header) =
                    ReadHeaderFile<TSaveHeader, THeaderSerializationProvider>(expectedHeaderFilePath);
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
        public static bool WriteHeaderFile<THeaderFile>(string headerPath, [NotNull] THeaderFile headerFile) where THeaderFile : SaveBase =>
            WriteHeaderFile<THeaderFile, OdinBinarySerializationProvider>(headerPath, headerFile);

        /// <summary>
        /// Write header file to path
        /// </summary>
        public static bool WriteHeaderFile<THeaderFile, TSerializationProvider>(string headerPath, [NotNull] THeaderFile headerFile)
            where THeaderFile : SaveBase
            where TSerializationProvider : ISerializationProvider, new()
        {
            // Create provider
            TSerializationProvider serializationProvider = new TSerializationProvider();
         
            // Set header update information
            headerFile.LastModified = DateTime.UtcNow;

            // Write the save data and return the result if successful
            return serializationProvider.WriteData(headerPath, headerFile);
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
        public static bool WriteSaveFile<TSaveFile>(string toPath, [NotNull] TSaveFile saveFile) where TSaveFile : SaveFileBase =>
            WriteSaveFile<TSaveFile, OdinBinarySerializationProvider>(toPath, saveFile);

        /// <summary>
        /// Write save file to path
        /// </summary>
        public static bool WriteSaveFile<TSaveFile, TSerializationProvider>(string toPath, [NotNull] TSaveFile saveFile)
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