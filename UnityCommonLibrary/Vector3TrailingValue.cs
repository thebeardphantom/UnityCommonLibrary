using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UCL
{
    [Serializable]
    public sealed class Vector3TrailingValue : TrailingValue<Vector3>
    {
        #region Types

        [Serializable]
        public struct Level
        {
            #region Fields

            [NonSerialized]
            public Vector3 Value;

            public Vector3 Speed;

            #endregion

            #region Methods

            public (Vector3 Value, Vector3 Speed) ToTuple()
            {
                return (Value, Speed);
            }

            #endregion
        }

        #endregion

        #region Fields

        [SerializeField]
        private List<Level> _levels = new List<Level>();

        #endregion

        #region Properties

        /// <inheritdoc />
        public override int LevelCount => _levels.Count;

        #endregion

        #region Methods

        /// <inheritdoc />
        public override (Vector3 Value, Vector3 Speed) GetLevelAt(int levelIndex)
        {
            return _levels[levelIndex].ToTuple();
        }

        /// <inheritdoc />
        public override void SetValueAt(int levelIndex, Vector3 value)
        {
            var level = _levels[levelIndex];
            level.Value = value;
            _levels[levelIndex] = level;
        }

        /// <inheritdoc />
        public override void SetSpeedAt(int levelIndex, Vector3 speed)
        {
            var level = _levels[levelIndex];
            level.Speed = speed;
            _levels[levelIndex] = level;
        }

        /// <inheritdoc />
        public override Vector3 DefaultAnimateCallback(
            (Vector3 Value, Vector3 Speed) level,
            Vector3 targetValue,
            float deltaTime)
        {
            (var currentValue, var speed) = level;
            return new Vector3
            {
                x = Mathf.Lerp(currentValue.x, targetValue.x, speed.x * deltaTime),
                y = Mathf.Lerp(currentValue.y, targetValue.y, speed.y * deltaTime),
                z = Mathf.Lerp(currentValue.z, targetValue.z, speed.z * deltaTime)
            };
        }

        /// <inheritdoc />
        protected override void RemoveLevelAt(int levelIndex)
        {
            _levels.RemoveAt(levelIndex);
        }

        /// <inheritdoc />
        protected override void InsertNewLevel(int levelIndex)
        {
            _levels.Insert(levelIndex, new Level());
        }

        #endregion
    }
}