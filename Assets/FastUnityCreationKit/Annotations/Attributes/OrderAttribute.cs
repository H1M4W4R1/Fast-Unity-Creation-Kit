namespace FastUnityCreationKit.Annotations.Attributes
{
    public sealed class OrderAttribute : System.Attribute
    {
        public int Order { get; }
        
        public OrderAttribute(int order)
        {
            Order = order;
        }
        
    }
}