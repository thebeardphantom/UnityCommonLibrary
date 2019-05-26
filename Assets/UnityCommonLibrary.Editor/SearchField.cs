using UnityEngine;

namespace BeardPhantom.UCL.Editor
{
    public class SearchField
    {
        #region Fields

        public readonly FilterValue FilterValue = new FilterValue();

        private readonly UnityEditor.IMGUI.Controls.SearchField _searchField
            = new UnityEditor.IMGUI.Controls.SearchField();

        #endregion

        #region Methods

        public bool OnGUI(bool toolbar, params GUILayoutOption[] options)
        {
            return FilterValue.Update(
                toolbar
                    ? _searchField.OnToolbarGUI(FilterValue.RawValue)
                    : _searchField.OnGUI(FilterValue.RawValue));
        }

        public void Focus()
        {
            _searchField.SetFocus();
        }

        #endregion
    }
}