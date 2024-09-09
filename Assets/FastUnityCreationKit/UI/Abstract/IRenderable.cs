using Cysharp.Threading.Tasks;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// Represents a renderable object.
    /// Renderable objects can be rendered by script. Do not mistake rendering with refreshing.
    /// Rendering is used to draw object on screen, however that object does not need to support refreshing at all.
    /// It can be for example a simple static text label that is rendered only once and never again.
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Render the object.
        /// </summary>
        public void Render() => RenderAsync();
        
        /// <summary>
        /// Renders the object.
        /// </summary>
        public UniTask RenderAsync();
    }
}