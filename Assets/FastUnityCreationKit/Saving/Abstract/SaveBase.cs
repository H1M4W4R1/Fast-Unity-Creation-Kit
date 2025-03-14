﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Core.Logging;
using FastUnityCreationKit.Core.Serialization.Interfaces;
using FastUnityCreationKit.Core.Serialization.Providers;
using FastUnityCreationKit.Saving.Interfaces;
using FastUnityCreationKit.Saving.Metadata;
using FastUnityCreationKit.Saving.Utility;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.Saving.Abstract
{
    /// <summary>
    ///     Base class for save files.
    ///     Uses <see cref="OdinBinarySerializationProvider" /> for serialization.
    /// </summary>
    public abstract class SaveBase<TSelfSealed> : SaveBase<TSelfSealed, OdinBinarySerializationProvider>
        where TSelfSealed : SaveBase<TSelfSealed>, new()
    {
    }

    /// <summary>
    ///     Base class for save files.
    /// </summary>
    public abstract class SaveBase<TSelfSealed, TSerializationProvider> : SaveBase
        where TSelfSealed : SaveBase<TSelfSealed, TSerializationProvider>, new()
        where TSerializationProvider : ISerializationProvider, new()
    {
        /// <summary>
        ///     Constructor for SaveBase, locked for security reasons.
        ///     Use <see cref="New" /> to create new instance of save file or
        ///     <see cref="Load" /> (alternatively: <see cref="TryLoad" />) to load existing save file.
        /// </summary>
        protected SaveBase()
        {
        }

        /// <summary>
        ///     Register metadata for save part.
        ///     You should update <see cref="Metadata" /> list in this method.
        /// </summary>
        protected abstract void SetupMetadata();

        /// <summary>
        ///     Called before save is saved. It can be used to update data before saving as it's
        ///     called before any data is written to disk.
        /// </summary>
        [UsedImplicitly] protected internal virtual async UniTask BeforeSaveWritten()
        {
            await UniTask.CompletedTask;
        }

        /// <summary>
        ///     Called before save is loaded. This event is a place to create all databases that will
        ///     be populated with data from disk using <see cref="ISaveableObject" /> interface as
        ///     this event is called before any data is loaded.
        /// </summary>
        [UsedImplicitly] protected internal virtual async UniTask BeforeSaveLoaded()
        {
            await UniTask.CompletedTask;
        }

        /// <summary>
        ///     Called when save is loaded.
        /// </summary>
        [UsedImplicitly] protected internal virtual async UniTask OnSaveLoaded()
        {
            await UniTask.CompletedTask;
        }

        /// <summary>
        ///     Called when save failed to load.
        /// </summary>
        [UsedImplicitly] protected internal virtual async UniTask OnSaveLoadFailed()
        {
            await UniTask.CompletedTask;
        }

        /// <summary>
        ///     Called when save is saved.
        /// </summary>
        [UsedImplicitly] protected internal virtual async UniTask OnSaveWritten()
        {
            await UniTask.CompletedTask;
        }

        /// <summary>
        ///     Called when save failed to save.
        /// </summary>
        [UsedImplicitly] protected internal virtual async UniTask OnSaveWriteFailed()
        {
            await UniTask.CompletedTask;
        }

        /// <summary>
        ///     Save this save to disk.
        /// </summary>
        public async UniTask<bool> Save()
        {
            // Write header file
            if (this is not TSelfSealed selfHeader)
            {
                Guard<SaveLogConfig>.Error(
                    $"Failed to cast {typeof(TSelfSealed).GetCompilableNiceFullName()} to {typeof(TSelfSealed).GetCompilableNiceFullName()}.");
                await OnSaveWriteFailed();
                return false;
            }

            // Check if save directory is set
            if (string.IsNullOrEmpty(SaveDirectory))
            {
                Guard<SaveLogConfig>.Error("Save directory is not set.");
                await OnSaveWriteFailed();
                return false;
            }

            // Perform actions before save is saved
            await BeforeSaveWritten();

            // Invoke event for all scene objects
            SaveAPI.InvokeOnFileSaved(this);

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
                                                 "Skipping (probably saving without loading or some obsolete data).");
                    continue;
                }

                // Write file data to disk
                if (await data.OnSave(this))
                {
                    // Update last modified date
                    LastModified = DateTime.UtcNow;
                    metadata.LastModified = DateTime.UtcNow;
                }
                else
                {
                    Guard<SaveLogConfig>.Error($"Failed to save {metadata.FileName}.");
                    await OnSaveWriteFailed();
                    return false;
                }

                // Write header file to save directory
                if (!SaveAPI.WriteHeaderFile<TSelfSealed, TSerializationProvider>(GetSaveFilePath(HeaderName),
                        selfHeader))
                {
                    await OnSaveWriteFailed();
                    return false;
                }
            }

            await OnSaveWritten();
            return true;
        }

        /// <summary>
        ///     Create new instance of save file
        /// </summary>
        /// <param name="saveName">Name of the save file.</param>
        /// <returns>New instance of save file.</returns>
        public static async UniTask<TSelfSealed> New(string saveName)
        {
            TSelfSealed saveFile = new()
            {
                SaveName = saveName
            };

            // Setup metadata for save file to make sure everything is loaded properly
            saveFile.SetupMetadata();

            // Loop through metadata and create new instances of save files
            // to store all data in memory
            foreach (SaveFileMetadata metadata in saveFile.Metadata) await saveFile.CreateDataFor(metadata);

            return saveFile;
        }

        /// <summary>
        ///     Load save file from disk.
        /// </summary>
        public static async UniTask<TSelfSealed> Load(string saveName)
        {
            (bool _, TSelfSealed saveFile) = await TryLoad(saveName);
            return saveFile;
        }

        /// <summary>
        ///     Try to load save file from disk.
        /// </summary>
        /// <param name="saveName">Name of the save file.</param>
        /// <returns>True if save file was loaded successfully.</returns>
        public static async UniTask<(bool, TSelfSealed)> TryLoad(string saveName)
        {
            // Create temporary instance of save to access necessary data
            // it's wasteful, but it's the only way to avoid issues with obsolete
            // C# version in Unity. Update if they'll fix it.
            TSelfSealed tempSave = new()
            {
                SaveName = saveName
            };

            // Load file from disk
            (bool status, TSelfSealed saveObj) =
                SaveAPI.ReadHeaderFile<TSelfSealed, TSerializationProvider>(tempSave.GetSaveFolder());

            // Setup save metadata as it's not loaded from disk
            // also call events based on status
            if (status)
            {
                // TODO: Add conversion subsystem for handling loading different save versions

                saveObj.SetupMetadata(); // We don't need to create new instances as they are loaded from disk

                // Perform actions before save is loaded
                await saveObj.BeforeSaveLoaded();

                // Invoke event for all scene objects
                SaveAPI.InvokeOnFileLoaded(saveObj);

                // Preload all save parts to prevent issues with loading
                // ignore return from embedded method as we only need to load data
                for (int index = 0; index < saveObj.Metadata.Count; index++)
                    await saveObj.GetOrLoadDataFor(saveObj.Metadata[index], false);

                await saveObj.OnSaveLoaded();
            }
            else
            {
                await saveObj.OnSaveLoadFailed();
            }

            // Return status and save object
            return (status, saveObj);
        }
    }

    [NotAllowedInObjects] [RequiresOdinSerialization] [Polymorph] // Must be serialized by Odin to prevent issues with polymorphism
    public abstract class SaveBase
    {

        /// <summary>
        ///     Name of the save file, provided by user.
        /// </summary>
        [OdinSerialize] [ShowInInspector] [ReadOnly] [Required] [TitleGroup(GROUP_CONFIGURATION)]
        public string SaveName { get; set; }

        /// <summary>
        ///     All metadata of this save. This should be always
        ///     loaded from script and not from disk to prevent issues between different save versions.
        ///     Constructor should set up metadata.
        /// </summary>
        [OdinSerialize] [HideInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)]
        protected virtual List<SaveFileMetadata> Metadata { get; set; } = new();

        // Automatically set to current DateTime
        [OdinSerialize] [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)]
        public DateTime CreationDate { get; internal set; } = DateTime.UtcNow;

        [OdinSerialize] [ShowInInspector] [ReadOnly] [TitleGroup(GROUP_DEBUG, Order = int.MaxValue)]
        public DateTime LastModified { get; internal set; } = DateTime.UtcNow;
        
        /// <summary>
        ///     Data of loaded save files. Those files are loaded from disk and stored locally
        ///     to prevent multiple disk reads and to allow easy access to data.
        /// </summary>
        [field: NonSerialized]
        private List<SaveFileBase> FileData { get; } = new();

        /// <summary>
        ///     Save directory to write this saves in. It's recommended to use
        ///     <see cref="Application.persistentDataPath" /> in this property.
        ///     This is main directory saves are stored in, should not end with '/'.
        /// </summary>
        [ShowInInspector] [ReadOnly] [Required] [TitleGroup(GROUP_CONFIGURATION)]
        public abstract string SaveDirectory { get; }

        /// <summary>
        ///     Name of the header file.
        /// </summary>
        [ShowInInspector] [ReadOnly] [Required] [TitleGroup(GROUP_CONFIGURATION)] [NotNull]
        public virtual string HeaderName => SaveAPI.DEFAULT_HEADER_NAME;

        /// <summary>
        ///     Check if this save has data for specified save part.
        /// </summary>
        /// <typeparam name="TSaveFile">Type of the save file.</typeparam>
        /// <returns>True if save has data for specified save part.</returns>
        public bool HasMetadataFor<TSaveFile>()
            where TSaveFile : SaveFileBase, new()
        {
            return GetMetadataFor<TSaveFile>() != null;
        }

        /// <summary>
        ///     Get metadata for specified save part.
        /// </summary>
        /// <typeparam name="TSaveFile">Type of the save file.</typeparam>
        /// <returns>Metadata for specified save part or null if not found.</returns>
        [CanBeNull] public SaveFileMetadata<TSaveFile> GetMetadataFor<TSaveFile>()
            where TSaveFile : SaveFileBase, new()
        {
            foreach (SaveFileMetadata metadata in Metadata)
                if (metadata is SaveFileMetadata<TSaveFile> saveFileMetadata)
                    return saveFileMetadata;

            return null;
        }

        /// <summary>
        ///     Check if this save has loaded data for specified save part.
        /// </summary>
        public bool HasLoadedDataFor<TSaveFile>()
            where TSaveFile : SaveFileBase
        {
            return GetLoadedDataFor<TSaveFile>() != null;
        }

        /// <summary>
        ///     Check if this save has data for specified save part.
        /// </summary>
        internal bool HasLoadedDataFor([NotNull] SaveFileMetadata metadata)
        {
            return GetLoadedDataFor(metadata) != null;
        }

        /// <summary>
        ///     Get loaded data for specified save part.
        ///     It is recommended to use <see cref="GetOrLoadDataFor{TSaveFile}" /> instead due to safety risks.
        ///     This method does not return new instance if data is not yet loaded.
        /// </summary>
        /// <typeparam name="TSaveFile">Save file type.</typeparam>
        /// <returns>Loaded data for specified save part or null if not found (or not loaded).</returns>
        [CanBeNull] public TSaveFile GetLoadedDataFor<TSaveFile>()
            where TSaveFile : SaveFileBase
        {
            for (int index = 0; index < FileData.Count; index++)
            {
                SaveFileBase saveFile = FileData[index];
                if (saveFile is TSaveFile data) return data;
            }

            return null;
        }

        /// <summary>
        ///     Get data for specified save part.
        /// </summary>
        [CanBeNull] internal SaveFileBase GetLoadedDataFor([NotNull] SaveFileMetadata metadata)
        {
            for (int index = 0; index < FileData.Count; index++)
            {
                SaveFileBase saveFile = FileData[index];
                if (saveFile.FileName == metadata.FileName) return saveFile;
            }

            return null;
        }

        internal async UniTask<SaveFileBase> CreateDataFor(
            [NotNull] SaveFileMetadata metadata,
            bool storeUserData = true)
        {
            // Check if data is already loaded
            if (HasLoadedDataFor(metadata))
            {
                Guard<SaveLogConfig>.Warning($"Data for {metadata.FileName} already exists.");
                return null;
            }

            // Create new metadata file and set-up file path
            SaveFileBase readFile = metadata.NewFile();
            readFile.FileName = metadata.FileName; // Names must match
            readFile.FilePath = GetSaveFilePath(metadata.FileName);
            FileData.Add(readFile);

            // Store user data in file (from current state of game)
            if (storeUserData) await readFile.BeforeSaveWritten(this);

            Guard<SaveLogConfig>.Verbose($"Created {metadata.FileName}.");
            return readFile;
        }

        /// <summary>
        ///     Get data for specified save part.
        /// </summary>
        public async UniTask<SaveFileBase> GetOrLoadDataFor(
            [NotNull] SaveFileMetadata metadata,
            bool storeUserDataIfNew = true)
        {
            // Check if data is already loaded
            SaveFileBase loadedData = GetLoadedDataFor(metadata);
            if (loadedData != null) return loadedData;

            // Load data from disk or create if missing
            string filePath = GetSaveFilePath(metadata.FileName);

            // Loading has built-in debugging, so no need to log here
            // if not found create new instance as fail-safe
            if (!metadata.FromPath(filePath, out SaveFileBase readFile))
            {
                Guard<SaveLogConfig>.Verbose(
                    $"Failed to load {metadata.FileName} from {filePath}. Creating new instance.");

                // Create new instance of save file
                // also automatically adds it to FileData list
                readFile = await CreateDataFor(metadata, storeUserDataIfNew);
            }
            else
            {
                // Add loaded data to list
                FileData.Add(readFile);
            }

            // Check if file is loaded properly
            if (readFile == null)
            {
                Guard<SaveLogConfig>.Error($"Failed to load {metadata.FileName} from {filePath}.");
                return null;
            }

            await readFile.OnLoad(this);
            Guard<SaveLogConfig>.Verbose($"Loaded {metadata.FileName} from {filePath}.");
            return readFile;
        }

        /// <summary>
        ///     Get data for specified save part.
        ///     If data is loaded returns it, otherwise loads it from disk.
        ///     Can be used to efficiently store different maps (or sth) in different save files to chunk
        ///     the data and reduce loading times.
        /// </summary>
        [CanBeNull] public TSaveFile GetOrLoadDataFor<TSaveFile>()
            where TSaveFile : SaveFileBase, new()
        {
            // Get metadata for specified save part
            SaveFileMetadata<TSaveFile> metadata = GetMetadataFor<TSaveFile>();
            if (metadata == null)
            {
                Guard<SaveLogConfig>.Error(
                    $"Failed to get metadata for {typeof(TSaveFile).GetCompilableNiceFullName()}.");
                return null;
            }

            // Get or load data for specified save part, safe cast as additional safety precaution
            // It should be of the correct type, but it's better to be safe than sorry
            return GetOrLoadDataFor(metadata) as TSaveFile;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] [NotNull] public string GetSaveFolder()
        {
            return $"{SaveDirectory.TrimEnd('/', '\\')}/{SaveName}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)] [NotNull] public string GetSaveFilePath(string fileName)
        {
            return $"{GetSaveFolder()}/{fileName}";
        }
    }
}