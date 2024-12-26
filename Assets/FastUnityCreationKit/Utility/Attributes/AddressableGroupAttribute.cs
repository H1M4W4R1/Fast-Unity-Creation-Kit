namespace FastUnityCreationKit.Utility.Attributes
{
    public sealed class AddressableGroupAttribute : System.Attribute
    {
        public string GroupName { get; }
        
        public string[] Labels { get; }
        
        public AddressableGroupAttribute(string groupName, params string[] labels)
        {
            GroupName = groupName;
            Labels = labels;
        }
        
    }
}