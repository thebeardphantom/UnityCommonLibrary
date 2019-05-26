using System;
using UnityEngine;

namespace BeardPhantom.UCL.Attributes
{
    /// <summary>
    /// Displays a HelpBox in the Inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class NoteAttribute : PropertyAttribute
    {
        #region Types

        /// <summary>
        /// The type of message to display.
        /// </summary>
        public enum MessageType
        {
#pragma warning disable 1591
            None,
            Info,
            Warning,
            Error
#pragma warning restore 1591
        }

        #endregion

        #region Fields

        /// <summary>
        /// The text of the message.
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// What type of message is displayed.
        /// </summary>
        public readonly MessageType Type;

        #endregion

        #region Constructors

        /// <summary>
        /// Display a note with no message type.
        /// </summary>
        public NoteAttribute(string note) : this(note, MessageType.None) { }

        /// <summary>
        /// Display a note with given message type.
        /// </summary>
        public NoteAttribute(string text, MessageType type)
        {
            Text = text.Replace("\n", Environment.NewLine);
            Type = type;
        }

        #endregion
    }
}