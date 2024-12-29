using FastUnityCreationKit.Data;
using FastUnityCreationKit.Data.Containers;
using FastUnityCreationKit.UI.Elements.Core;
using FastUnityCreationKit.Utility.Attributes;

namespace FastUnityCreationKit.UI
{
    [AutoCreatedObject(DatabaseConstants.DATABASE_DIRECTORY)]
    [AddressableGroup(DatabaseConstants.DATABASE_ADDRESSABLE_TAG)]
    public sealed class UIWindowsDatabase : AddressableDatabase<UIWindowsDatabase, UIWindow>
    {
        public UIWindowsDatabase()
        {
            addressableTag = LocalConstants.UI_WINDOWS_ADDRESSABLE_TAG;
        }
    }
}