using JetBrains.Annotations;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace FastUnityCreationKit.Core.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        ///     Converts Color to Hex string.
        /// </summary>
        /// <param name="color">Color to convert.</param>
        [NotNull] public static string ToHex(this Color color)
        {
            return $"#{ColorUtility.ToHtmlStringRGBA(color)}";
        }

        /// <summary>
        ///     Converts Hex string to Color. "#" is optional.
        /// </summary>
        /// <param name="hex">Hex string to convert.</param>
        public static Color FromHex([NotNull] string hex)
        {
            ColorUtility.TryParseHtmlString(hex.TrimStart('#'), out Color color);
            return color;
        }

        [BurstCompile] public static void Gradient(
            in float4 fromColor,
            in float4 toColor,
            float t,
            out float4 result)
        {
            t = math.clamp(t, 0f, 1f);
            result = math.lerp(fromColor, toColor, t);
        }

        [BurstCompile] public static void GradientBidirectional(
            in float4 fromColor,
            in float4 positiveColor,
            in float4 negativeColor,
            float t,
            out float4 result)
        {
            t = math.clamp(t, -1f, 1f);

            // Switch between positive and negative colors
            switch (t)
            {
                case >= 0f: Gradient(fromColor, positiveColor, t, out result); break;
                default: Gradient(fromColor, negativeColor, -t, out result); break;
            }
        }

        /// <summary>
        ///     Calculates color gradient between two colors.
        /// </summary>
        public static Color Gradient(this Color fromColor, Color toColor, float t)
        {
            return Color.Lerp(fromColor, toColor, t);
        }

        /// <summary>
        ///     Calculates color gradient between two colors.
        ///     If t is negative, gradient will be calculated to negativeColor
        ///     else, gradient will be calculated to positiveColor.
        /// </summary>
        public static Color GradientBidirectional(
            this Color fromColor,
            Color positiveColor,
            Color negativeColor,
            float t)
        {
            return t > 0 ? Gradient(fromColor, positiveColor, t) : Gradient(fromColor, negativeColor, -t);
        }
    }
}