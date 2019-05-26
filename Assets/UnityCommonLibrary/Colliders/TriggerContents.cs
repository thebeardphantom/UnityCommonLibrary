using System.Collections.Generic;
using BeardPhantom.UCL.Signals;
using UnityEngine;

namespace BeardPhantom.UCL.Colliders
{
    public class TriggerContents<T> : MonoBehaviour where T : Component
    {
        #region Fields

        public readonly Signal<bool> ContentsChanged = new Signal<bool>();

        #endregion

        #region Properties

        /// <summary>
        /// The trigger collider
        /// </summary>
        public T Trigger { get; protected set; }

        /// <summary>
        /// The contents of the triggers
        /// </summary>
        public HashSet<T> Contents { get; } = new HashSet<T>();

        /// <summary>
        /// Is there anything contained in the trigger?
        /// </summary>
        public bool HasAny => Contents.Count > 0;

        #endregion

        #region Methods

        protected virtual void Awake()
        {
            Trigger = GetComponent<T>();
        }

        /// <summary>
        /// Update collection when collider enters
        /// </summary>
        protected void OnEnter(T other)
        {
            Contents.RemoveWhere(c => c == null);
            Contents.Add(other);
            ContentsChanged.Publish(Contents.Count == 1);
        }

        /// <summary>
        /// Update collection when collider exits
        /// </summary>
        protected void OnExit(T other)
        {
            Contents.RemoveWhere(c => c == null || c == other);
            ContentsChanged.Publish(Contents.Count == 0);
        }

        #endregion
    }
}