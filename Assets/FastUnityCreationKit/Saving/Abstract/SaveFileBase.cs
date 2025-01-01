using System;
using FastUnityCreationKit.Saving.Metadata;
using FastUnityCreationKit.Saving.Utility;
using FastUnityCreationKit.Utility.Logging;
using FastUnityCreationKit.Utility.Serialization.Interfaces;
using FastUnityCreationKit.Utility.Serialization.Providers;

namespace FastUnityCreationKit.Saving.Abstract
{
    /// <summary>
    /// Base class for save files.
    /// Uses <see cref="OdinBinarySerializationProvider"/> as the default serialization provider.
    /// </summary>
    public abstract class SaveFileBase<TSelfSealed> : SaveFileBase<TSelfSealed, OdinBinarySerializationProvider>
        where TSelfSealed : SaveFileBase<TSelfSealed>, new()
    {
    }
    
    /// <summary>
    /// Base class for save files.
    /// Save file is used to represents partial data of a save. For full save data <see cref="SaveBase{TSelfSealed,TSerializationProvider}"/>
    /// </summary>
    public abstract class SaveFileBase<TSelfSealed, TSerializationProvider> : SaveFileBase,
        IWithSerializationProvider<TSerializationProvider>
        where TSelfSealed : SaveFileBase<TSelfSealed, TSerializationProvider>, new()
        where TSerializationProvider : ISerializationProvider, new()
    {
        public sealed override bool Save(SaveBase header)
        {
            // Check if the file path is not null or empty.
            if (string.IsNullOrEmpty(FilePath))
            {
                Guard<SaveLogConfig>.Error("File path is null or empty.");
                return false;
            }

            // Store user data.
            StoreUserData(header);

            if (this is TSelfSealed saveFile)
                return SaveAPI.WriteSaveFile<TSelfSealed, TSerializationProvider>(FilePath, saveFile);
            
            Guard<SaveLogConfig>.Error("Save file is not of the correct type.");
            return false;
        }
        
        public sealed override void Load(SaveBase header) => LoadUserData(header);
    }

    public abstract class SaveFileBase
    {
        /// <summary>
        /// Version of the file. Can be used for compatibility checks.
        /// </summary>
        public string FileVersion { get; internal set; }
        
        /// <summary>
        /// Name of file, must match <see cref="SaveFileMetadata{TSaveFileSealed}.FileName"/>
        /// Assigned automatically.
        /// </summary>
        public string FileName { get; internal set; }
        
        /// <summary>
        /// Path to the file on the disk.
        /// Assigned automatically.
        /// </summary>
        public string FilePath { get; internal set; }

        /// <summary>
        /// Saves the data to the file at <see cref="FilePath"/>.
        /// </summary>
        public abstract bool Save(SaveBase header);
        
        /// <summary>
        /// Loads the data from the file at <see cref="FilePath"/>.
        /// </summary>
        public abstract void Load(SaveBase header);

        /// <summary>
        /// Stores the user data to the save file.
        /// </summary>
        public abstract void StoreUserData(SaveBase header);
        
        /// <summary>
        /// Loads the user data from the save file.
        /// </summary>
        public abstract void LoadUserData(SaveBase header);
    }
}