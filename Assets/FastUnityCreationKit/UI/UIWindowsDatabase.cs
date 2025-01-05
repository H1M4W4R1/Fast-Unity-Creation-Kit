using FastUnityCreationKit.Annotations.Unity;
using FastUnityCreationKit.Data.Containers;
using FastUnityCreationKit.UI.Elements.Core;
using static FastUnityCreationKit.Core.Constants;

namespace FastUnityCreationKit.UI
{
    [AutoCreatedObject(DATABASE_DIRECTORY)]
    public sealed class UIWindowsDatabase : AddressableDatabase<UIWindowsDatabase, UIWindow>
    {
        public UIWindowsDatabase()
        {
            addressableTags.Add(UI_WINDOWS_ADDRESSABLE_TAG);
        }
    }
}