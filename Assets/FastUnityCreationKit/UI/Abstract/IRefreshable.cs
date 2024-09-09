using Cysharp.Threading.Tasks;

namespace FastUnityCreationKit.UI.Abstract
{
    /// <summary>
    /// Represents a refreshable object - an object that can be refreshed by request.
    /// Refreshable objects are rendered when they are refreshed. This is different from rendering because
    /// some objects may be rendered only once and never again, while refreshable objects can be refreshed multiple times
    /// and thus re-rendered when needed.
    /// <br/><br/>
    /// This interface automatically extends <see cref="IRenderable"/> interface.
    /// </summary>
    public interface IRefreshable : IRenderable
    {
        /// <summary>
        /// Refreshes the object.
        /// </summary>
        public UniTask Refresh() => Render();
    }
}