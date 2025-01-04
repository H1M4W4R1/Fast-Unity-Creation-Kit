using FastUnityCreationKit.Saving.Abstract;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Saving.Interfaces
{
    /// <summary>
    ///     Represents an object that can be saved and loaded.
    ///     Avoid using this interface directly.
    ///     Any class that implements this interface must register savable object in global save event
    ///     for it to be saved and loaded properly.
    /// </summary>
    public interface ISaveableObject
    {
        /// <summary>
        ///     Write the data to the save object.
        /// </summary>
        void BeforeSaveSaved([NotNull] SaveBase toSave);

        /// <summary>
        ///     Load the stored data from the save object.
        /// </summary>
        void AfterSaveLoaded([NotNull] SaveBase fromSave);
    }
}