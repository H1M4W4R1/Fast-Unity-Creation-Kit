using FastUnityCreationKit.Context.Abstract;
using FastUnityCreationKit.Core.Utility.Properties;
using UnityEngine;

namespace FastUnityCreationKit.Tests.Core.Utility.Data
{
    public class ExampleObjectWithProperties : IWithName<ExampleUsageContext>,
        IWithName<OtherExampleUsageContext>, IWithDescription<ExampleUsageContext>,
        IWithIcon<ExampleUsageContext>
    {
        private Sprite _cachedIcon;
        
        string IWithName<ExampleUsageContext>.Name => "Wolf";
        string IWithName<OtherExampleUsageContext>.Name => "Queen of the Night";
        public string Description => "A creature of the night";

        public Sprite Icon
        {
            get
            {
                if (_cachedIcon) return _cachedIcon;
                
                // Create new texture
                Texture2D texture = new Texture2D(1, 1);
                texture.SetPixel(0, 0, Color.black);
                
                // Apply changes
                texture.Apply();
                
                // Create new sprite
                _cachedIcon = Sprite.Create(texture, new Rect(0, 0, 1, 1), Vector2.zero);
                return _cachedIcon;
            }
        }
    }
    
    public class ExampleObjectWithAnyProperty : IWithName<ExampleUsageContext>,
        IWithName<AnyUsageContext>
    {
        string IWithName<ExampleUsageContext>.Name => "Wolf";
        string IWithName<AnyUsageContext>.Name => "WOLF_NAME_NOT_DEFINED";
    }
}