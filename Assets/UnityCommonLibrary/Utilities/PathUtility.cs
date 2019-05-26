using System.IO;

namespace BeardPhantom.UCL.Utility
{
    public static class PathUtility
    {
        #region Methods

        public static string Combine(params string[] paths)
        {
            var path = "";

            foreach (var p in paths)
            {
                path = Path.Combine(path, p);
            }

            return path;
        }

        #endregion
    }
}