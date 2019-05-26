using BeardPhantom.UCL.Attributes;
using UnityEditor;
using UnityEngine;

namespace BeardPhantom.UCL.Editor.Inspectors
{
    [CustomPropertyDrawer(typeof(NoteAttribute))]
    public class NoteAttributeDrawer : DecoratorDrawer
    {
        #region Fields

        private float _height;

        private NoteAttribute _note;

        private MessageType _type;

        #endregion

        #region Methods

        public override float GetHeight()
        {
            EnsureNoteData();

            return _height;
        }

        public override void OnGUI(Rect position)
        {
            EnsureNoteData();
            EditorGUI.HelpBox(position, _note.Text, _type);
        }

        private void EnsureNoteData()
        {
            if (_note == null)
            {
                _note = attribute as NoteAttribute;
                _type = (MessageType) (int) _note.Type;

                _height = EditorStyles
                    .helpBox.CalcSize(new GUIContent(_note.Text))
                    .y;
            }
        }

        #endregion
    }
}