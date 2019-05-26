using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeardPhantom.UCL
{
    [Serializable]
    public sealed class FloatTrailingValue : TrailingValue<float>
    {
        #region Types

        [Serializable]
        public struct Level
        {
            #region Fields

            [NonSerialized]
            public float Value;

            public float Speed;

            #endregion

            #region Methods

            public (float Value, float Speed) ToTuple()
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
        public override (float Value, float Speed) GetLevelAt(int levelIndex)
        {
            return _levels[levelIndex].ToTuple();
        }

        /// <inheritdoc />
        public override void SetValueAt(int levelIndex, float value)
        {
            var level = _levels[levelIndex];
            level.Value = value;
            _levels[levelIndex] = level;
        }

        /// <inheritdoc />
        public override void SetSpeedAt(int levelIndex, float speed)
        {
            var level = _levels[levelIndex];
            level.Speed = speed;
            _levels[levelIndex] = level;
        }

        /// <inheritdoc />
        public override float DefaultAnimateCallback(
            (float Value, float Speed) level,
            float targetValue,
            float deltaTime)
        {
            (var currentValue, var speed) = level;
            return Mathf.Lerp(currentValue, targetValue, speed * deltaTime);
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