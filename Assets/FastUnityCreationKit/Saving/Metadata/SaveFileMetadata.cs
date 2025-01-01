using System;
using FastUnityCreationKit.Saving.Abstract;
using FastUnityCreationKit.Saving.Utility;

namespace FastUnityCreationKit.Saving.Metadata
{
    /// <summary>
    /// Represents metadata for a save file.
    /// </summary>
    public sealed class SaveFileMetadata<TSaveFileSealed> : SaveFileMetadata
        where TSaveFileSealed : SaveFileBase, new()
    {
        /// <summary>
        /// Tries to load save file from path.
        /// </summary>
        public bool TryLoad(string fromPath, out TSaveFileSealed saveFile)
        {
            (bool success, TSaveFileSealed loadedSaveFile) = SaveAPI.ReadSaveFile<TSaveFileSealed>(fromPath);
            saveFile = loadedSaveFile;
            return success;
        }
        
        /// <summary>
        /// Tries to load save file from path.
        /// </summary>
        public TSaveFileSealed Load(string fromPath)
        {
            (bool _, TSaveFileSealed saveFile) = SaveAPI.ReadSaveFile<TSaveFileSealed>(fromPath);
            return saveFile;
        }

        /// <summary>
        /// Tries to save the save file to the specified path.
        /// </summary>
        public bool TrySave(string toPath, TSaveFileSealed saveFile) =>
             SaveAPI.WriteSaveFile(toPath, saveFile);
        
        /// <summary>
        /// Saves the save file to the specified path.
        /// </summary>
        public void Save(string toPath, TSaveFileSealed saveFile) =>
            SaveAPI.WriteSaveFile(toPath, saveFile);

        internal sealed override SaveFileBase NewFile() => new TSaveFileSealed();
    }
    
    /// <summary>
    /// Save file metadata - base class for simple save file information.
    /// </summary>
    public abstract class SaveFileMetadata
    {
        /// <summary>
        /// Name of file. Must match <see cref="SaveFileBase.FileName"/>
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// Date this metadata was created.
        /// </summary>
        public DateTime CreationDate { get; internal set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Date this metadata was last modified.
        /// </summary>
        public DateTime LastModified { get; internal set; } = DateTime.UtcNow;

        /// <summary>
        /// Creates a new save file for this metadata.
        /// </summary>
        internal abstract SaveFileBase NewFile();
    }
}