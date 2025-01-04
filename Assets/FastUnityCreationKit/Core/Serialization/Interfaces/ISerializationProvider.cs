namespace FastUnityCreationKit.Core.Serialization.Interfaces
{
    /// <summary>
    /// Used to provide serialization for writing and reading data
    /// </summary>
    public interface ISerializationProvider
    {
        /// <summary>
        /// Writes data to a file
        /// </summary>
        bool WriteData<TSaveFile>(string filePath, TSaveFile saveData);

        /// <summary>
        /// Reads data from a file
        /// </summary>
        (bool, TSaveFile) ReadData<TSaveFile>(string savePath);
    }
}