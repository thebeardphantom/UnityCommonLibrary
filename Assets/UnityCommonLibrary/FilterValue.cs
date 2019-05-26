using System;
using System.Text.RegularExpressions;
using BeardPhantom.UCL.Signals;

namespace BeardPhantom.UCL
{
    /// <summary>
    /// A string wrapper class for managing a search value
    /// </summary>
    public class FilterValue
    {
        #region Fields

        public readonly Signal<FilterValue> ValueChanged = new Signal<FilterValue>();

        #endregion

        #region Properties

        /// <summary>
        /// Raw input string
        /// </summary>
        public string RawValue { get; private set; }

        /// <summary>
        /// Cleaned and trimmed value string
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Whitespace delimited keywords
        /// </summary>
        public string[] Keywords { get; private set; }

        /// <summary>
        /// Whether there is any filter active
        /// </summary>
        public bool HasFilter => !string.IsNullOrWhiteSpace(Value);

        #endregion

        #region Methods

        /// <summary>
        /// Does input match the current Value
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool MatchesValue(string input)
        {
            return !HasFilter || input.IndexOf(Value, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Does input match any or all of the whitespace delimited keywords
        /// </summary>
        /// <param name="input"></param>
        /// <param name="all"></param>
        /// <returns></returns>
        public bool MatchesKeywords(string input, bool all)
        {
            if (!HasFilter)
            {
                return true;
            }

            foreach (var keyword in Keywords)
            {
                var isMatch = input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
                if (all && !isMatch)
                {
                    // Early out if not all match
                    return false;
                }

                if (!all && isMatch)
                {
                    // Early out if any match
                    return true;
                }
            }

            // Match found if early out didn't happen
            return all;
        }

        /// <summary>
        /// Updates underlying values
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if there was an update, false if nothing has changed</returns>
        public bool Update(string value)
        {
            if (RawValue != value)
            {
                RawValue = value;
                Value = RawValue.Trim();
                Keywords = Regex.Split(Value, @"\s");
                ValueChanged.Publish(this);
                return true;
            }

            return false;
        }

        #endregion
    }
}