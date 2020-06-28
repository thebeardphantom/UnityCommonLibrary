using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class GradientUtility
    {
        #region Methods

        public static Gradient MakeRainbow(this Gradient g)
        {
            g.SetColors(
                new Color(1f, 0f, 0f),
                new Color(1f, 1f, 0f),
                new Color(0f, 1f, 0f),
                new Color(0f, 1f, 1f),
                new Color(1f, 0f, 1f));

            g.alphaKeys = new[]
            {
                new GradientAlphaKey(1f, 0f)
            };

            return g;
        }

        public static Gradient MakeMeter(this Gradient g)
        {
            g.SetColors(Color.red, Color.yellow, Color.green);

            g.alphaKeys = new[]
            {
                new GradientAlphaKey(1f, 0f)
            };

            return g;
        }

        public static Gradient SetColors(this Gradient g, params Color[] colors)
        {
            var interval = 1f / (colors.Length - 1);
            var keys = new GradientColorKey[colors.Length];

            for (var i = 0; i < colors.Length; i++)
            {
                var t = Mathf.Clamp01(i * interval);
                keys[i] = new GradientColorKey(colors[i], t);
            }

            g.colorKeys = keys;

            return g;
        }

        public static Gradient SetAlphas(this Gradient g, params float[] alphas)
        {
            var interval = 1f / (alphas.Length - 1);
            var keys = new GradientAlphaKey[alphas.Length];

            for (var i = 0; i < alphas.Length; i++)
            {
                var t = Mathf.Clamp01(i * interval);
                keys[i] = new GradientAlphaKey(alphas[i], t);
            }

            g.alphaKeys = keys;

            return g;
        }

        #endregion
    }
}