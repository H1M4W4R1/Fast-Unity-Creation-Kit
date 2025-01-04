using FastUnityCreationKit.Identification.Identifiers;
using JetBrains.Annotations;
using Unity.Mathematics;

namespace FastUnityCreationKit.Editor.Drawers.Identification
{
    [UsedImplicitly]
    public sealed class ID8Drawer : NumericIdentifierDrawerBase<ID8, byte>
    {
   
    }
    
    [UsedImplicitly]
    public sealed class ID16Drawer : NumericIdentifierDrawerBase<ID16, ushort>
    {
   
    }
    
    [UsedImplicitly]
    public sealed class ID32Drawer : NumericIdentifierDrawerBase<ID32, uint>
    {
   
    }
    
    [UsedImplicitly]
    public sealed class ID64Drawer : NumericIdentifierDrawerBase<ID64, ulong>
    {
   
    }
    
    [UsedImplicitly]
    public sealed class ID128Drawer : NumericIdentifierDrawerBase<ID128, uint4>
    {
   
    }
    
    [UsedImplicitly]
    public sealed class ID256Drawer : NumericIdentifierDrawerBase<ID256, uint4x2>
    {
   
    }
    
    [UsedImplicitly]
    public sealed class ID512Drawer : NumericIdentifierDrawerBase<ID512, uint4x4>
    {
   
    }
}