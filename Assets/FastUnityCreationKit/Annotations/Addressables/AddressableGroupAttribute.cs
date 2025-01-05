using System;

namespace FastUnityCreationKit.Annotations.Addressables
{
    /// <summary>
    ///     This attribute can be used to place ScriptableObject into AddressableGroup.
    ///     Should also work on prefabs.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AddressableGroupAttribute : Attribute
    {
        public AddressableGroupAttribute(string groupName, params string[] labels)
        {
            GroupName = groupName;
            Labels = labels;
        }

        public AddressableGroupAttribute(string groupName)
        {
            GroupName = groupName;
            Labels = new[] {groupName};
        }

        public string GroupName { get; }

        public string[] Labels { get; }
    }
}