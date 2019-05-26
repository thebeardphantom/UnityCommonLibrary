using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class GizmosUtility
    {
        #region Fields

        private static readonly Stack<Color> _gizmosColorStack =
            new Stack<Color>();

        #endregion

        #region Methods

        public static void DrawBounds(Bounds b)
        {
            Gizmos.DrawWireCube(b.center, b.size);
        }

        public static void PushGizmosColor(Color color)
        {
            _gizmosColorStack.Push(Gizmos.color);
            Gizmos.color = color;
        }

        public static void PopGizmosColor()
        {
            if (_gizmosColorStack.Count > 0)
            {
                Gizmos.color = _gizmosColorStack.Pop();
            }
        }

        public static void DrawArrow(
            Vector3 pos,
            Vector3 direction,
            float scale = 1f,
            float arrowHeadLength = 0.25f,
            float arrowHeadAngle = 20.0f)
        {
            Gizmos.DrawRay(pos, direction * scale);

            var right = Quaternion.LookRotation(direction)
                * Quaternion.Euler(0, 180 + arrowHeadAngle, 0)
                * new Vector3(0, 0, 1);

            var left = Quaternion.LookRotation(direction)
                * Quaternion.Euler(0, 180 - arrowHeadAngle, 0)
                * new Vector3(0, 0, 1);

            Gizmos.DrawRay(pos + direction * scale, right * arrowHeadLength * scale);
            Gizmos.DrawRay(pos + direction * scale, left * arrowHeadLength * scale);
        }

        #endregion
    }
}