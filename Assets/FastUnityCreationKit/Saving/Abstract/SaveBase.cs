using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FastUnityCreationKit.Saving.Metadata;
using FastUnityCreationKit.Saving.Utility;
using FastUnityCreationKit.Utility.Logging;
using FastUnityCreationKit.Utility.Serialization.Interfaces;
using FastUnityCreationKit.Utility.Serialization.Providers;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace FastUnityCreationKit.Saving.Abstract
{
    /// <summary>
    /// Base class for save files.
    /// Uses <see cref="OdinBinarySerializationProvider"/> for serialization.
    /// </summary>
    public abstract class SaveBase<TSelfSealed> : SaveBase<TSelfSealed, OdinBinarySerializationProvider>
        where TSelfSealed : SaveBase<TSelfSealed>, new()
    {
    }
    
    /// <summary>
    /// Base class for save files.
    /// </summary>
    public abstract class SaveBase<TSelfSealed, TSerializationProvider> : SaveBase,
        IWithSerializationProvider<TSerializationProvider>
        where TSelfSealed : SaveBase<TSelfSealed, TSerializationProvider>, new()
        where TSerializationProvider : ISerializationProvider, new()
    {
        /// <summary>
        /// Register metadata for save part.
        /// You should update <see cref="Metadata"/> list in this method.
        /// </summary>
        protected abstract bool SetupMetadata();

        /// <summary>
        /// Save this save to disk.
        /// </summary>
        public bool Save()
        {
            // Write header file
            if (this is not TSelfSealed selfHeader)
            {
                Guard<SaveLogConfig>.Error($"Failed to cast {typeof(TSelfSealed).Name} to {typeof(TSelfSealed).Name}.");
                return false;
            }

            // Check if save directory is set
            if (string.IsNullOrEmpty(SaveDirectory))
            {
                Guard<SaveLogConfig>.Error("Save directory is not set.");
                return false;
            }

            // Write data for each save part
            // that metadata was registered for
            for (int index = 0; index < Metadata.Count; index++)
            {
                SaveFileMetadata metadata = Metadata[index];

                // Check, if this save has loaded data for specific metadata
                SaveFileBase data = GetLoadedDataFor(metadata);
                if (data == null)
                {
                    Guard<SaveLogConfig>.Warning($"Failed to get data for {metadata.FileName}. " +
                                                 $"Skipping (probably saving without loading or some obsolete data).");
                    continue;
                }

                // Write file data to disk
                if (data.Save(this))
                {
                    // Update last modified date
                    LastModified = DateTime.UtcNow;
                    metadata.LastModified = DateTime.UtcNow;
                }
                else
                {
                    Guard<SaveLogConfig>.Error($"Failed to save {metadata.FileName}.");
                    return false;
                }

                // Write header file to save directory
                if (!SaveAPI.WriteHeaderFile<TSelfSealed, TSerializationProvider>(GetSaveFilePath(HeaderName), selfHeader))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Constructor for SaveBase, locked for security reasons.
        /// Use <see cref="New"/> to create new instance of save file or
        /// <see cref="Load"/> (alternatively: <see cref="TryLoad"/>) to load existing save file.
        /// </summary>
        protected SaveBase()
        {
        }

        /// <summary>
        /// Create new instance of save file
        /// </summary>
        /// <param name="saveName">Name of the save file.</param>
        /// <returns>New instance of save file.</returns>
        public static TSelfSealed New(string saveName)
        {
            TSelfSealed saveFile = new TSelfSealed()
            {
                SaveName = saveName
            };

            // Setup metadata for save file to make sure everything is loaded properly
            saveFile.SetupMetadata();

            // Loop through metadata and create new instances of save files
            // to store all data in memory
            foreach (SaveFileMetadata metadata in saveFile.Metadata)
                saveFile.CreateDataFor(metadata);

            return saveFile;
        }

        /// <summary>
        /// Load save file from disk.
        /// </summary>
        public static TSelfSealed Load(string saveName)
        {
            TryLoad(saveName, out TSelfSealed saveFile);
            return saveFile;
        }

        /// <summary>
        /// Try to load save file from disk.
        /// </summary>
        /// <param name="saveName">Name of the save file.</param>
        /// <param name="saveFile">Loaded save file.</param>
        /// <returns>True if save file was loaded successfully.</returns>
        public static bool TryLoad(string saveName, out TSelfSealed saveFile)
        {
            // Create temporary instance of save to access necessary data
            // it's wasteful, but it's the only way to avoid issues with obsolete
            // C# version in Unity. Update if they'll fix it.
            TSelfSealed tempSave = new TSelfSealed()
            {
                SaveName = saveName
            };

            // Load file from disk
            (bool status, TSelfSealed saveObj) =
                SaveAPI.ReadHeaderFile<TSelfSealed, TSerializationProvider>(tempSave.GetSaveFolder());
            saveFile = saveObj;
            return status;
        }
    }

    public abstract class SaveBase
    {
        /// <summary>
        /// Name of the save file, provided by user.
        /// </summary>
        [OdinSerialize]
        [ShowInInspector]
        [ReadOnly]
        [TabGroup("Debug")]
        [Required]
        public string SaveName { get; set; }

        /// <summary>
        /// All metadata of this save. This should be always
        /// loaded from script and not from disk to prevent issues between different save versions.
        /// Constructor should set up metadata.
        /// </summary>
        protected virtual List<SaveFileMetadata> Metadata { get; set; } = new List<SaveFileMetadata>();

        /// <summary>
        /// Data of loaded save files. Those files are loaded from disk and stored locally
        /// to prevent multiple disk reads and to allow easy access to data.
        /// </summary>
        private List<SaveFileBase> FileData { get; } = new List<SaveFileBase>();

        /// <summary>
        /// Save directory to write this saves in. It's recommended to use
        /// <see cref="Application.persistentDataPath"/> in this property.
        /// This is main directory saves are stored in, should not end with '/'.
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [TabGroup("Debug")]
        [Required]
        public abstract string SaveDirectory { get; }
        
        /// <summary>
        /// Name of the header file.
        /// </summary>
        [ShowInInspector]
        [ReadOnly]
        [TabGroup("Debug")]
        [Required]
        public virtual string HeaderName => SaveAPI.DEFAULT_HEADER_NAME;

        // Automatically set to current DateTime
        [OdinSerialize]
        [ShowInInspector]
        [ReadOnly]
        [TabGroup("Debug")]
        public DateTime CreationDate { get; internal set; } = DateTime.UtcNow;

        [OdinSerialize]
        [ShowInInspector]
        [ReadOnly]
        [TabGroup("Debug")]
        public DateTime LastModified { get; internal set; } = DateTime.UtcNow;

        /// <summary>
        /// Check if this save has data for specified save part.
        /// </summary>
        /// <typeparam name="TSaveFile">Type of the save file.</typeparam>
        /// <returns>True if save has data for specified save part.</returns>
        public bool HasMetadataFor<TSaveFile>() where TSaveFile : SaveFileBase, new() =>
            GetMetadataFor<TSaveFile>() != null;

        /// <summary>
        /// Get metadata for specified save part.
        /// </summary>
        /// <typeparam name="TSaveFile">Type of the save file.</typeparam>
        /// <returns>Metadata for specified save part or null if not found.</returns>
        public SaveFileMetadata<TSaveFile> GetMetadataFor<TSaveFile>()
            where TSaveFile : SaveFileBase, new()
        {
            foreach (SaveFileMetadata metadata in Metadata)
            {
                if (metadata is SaveFileMetadata<TSaveFile> saveFileMetadata)
                    return saveFileMetadata;
            }

            return null;
        }

        /// <summary>
        /// Check if this save has loaded data for specified save part.
        /// </summary>
        public bool HasLoadedDataFor<TSaveFile>() where TSaveFile : SaveFileBase =>
            GetLoadedDataFor<TSaveFile>() != null;

        /// <summary>
        /// Check if this save has data for specified save part.
        /// </summary>
        internal bool HasLoadedDataFor([NotNull] SaveFileMetadata metadata) =>
            GetLoadedDataFor(metadata) != null;

        /// <summary>
        /// Get loaded data for specified save part.
        /// It is recommended to use <see cref="GetOrLoadDataFor{TSaveFile}"/> instead due to safety risks.
        /// This method does not return new instance if data is not yet loaded.
        /// </summary>
        /// <typeparam name="TSaveFile">Save file type.</typeparam>
        /// <returns>Loaded data for specified save part or null if not found (or not loaded).</returns>
        public TSaveFile GetLoadedDataFor<TSaveFile>() where TSaveFile : SaveFileBase
        {
            for (int index = 0; index < FileData.Count; index++)
            {
                SaveFileBase saveFile = FileData[index];
                if (saveFile is TSaveFile data)
                    return data;
            }

            return null;
        }

        /// <summary>
        /// Get data for specified save part.
        /// </summary>
        [CanBeNull]
        internal SaveFileBase GetLoadedDataFor([NotNull] SaveFileMetadata metadata)
        {
            for (int index = 0; index < FileData.Count; index++)
            {
                SaveFileBase saveFile = FileData[index];
                if (saveFile.FileName == metadata.FileName)
                    return saveFile;
            }

            return null;
        }

        internal void CreateDataFor([NotNull] SaveFileMetadata metadata)
        {
            // Check if data is already loaded
            if (HasLoadedDataFor(metadata))
            {
                Guard<SaveLogConfig>.Warning($"Data for {metadata.FileName} already exists.");
                return;
            }

            // Create new metadata file and set-up file path
            SaveFileBase readFile = metadata.NewFile();
            readFile.FileName = metadata.FileName; // Names must match
            readFile.FilePath = GetSaveFilePath(metadata.FileName);
            FileData.Add(readFile);

            // Store user data in file (from current state of game)
            readFile.StoreUserData(this);

            Guard<SaveLogConfig>.Verbose($"Created {metadata.FileName}.");
        }

        /// <summary>
        /// Get data for specified save part.
        /// If data is loaded returns it, otherwise loads it from disk.
        /// 
        /// Can be used to efficiently store different maps (or sth) in different save files to chunk
        /// the data and reduce loading times.
        /// </summary>
        public TSaveFile GetOrLoadDataFor<TSaveFile>()
            where TSaveFile : SaveFileBase, new()
        {
            // Check if data is already loaded
            TSaveFile loadedData = GetLoadedDataFor<TSaveFile>();
            if (loadedData != null)
                return loadedData;

            // Load data from disk or create if missing
            SaveFileMetadata<TSaveFile> metadata = GetMetadataFor<TSaveFile>();
            if (metadata == null)
            {
                Guard<SaveLogConfig>.Verbose($"No metadata found for {typeof(TSaveFile).Name}. Creating new instance.");

                // Create new instance of metadata and register it as fail-safe
                metadata = new SaveFileMetadata<TSaveFile>();
                metadata.FileName = typeof(TSaveFile).Name;
                Metadata.Add(metadata);
            }

            string filePath = GetSaveFilePath(metadata.FileName);


            // Loading has built-in debugging, so no need to log here
            // if not found create new instance as fail-safe
            if (!metadata.TryLoad(filePath, out TSaveFile readFile))
            {
                // This part of code is handling save updates when new parts are added
                // and old saves are loaded. It creates new instances of save files
                // to prevent issues with missing data. Also loads data from current state of game.
                metadata.NewFile();
                readFile.FileName = metadata.FileName; // Names must match
                readFile.FilePath = filePath;

                // Save data from current state of game
                // aka. update save file with current data
                readFile.StoreUserData(this);

                Guard<SaveLogConfig>.Verbose(
                    $"Failed to load {typeof(TSaveFile).Name} from {filePath}. Creating new instance.");
            }

            // Add to loaded data, load data from file and return it
            FileData.Add(readFile);
            readFile.Load(this);
            Guard<SaveLogConfig>.Verbose($"Loaded {typeof(TSaveFile).Name} from {filePath}.");
            return readFile;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetSaveFolder() => $"{SaveDirectory.TrimEnd('/', '\\')}/{SaveName}";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetSaveFilePath(string fileName) => $"{GetSaveFolder()}/{fileName}";
    }
}