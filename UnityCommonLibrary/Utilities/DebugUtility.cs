using UnityEngine;

namespace BeardPhantom.UCL.Utility
{
    public static class DebugUtility
    {
        #region Methods

        public static string GetDebugName(this Component cmp)
        {
            return GetDebugName(cmp.gameObject);
        }

        public static string GetDebugName(this GameObject go)
        {
            return $"[{go.GetInstanceID()}] {go.name}";
        }

        public static void DrawArrow(
            Vector3 pos,
            Vector3 direction,
            float arrowHeadLength = 0.25f,
            float arrowHeadAngle = 20.0f)
        {
            DrawArrow(
                pos,
                direction,
                Color.white,
                arrowHeadLength,
                arrowHeadAngle);
        }

        public static void DrawArrow(
            Vector3 pos,
            Vector3 direction,
            Color color,
            float arrowHeadLength = 0.25f,
            float arrowHeadAngle = 20.0f)
        {
            Debug.DrawRay(pos, direction, color);

            var right = Quaternion.LookRotation(direction)
                * Quaternion.Euler(0, 180 + arrowHeadAngle, 0)
                * new Vector3(0, 0, 1);

            var left = Quaternion.LookRotation(direction)
                * Quaternion.Euler(0, 180 - arrowHeadAngle, 0)
                * new Vector3(0, 0, 1);

            Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
        }

        public static void DrawLine(
            Vector3 start,
            Vector3 end,
            Color? color = null,
            float duration = 0f,
            float thickness = 1f)
        {
#if UNITY_EDITOR
            var sceneView = UnityEditor.SceneView.currentDrawingSceneView;
            if (sceneView == null)
            {
                sceneView = UnityEditor.SceneView.lastActiveSceneView;
                if (sceneView == null && UnityEditor.SceneView.sceneViews.Count > 0)
                {
                    sceneView = (UnityEditor.SceneView)UnityEditor.SceneView.sceneViews[0];
                }
            }
            if (sceneView == null)
            {
                return;
            }

            var camera = sceneView.camera;

            const float STEP = 0.01f;
            var quarterThick = thickness / 4f;
            var v1 = (end - start).normalized; // line direction
            var v2 = (camera.transform.position - start).normalized; // direction to camera
            var normalDir = Vector3.Cross(v1, v2); // normal vector
            var finalColor = color ?? Color.white;
            var useDuration = duration > 0f;
            for (var i = -quarterThick; i <= quarterThick; i += STEP)
            {
                var offset = normalDir * i;
                if (useDuration)
                {
                    Debug.DrawLine(start + offset, end + offset, finalColor, duration);
                }
                else
                {
                    Debug.DrawLine(start + offset, end + offset, finalColor);
                }
            }
#else
            Debug.Log("NOT IN EDITOR");
#endif
        }

        #endregion
    }
}