using FastUnityCreationKit.Annotations.Unity;
using FastUnityCreationKit.Data;
using FastUnityCreationKit.Data.Containers;
using FastUnityCreationKit.UI.Elements.Core;

namespace FastUnityCreationKit.UI
{
    [AutoCreatedObject(DatabaseConstants.DATABASE_DIRECTORY)]
    public sealed class UIWindowsDatabase : AddressableDatabase<UIWindowsDatabase, UIWindow>
    {
        public UIWindowsDatabase()
        {
            addressableTags.Add(LocalConstants.UI_WINDOWS_ADDRESSABLE_TAG);
        }
    }
}