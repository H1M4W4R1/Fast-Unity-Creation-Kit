using FastUnityCreationKit.Core.Numerics.Abstract;
using FastUnityCreationKit.Economy.Abstract;

namespace FastUnityCreationKit.Economy
{
    /// <summary>
    /// Local resources are resources that are available at the local (object) level.
    /// This can be for example an entity health.
    /// <br/><br/>
    /// For more information, see <see cref="ResourceBase{TNumberType}"/>.
    /// </summary>
    public abstract class LocalResource<TNumberType> : ResourceBase<TNumberType>
        where TNumberType : struct, INumber, ISupportsFloatConversion<TNumberType>
    {
        
    }
}