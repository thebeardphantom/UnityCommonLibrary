using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class ObjectUtility
    {
        #region Methods

        public static void Destroy(ref Object obj)
        {
            if (obj != null)
            {
                Object.Destroy(obj);
                obj = null;
            }
        }

        public static void Destroy(params Object[] objs)
        {
            foreach (var obj in objs)
            {
                if (obj != null)
                {
                    Object.Destroy(obj);
                }
            }
        }

        #endregion
    }
}