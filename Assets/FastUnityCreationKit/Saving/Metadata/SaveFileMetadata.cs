using System;
using FastUnityCreationKit.Annotations.Utility;
using FastUnityCreationKit.Saving.Abstract;
using FastUnityCreationKit.Saving.Utility;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Saving.Metadata
{
    /// <summary>
    ///     Represents metadata for a save file.
    /// </summary>
    public sealed class SaveFileMetadata<TSaveFileSealed> : SaveFileMetadata
        where TSaveFileSealed : SaveFileBase, new()
    {
        internal override SaveFileBase NewFile()
        {
            return new TSaveFileSealed();
        }

        internal override bool FromPath(string path, out SaveFileBase saveFile)
        {
            (bool success, TSaveFileSealed loadedSaveFile) = SaveAPI.ReadSaveFile<TSaveFileSealed>(path);
            saveFile = loadedSaveFile;
            return success;
        }
    }

    /// <summary>
    ///     Save file metadata - base class for simple save file information.
    /// </summary>
    [RequiresOdinSerialization] [Polymorph]
    public abstract class SaveFileMetadata
    {
        /// <summary>
        ///     If true, the file will be automatically loaded on save file load.
        /// </summary>
        public bool AutoLoad { get; set; }

        /// <summary>
        ///     Name of file. Must match <see cref="SaveFileBase.FileName" />
        /// </summary>
        public string FileName { get; [UsedImplicitly] set; }

        /// <summary>
        ///     Date this metadata was created.
        /// </summary>
        public DateTime CreationDate { get; internal set; } = DateTime.UtcNow;

        /// <summary>
        ///     Date this metadata was last modified.
        /// </summary>
        public DateTime LastModified { [UsedImplicitly] get; internal set; } = DateTime.UtcNow;

        /// <summary>
        ///     Tries to load save file from path.
        /// </summary>
        internal abstract bool FromPath([NotNull] string path, [CanBeNull] out SaveFileBase saveFile);

        /// <summary>
        ///     Creates a new save file for this metadata.
        /// </summary>
        [NotNull] internal abstract SaveFileBase NewFile();
    }
}