using UnityEngine.UI;

namespace BeardPhantom.UCL
{
    public class UIHitbox : Graphic
    {
        #region Methods

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }

        #endregion
    }
}