using System;
using JetBrains.Annotations;

namespace FastUnityCreationKit.Annotations.Editor
{
    /// <summary>
    ///     You can use this attribute to draw nicely-formatted named sections
    ///     in the inspector. It is useful for objects that don't have a custom
    ///     drawer, but you don't like the default foldout groups.
    ///
    ///     It also works with custom drawers as it should take precedence over
    ///     any other drawer.
    /// </summary>
    [UsedImplicitly] public sealed class NamedSectionAttribute : Attribute
    {
        public string Name { get; }
        public int FontSize { get; }
        public bool WithColor { get; }
        public float Red { get; }
        public float Green { get; }
        public float Blue { get; }
        public bool Bold { get; }
        public bool Italic { get; }

        public NamedSectionAttribute(
            string name,
            int fontSize = 0,
            bool bold = true,
            bool italic = false,
            float red = -1f,
            float green = -1f,
            float blue = -1f)
        {
            Name = name;
            FontSize = fontSize;
            Red = red;
            Green = green;
            Blue = blue;
            Bold = bold;
            Italic = italic;
            
            WithColor = red >= 0 && green >= 0 && blue >= 0;
        }
    }
}