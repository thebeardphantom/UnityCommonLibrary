namespace BeardPhantom.UCL
{
    /// <summary>
    /// A value that fires a signal when its value changes.
    /// </summary>
    public class ObservedValue<T>
    {
        #region Types

        public readonly struct ChangedEventArgs
        {
            #region Fields

            public readonly T PreviousValue;

            public readonly T Value;

            #endregion

            #region Constructors

            public ChangedEventArgs(T previousValue, T value)
            {
                PreviousValue = previousValue;
                Value = value;
            }

            #endregion
        }

        #endregion

        #region Events

        public event ValueEventHandler<ChangedEventArgs> ValueChanged;

        #endregion

        #region Fields

        private T _value;

        #endregion

        #region Properties

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(_value, value))
                {
                    return;
                }

                var previousValue = _value;
                _value = value;
                ValueChanged?.Invoke(new ChangedEventArgs(previousValue, _value));
            }
        }

        #endregion

        #region Constructors

        public ObservedValue(T value = default)
        {
            _value = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Implicity convert to T
        /// </summary>
        public static implicit operator T(ObservedValue<T> t)
        {
            return t.Value;
        }

        #endregion
    }
}