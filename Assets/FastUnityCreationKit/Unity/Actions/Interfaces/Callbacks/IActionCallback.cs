using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Unity.Actions.Interfaces.Callbacks
{
    /// <summary>
    ///     Represents a callback for an action.
    /// </summary>
    public interface IActionCallback<in TCallbackType> : IActionCallback
        where TCallbackType : IActionCallback<TCallbackType>
    {
        UniTask PerformCallback([NotNull] TCallbackType callback);
    }
    
    public interface IActionCallback
    {
        
    }
}