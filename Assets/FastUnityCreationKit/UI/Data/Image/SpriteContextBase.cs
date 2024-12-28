using FastUnityCreationKit.UI.Context;
using FastUnityCreationKit.UI.Data.Interfaces;
using UnityEngine;

namespace FastUnityCreationKit.UI.Data.Image
{
    /// <summary>
    /// Represents a context that is populated with sprite data.
    /// </summary>
    public abstract class SpriteContextBase<TSelfSealed> : DataContext<TSelfSealed>, ISpriteContext
        where TSelfSealed : SpriteContextBase<TSelfSealed>, new()
    {
        public abstract Sprite Image { get; }
    }
}