using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class TextureUtility
    {
        #region Methods

        public static float GetAspect(this Texture texture)
        {
            return (float) texture.width / texture.height;
        }

        #endregion
    }
}