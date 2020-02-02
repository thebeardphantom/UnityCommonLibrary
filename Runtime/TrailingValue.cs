using System;

namespace BeardPhantom.UCL
{
    [Serializable]
    public abstract class TrailingValue<T> where T : struct
    {
        #region Types

        public delegate T AnimateValue((T Value, T Speed) level, T targetValue, float deltaTime);

        #endregion

        #region Fields

        [NonSerialized]
        public T TargetValue;

        public AnimateValue AnimateCallback;

        #endregion

        #region Properties

        public abstract int LevelCount { get; }

        public T Value => GetLevelAt(LevelCount - 1).Value;

        #endregion

        #region Constructors

        protected TrailingValue(int levels)
        {
            SetLevelCount(levels);
        }

        protected TrailingValue() : this(2)
        {
            AnimateCallback = DefaultAnimateCallback;
        }

        #endregion

        #region Methods

        public T this[int index] => GetLevelAt(index).Value;

        public abstract (T Value, T Speed) GetLevelAt(int levelIndex);

        public abstract void SetValueAt(int levelIndex, T value);

        public abstract void SetSpeedAt(int levelIndex, T speed);

        public abstract T DefaultAnimateCallback((T Value, T Speed) level, T targetValue, float deltaTime);

        protected abstract void RemoveLevelAt(int levelIndex);

        protected abstract void InsertNewLevel(int levelIndex);

        public void SetLevelAt(int levelIndex, (T Value, T Speed) level)
        {
            (var value, var speed) = level;
            SetValueAt(levelIndex, value);
            SetSpeedAt(levelIndex, speed);
        }

        public void SetLevels(params T[] speeds)
        {
            SetLevelCount(speeds.Length);
            for (var i = 0; i < speeds.Length; i++)
            {
                SetSpeedAt(i, speeds[i]);
            }
        }

        public void Complete()
        {
            SetAllLevelValues(TargetValue);
        }

        public void SetAllLevelValues(T value)
        {
            var levelCount = LevelCount;
            for (var i = 0; i < levelCount; i++)
            {
                SetValueAt(i, value);
            }
        }

        public void Tick(float deltaTime)
        {
            var levelCount = LevelCount;
            for (var i = 0; i < levelCount; i++)
            {
                var currentLevel = GetLevelAt(i);
                var targetValue = TargetValue;
                if (i > 0)
                {
                    (var previousValue, var _) = GetLevelAt(i);
                    targetValue = previousValue;
                }

                currentLevel.Value = AnimateCallback(currentLevel, targetValue, deltaTime);
                SetLevelAt(i, currentLevel);
            }
        }

        public void SetLevelCount(int levels)
        {
            if (levels <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(levels));
            }

            while (LevelCount > levels)
            {
                RemoveLevelAt(LevelCount - 1);
            }

            while (LevelCount < levels)
            {
                InsertNewLevel(LevelCount);
            }
        }

        #endregion
    }
}