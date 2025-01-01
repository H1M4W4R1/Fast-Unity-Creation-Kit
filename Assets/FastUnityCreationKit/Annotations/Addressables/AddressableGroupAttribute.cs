namespace FastUnityCreationKit.Annotations.Addressables
{
    /// <summary>
    /// This attribute can be used to place ScriptableObject into AddressableGroup.
    /// Should also work on prefabs.
    /// </summary>
    public sealed class AddressableGroupAttribute : System.Attribute
    {
        public string GroupName { get; }
        
        public string[] Labels { get; }
        
        public AddressableGroupAttribute(string groupName, params string[] labels)
        {
            GroupName = groupName;
            Labels = labels;
        }
        
        public AddressableGroupAttribute(string groupName)
        {
            GroupName = groupName;
            Labels = new string[] {groupName};
        }
        
    }
}