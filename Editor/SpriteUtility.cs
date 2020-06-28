using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor
{
    public class SpriteUtility : MonoBehaviour
    {
        #region Methods

        [MenuItem("CONTEXT/Rigidbody2D/Ground Sprite")]
        public static void GroundSprite(MenuCommand command)
        {
            var rb2D = command.context as Rigidbody2D;
            var p2D = rb2D.gameObject.AddComponent<PolygonCollider2D>();

            var hits2D = Physics2D.RaycastAll(
                rb2D.transform.position,
                Vector2.down,
                float.MaxValue);

            foreach (var h2D in hits2D)
            {
                if (h2D.collider.gameObject != rb2D.gameObject)
                {
                    var diff = h2D.distance - p2D.bounds.extents.y;
                    diff *= -1f;
                    rb2D.transform.position += Vector3.up * diff;
                }
            }

            DestroyImmediate(p2D);
        }

        #endregion
    }
}