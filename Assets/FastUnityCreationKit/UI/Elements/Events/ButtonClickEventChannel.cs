using FastUnityCreationKit.Core.Events;

namespace FastUnityCreationKit.UI.Elements.Events
{
    /// <summary>
    /// Represents a button click event channel.
    /// </summary>
    public sealed class ButtonClickEventChannel<TButton> : GlobalEventChannel<ButtonClickEventChannel<TButton>>
        where TButton : UIButton<TButton>
    {
        
    }
}