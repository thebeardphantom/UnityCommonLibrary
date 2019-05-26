using BeardPhantom.UCL.Signals;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// A value that fires a signal when its value changes.
    /// </summary>
    public class ObservedValue<T>
    {
        #region Fields

        public readonly Signal<T, T> ValueChanged = new Signal<T, T>();

        private T _value;

        #endregion

        #region Properties

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    var previousValue = _value;
                    _value = value;
                    ValueChanged.Publish(previousValue, _value);
                }
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